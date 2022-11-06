--SELECT * FROM UnidadeFederativa
SET NOCOUNT ON

DECLARE @Turno tinyint = 1;

DECLARE @UFSigla    char(2),
        @AuxInt     int,
        @AuxInt2    int,
        @AuxVarchar varchar(200);

BEGIN -- Relatório 1 - Sessões eleitorais com o maior percentual de votos para Lula (na Comparação Lula/Bolsonaro)
    DECLARE @tmpVotosPorSecaoLulaBolsonaro TABLE (
        UFSigla             char(2),
        CodMunicipio        int,
        CodZonaEleitoral    smallint,
        CodSecaoEleitoral   smallint,
        QtdVotosLula        int,
        QtdVotosBolsonaro   int,
        QtdVotosTotal       int,
        QtdVotosLulaEBolso  int,
        PercentualLula      numeric(18,2),
        PercentualBolsonaro numeric(18,2)
    )

    SET @UFSigla = ''
    WHILE EXISTS(SELECT TOP 1 Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla)
    BEGIN
        SELECT TOP 1 @UFSigla = Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla
    
        INSERT INTO @tmpVotosPorSecaoLulaBolsonaro (UFSigla, CodMunicipio, CodZonaEleitoral, CodSecaoEleitoral, QtdVotosLula, QtdVotosBolsonaro, QtdVotosLulaEBolso, QtdVotosTotal)
        SELECT      M.UFSigla, 
                    SE.MunicipioCodigo, 
                    SE.CodigoZonaEleitoral, 
                    SE.CodigoSecao, 
                    ISNULL(VS13.QtdVotos, 0) As Votos13,
                    ISNULL(VS22.QtdVotos, 0) As Votos22,
                    ISNULL(VS22.QtdVotos, 0) + ISNULL(VS13.QtdVotos, 0) as VotosLulaBolso,
                    SE.PR_VotosNominais as QtdVotosTotal
        FROM        SecaoEleitoral SE with (NOLOCK)
        LEFT JOIN   VotosSecao VS13 with (NOLOCK) 
            ON      VS13.MunicipioCodigo = SE.MunicipioCodigo 
                AND VS13.CodigoZonaEleitoral = SE.CodigoZonaEleitoral 
                AND VS13.CodigoSecao = SE.CodigoSecao 
                AND VS13.Cargo = 5 
                AND VS13.NumeroCandidato = 13
        LEFT JOIN   VotosSecao VS22 with (NOLOCK) 
            ON      VS22.MunicipioCodigo = SE.MunicipioCodigo 
                AND VS22.CodigoZonaEleitoral = SE.CodigoZonaEleitoral 
                AND VS22.CodigoSecao = SE.CodigoSecao 
                AND VS22.Cargo = 5 
                AND VS22.NumeroCandidato = 22
        INNER JOIN  Municipio M with (NOLOCK) 
            ON      M.Codigo = SE.MunicipioCodigo 
                AND M.UFSigla = @UFSigla
    END

    UPDATE  @tmpVotosPorSecaoLulaBolsonaro
        SET PercentualLula      = CASE WHEN (QtdVotosTotal) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosLula) / QtdVotosTotal) * 100 END,
            PercentualBolsonaro = CASE WHEN (QtdVotosTotal) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosBolsonaro) / QtdVotosTotal) * 100 END

    -- Contar quantas seções tiveram 0 votos para Lula ou para Bolsonaro
    SELECT @AuxInt = COUNT(*) FROM @tmpVotosPorSecaoLulaBolsonaro WHERE QtdVotosLula = 0
    PRINT '### Quantidade de Seções eleitorais que não tiveram votos para o Lula: ' + CONVERT(varchar(20), @AuxInt) + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Qtd Votos Bolsonaro: ' + CONVERT(varchar(20), T.QtdVotosBolsonaro) + ' - ' + FORMAT((CONVERT(numeric(18,2), T.QtdVotosBolsonaro) / T.QtdVotosTotal) * 100, 'N2') + '% do total.' as ' ' 
        FROM @tmpVotosPorSecaoLulaBolsonaro T 
        INNER JOIN Municipio M with (NOLOCK) ON M.Codigo = T.CodMunicipio 
        INNER JOIN UnidadeFederativa U with (NOLOCK) ON U.Sigla = T.UFSigla
        WHERE T.QtdVotosLula = 0
        ORDER BY T.UFSigla, T.CodMunicipio, T.CodZonaEleitoral, T.CodSecaoEleitoral
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

    SELECT @AuxInt = COUNT(*) FROM @tmpVotosPorSecaoLulaBolsonaro WHERE QtdVotosBolsonaro = 0
    PRINT '### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: ' + CONVERT(varchar(20), @AuxInt) + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Qtd Votos Lula: ' + CONVERT(varchar(20), T.QtdVotosLula) + ' - ' + FORMAT((CONVERT(numeric(18,2), T.QtdVotosLula) / T.QtdVotosTotal) * 100, 'N2') + '% do total.' as ' ' 
        FROM @tmpVotosPorSecaoLulaBolsonaro T 
        INNER JOIN Municipio M with (NOLOCK) ON M.Codigo = T.CodMunicipio 
        INNER JOIN UnidadeFederativa U with (NOLOCK) ON U.Sigla = T.UFSigla
        WHERE T.QtdVotosBolsonaro = 0 
        ORDER BY T.UFSigla, T.CodMunicipio, T.CodZonaEleitoral, T.CodSecaoEleitoral
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

END

BEGIN -- Relatório 2 - Contagem de seções eleitorais por UF, e votos por seção eleitoral (comparando com a informação disponível no site do TSE)
    PRINT '### Comparação da carga dos arquivos do TSE com o número informado no site do TSE.'
    
    DECLARE @DadosTSE TABLE(
        UFSigla             char(2),
        QtdSecoes           int,
        QtdVotosPresidente  int
    )

    --AC,AL,AP,AM,BA,CE,DF,ES,GO,MA,MT,MS,MG,PA,PB,PR,PE,PI,RJ,RN,RS,RO,RR,SC,SP,SE,TO,ZZ
    -- 1o Turno
    IF(@Turno = 1)
    BEGIN
        INSERT INTO @DadosTSE SELECT 'AC', 2124, 455903
        INSERT INTO @DadosTSE SELECT 'AL', 6626, 1805971
        INSERT INTO @DadosTSE SELECT 'AP', 1740, 442842
        INSERT INTO @DadosTSE SELECT 'AM', 7454, 2113771
        INSERT INTO @DadosTSE SELECT 'BA', 34424, 8874841
        INSERT INTO @DadosTSE SELECT 'CE', 22796, 5628610
        INSERT INTO @DadosTSE SELECT 'DF', 6748, 1819900
        INSERT INTO @DadosTSE SELECT 'ES', 9239, 2315889
        INSERT INTO @DadosTSE SELECT 'GO', 14620, 3812597
        INSERT INTO @DadosTSE SELECT 'MA', 16423, 3920435
        INSERT INTO @DadosTSE SELECT 'MT', 7652, 1892180
        INSERT INTO @DadosTSE SELECT 'MS', 6912, 1555149
        INSERT INTO @DadosTSE SELECT 'MG', 49981, 12655228
        INSERT INTO @DadosTSE SELECT 'PA', 18235, 4789311
        INSERT INTO @DadosTSE SELECT 'PB', 9602, 2557479
        INSERT INTO @DadosTSE SELECT 'PR', 25721, 6828543
        INSERT INTO @DadosTSE SELECT 'PE', 20572, 5738371
        INSERT INTO @DadosTSE SELECT 'PI', 8963, 2115645
        INSERT INTO @DadosTSE SELECT 'RJ', 34068, 9909463
        INSERT INTO @DadosTSE SELECT 'RN', 7674, 2090604
        INSERT INTO @DadosTSE SELECT 'RS', 27201, 6890016
        INSERT INTO @DadosTSE SELECT 'RO', 4198, 926827
        INSERT INTO @DadosTSE SELECT 'RR', 1268, 305404
        INSERT INTO @DadosTSE SELECT 'SC', 16242, 4487474
        INSERT INTO @DadosTSE SELECT 'SP', 101073, 27189714
        INSERT INTO @DadosTSE SELECT 'SE', 5498, 1364724
        INSERT INTO @DadosTSE SELECT 'TO', 3957, 891449
        INSERT INTO @DadosTSE SELECT 'ZZ', 1064, 304032
    END

    IF(@Turno = 2)
    BEGIN
        INSERT INTO @DadosTSE SELECT 'AC', 2124, 420760
        INSERT INTO @DadosTSE SELECT 'AL', 6626, 1784988
        INSERT INTO @DadosTSE SELECT 'AP', 1740, 400.683
        INSERT INTO @DadosTSE SELECT 'AM', 7454, 2067875
        INSERT INTO @DadosTSE SELECT 'BA', 34424, 8971728
        INSERT INTO @DadosTSE SELECT 'CE', 22796, 5649398
        INSERT INTO @DadosTSE SELECT 'DF', 6748, 1838492
        INSERT INTO @DadosTSE SELECT 'ES', 9239, 2322269
        INSERT INTO @DadosTSE SELECT 'GO', 14620, 3860351
        INSERT INTO @DadosTSE SELECT 'MA', 16423, 3854804
        INSERT INTO @DadosTSE SELECT 'MT', 7652, 1913231
        INSERT INTO @DadosTSE SELECT 'MS', 6912, 1549873
        INSERT INTO @DadosTSE SELECT 'MG', 49981, 12866284
        INSERT INTO @DadosTSE SELECT 'PA', 18235, 4701740
        INSERT INTO @DadosTSE SELECT 'PB', 9602, 2574215
        INSERT INTO @DadosTSE SELECT 'PR', 25721, 6900420
        INSERT INTO @DadosTSE SELECT 'PE', 20572, 5800735
        INSERT INTO @DadosTSE SELECT 'PI', 8963, 2088530
        INSERT INTO @DadosTSE SELECT 'RJ', 34068, 9973822
        INSERT INTO @DadosTSE SELECT 'RN', 7674, 2108799
        INSERT INTO @DadosTSE SELECT 'RS', 27201, 6930852
        INSERT INTO @DadosTSE SELECT 'RO', 4198, 926517
        INSERT INTO @DadosTSE SELECT 'RR', 1268, 286269
        INSERT INTO @DadosTSE SELECT 'SC', 16242, 4542817
        INSERT INTO @DadosTSE SELECT 'SP', 101073, 27380491
        INSERT INTO @DadosTSE SELECT 'SE', 5498, 1355467
        INSERT INTO @DadosTSE SELECT 'TO', 3957, 871238
        INSERT INTO @DadosTSE SELECT 'ZZ', 1064, 310148
    END

    SET @UFSigla = ''
    WHILE EXISTS(SELECT TOP 1 Sigla FROM UnidadeFederativa with(NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla)
    BEGIN
        SELECT TOP 1 @UFSigla = Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla

        SELECT      @AuxInt = COUNT(*)
        FROM        SecaoEleitoral SE with (NOLOCK) 
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = SE.MunicipioCodigo
                AND M.UFSigla = @UFSigla

        SELECT @AuxInt2 = QtdSecoes FROM @DadosTSE WHERE UFSigla = @UFSigla

        IF @AuxInt <> @AuxInt2
        BEGIN
            PRINT '- UF ' + @UFSigla + ' - Quantidade de seções eleitorais carregadas (' + CONVERT(varchar(20), @AuxInt) + ') é diferente do TSE (' + CONVERT(varchar(20), @AuxInt2) + ').'
        END

        SELECT      @AuxInt = SUM(CONVERT(int, SE.PR_Total))
        FROM        SecaoEleitoral SE with (NOLOCK) 
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = SE.MunicipioCodigo
                AND M.UFSigla = @UFSigla

        SELECT @AuxInt2 = QtdVotosPresidente FROM @DadosTSE WHERE UFSigla = @UFSigla

        IF @AuxInt <> @AuxInt2
        BEGIN
            PRINT '- UF ' + @UFSigla + ' - Quantidade de votos válidos carregados (' + CONVERT(varchar(20), @AuxInt) + ') é diferente do TSE (' + CONVERT(varchar(20), @AuxInt2) + ').'
        END

    END

END

BEGIN -- Relatório 3 - Códigos de identificação de urna eletrônica repetidos
    PRINT '### Seções eleitorais com Códigos de Identificação de Urna Eletrônica repetidos'

    SELECT @AuxInt = COUNT(*) FROM (
        SELECT      CodigoIdentificacaoUrnaEletronica
        FROM        SecaoEleitoral with (NOLOCK)
        GROUP BY    CodigoIdentificacaoUrnaEletronica
        HAVING      COUNT(*) > 1
    ) as T

    PRINT 'Existem ' + CONVERT(varchar(20), @AuxInt) + ' Códigos de Identificação de Urna Eletrônica que se repetem para duas ou mais seções eleitorais. Cada urna não deveria ter seu próprio número de série único?'

    DECLARE C1 CURSOR FOR
    SELECT      CodigoIdentificacaoUrnaEletronica, 
                COUNT(*) as QtdRepetidos
    FROM        SecaoEleitoral with (NOLOCK)
    GROUP BY    CodigoIdentificacaoUrnaEletronica
    HAVING      COUNT(*) > 1
    ORDER BY    COUNT(*) DESC

    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT '#### Código Identificador de Urna Eletrônica: ' + CONVERT(varchar(20), @AuxInt) + ' - Quantidade de ocorrências: ' + CONVERT(varchar(20), @AuxInt2) + '.'

        DECLARE C2 CURSOR FOR
        SELECT      '- ' + UF.Sigla + '(' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto
        FROM        SecaoEleitoral SE with (NOLOCK)
        INNER JOIN  Municipio M with (NOLOCK) ON M.Codigo = SE.MunicipioCodigo
        INNER JOIN  UnidadeFederativa UF with (NOLOCK) ON UF.Sigla = M.UFSigla
        WHERE       SE.CodigoIdentificacaoUrnaEletronica = @AuxInt
        ORDER BY    UF.Sigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

        OPEN C2
        FETCH NEXT FROM C2 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C2 INTO @AuxVarchar
        END
        CLOSE C2
        DEALLOCATE C2

        FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    END
    CLOSE C1
    DEALLOCATE C1

END



