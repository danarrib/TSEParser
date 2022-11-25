--SELECT * FROM UnidadeFederativa
SET NOCOUNT ON

DECLARE @Turno tinyint;
SELECT @Turno = CASE WHEN DB_NAME() = 'TSEParser_T1' THEN 1 ELSE 2 END

DECLARE @UFSigla    char(2),
        @AuxInt     int,
        @AuxInt2    int,
        @AuxInt3    int,
        @AuxVarchar varchar(200);

IF 0=1
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
    PRINT '#### Quantidade de Seções eleitorais que não tiveram votos para o Lula: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Qtd Votos Bolsonaro: ' + FORMAT(T.QtdVotosBolsonaro, '#,###', 'pt-br') + ' - ' + FORMAT((CONVERT(numeric(18,2), T.QtdVotosBolsonaro) / T.QtdVotosTotal) * 100, 'N2', 'pt-br') + '% do total.' as ' ' 
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
    PRINT '#### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Qtd Votos Lula: ' + FORMAT(T.QtdVotosLula, '#,###', 'pt-br') + ' - ' + FORMAT((CONVERT(numeric(18,2), T.QtdVotosLula) / T.QtdVotosTotal) * 100, 'N2', 'pt-br') + '% do total.' as ' ' 
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

    -- Por UF
    SELECT @AuxInt = SUM(QtdVotosBolsonaro) FROM @tmpVotosPorSecaoLulaBolsonaro WHERE QtdVotosLula = 0
    PRINT '#### Quantidade de votos para o Bolsonaro nas seções eleitorais que não tiveram votos para o Lula: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Qtd Votos Bolsonaro: ' + FORMAT(SUM(T.QtdVotosBolsonaro), '#,###', 'pt-br') + '.' as ' ' 
        FROM @tmpVotosPorSecaoLulaBolsonaro T 
        INNER JOIN UnidadeFederativa U with (NOLOCK) ON U.Sigla = T.UFSigla
        WHERE T.QtdVotosLula = 0
        GROUP BY T.UFSigla, U.Nome
        ORDER BY T.UFSigla
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

    SELECT @AuxInt = SUM(QtdVotosLula) FROM @tmpVotosPorSecaoLulaBolsonaro WHERE QtdVotosBolsonaro = 0
    PRINT '#### Quantidade de votos para o Lula nas seções eleitorais que não tiveram votos para o Bolsonaro: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Qtd Votos Lula: ' + FORMAT(SUM(T.QtdVotosLula), '#,###', 'pt-br') + '.' as ' ' 
        FROM @tmpVotosPorSecaoLulaBolsonaro T 
        INNER JOIN UnidadeFederativa U with (NOLOCK) ON U.Sigla = T.UFSigla
        WHERE T.QtdVotosBolsonaro = 0
        GROUP BY T.UFSigla, U.Nome
        ORDER BY T.UFSigla
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

IF 0=1
BEGIN -- Relatório 2 - Contagem de seções eleitorais por UF, e votos por seção eleitoral (comparando com a informação disponível no site do TSE)
    PRINT '### Comparação da carga dos arquivos do TSE com o número informado no site do TSE.'
    
    DECLARE @DadosTSE TABLE(
        UFSigla             char(2),
        QtdSecoes           int,
        QtdVotosPresidente  int
    )

    IF EXISTS(SELECT 1 FROM @DadosTSE T LEFT JOIN UnidadeFederativa UF ON UF.Sigla = T.UFSigla WHERE UF.Sigla IS NULL)
    BEGIN
        PRINT 'Existem UFs que ainda não foram carregadas'
        SELECT UFSigla FROM @DadosTSE T LEFT JOIN UnidadeFederativa UF ON UF.Sigla = T.UFSigla WHERE UF.Sigla IS NULL
    END

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
        INSERT INTO @DadosTSE SELECT 'AP', 1740, 400683
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
            PRINT '- UF ' + @UFSigla + ' - Quantidade de votos válidos para presidente carregados (' + CONVERT(varchar(20), @AuxInt) + ') é diferente do TSE (' + CONVERT(varchar(20), @AuxInt2) + ').'
        END
    END
END

IF 0=1
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

IF 0=1
BEGIN -- Relatório 4 - Seções eleitorais que tiveram as maiores mudanças de lado (Viraram de Bolsonaro para Lula e Vice-versa)

    DECLARE @tmpVotosPorSecaoLulaBolsonaro2T TABLE (
        UFSigla                 char(2),
        CodMunicipio            int,
        CodZonaEleitoral        smallint,
        CodSecaoEleitoral       smallint,
        QtdVotosLula1T          int,
        QtdVotosBolsonaro1T     int,
        QtdVotosTotal1T         int,
        QtdVotosLulaEBolso1T    int,
        PercentualLula1T        numeric(18,2),
        PercentualBolsonaro1T   numeric(18,2),
        QtdVotosLula2T          int,
        QtdVotosBolsonaro2T     int,
        QtdVotosTotal2T         int,
        QtdVotosLulaEBolso2T    int,
        PercentualLula2T        numeric(18,2),
        PercentualBolsonaro2T   numeric(18,2),
        VariacaoBolsoLula       numeric(18,2),
        VariacaoVotosBolsoLula  int
    )

    SET @UFSigla = ''
    WHILE EXISTS(SELECT TOP 1 Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla)
    BEGIN
        SELECT TOP 1 @UFSigla = Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla
    
        INSERT INTO @tmpVotosPorSecaoLulaBolsonaro2T (UFSigla, CodMunicipio, CodZonaEleitoral, CodSecaoEleitoral,
                                                        QtdVotosLula1T, QtdVotosBolsonaro1T, QtdVotosLulaEBolso1T, QtdVotosTotal1T,
                                                        QtdVotosLula2T, QtdVotosBolsonaro2T, QtdVotosLulaEBolso2T, QtdVotosTotal2T)
        SELECT      M.UFSigla, 
                    SE.MunicipioCodigo, 
                    SE.CodigoZonaEleitoral, 
                    SE.CodigoSecao, 
                    ISNULL(VS13.QtdVotos, 0) As Votos13,
                    ISNULL(VS22.QtdVotos, 0) As Votos22,
                    ISNULL(VS22.QtdVotos, 0) + ISNULL(VS13.QtdVotos, 0) as VotosLulaBolso,
                    SE.PR_VotosNominais as QtdVotosTotal,
                    ISNULL(VS132T.QtdVotos, 0) As Votos132T,
                    ISNULL(VS222T.QtdVotos, 0) As Votos222T,
                    ISNULL(VS222T.QtdVotos, 0) + ISNULL(VS13.QtdVotos, 0) as VotosLulaBolso2T,
                    SE2T.PR_VotosNominais as QtdVotosTotal2T
        FROM        SecaoEleitoral SE with (NOLOCK)
        LEFT JOIN   TSEParser_T2..SecaoEleitoral SE2T with (NOLOCK)
            ON      SE2T.MunicipioCodigo = SE.MunicipioCodigo 
                AND SE2T.CodigoZonaEleitoral = SE.CodigoZonaEleitoral 
                AND SE2T.CodigoSecao = SE.CodigoSecao
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
        LEFT JOIN   TSEParser_T2..VotosSecao VS132T with (NOLOCK) 
            ON      VS132T.MunicipioCodigo = SE.MunicipioCodigo 
                AND VS132T.CodigoZonaEleitoral = SE.CodigoZonaEleitoral 
                AND VS132T.CodigoSecao = SE.CodigoSecao 
                AND VS132T.Cargo = 5 
                AND VS132T.NumeroCandidato = 13
        LEFT JOIN   TSEParser_T2..VotosSecao VS222T with (NOLOCK) 
            ON      VS222T.MunicipioCodigo = SE.MunicipioCodigo 
                AND VS222T.CodigoZonaEleitoral = SE.CodigoZonaEleitoral 
                AND VS222T.CodigoSecao = SE.CodigoSecao 
                AND VS222T.Cargo = 5 
                AND VS222T.NumeroCandidato = 22
        INNER JOIN  Municipio M with (NOLOCK) 
            ON      M.Codigo = SE.MunicipioCodigo 
                AND M.UFSigla = @UFSigla
    END

    UPDATE  @tmpVotosPorSecaoLulaBolsonaro2T
        SET PercentualLula1T      = CASE WHEN (QtdVotosTotal1T) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosLula1T) / QtdVotosTotal1T) * 100 END,
            PercentualBolsonaro1T = CASE WHEN (QtdVotosTotal1T) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosBolsonaro1T) / QtdVotosTotal1T) * 100 END,
            PercentualLula2T      = CASE WHEN (QtdVotosTotal2T) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosLula2T) / QtdVotosTotal2T) * 100 END,
            PercentualBolsonaro2T = CASE WHEN (QtdVotosTotal2T) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosBolsonaro2T) / QtdVotosTotal2T) * 100 END

    UPDATE  @tmpVotosPorSecaoLulaBolsonaro2T
        SET VariacaoBolsoLula = (PercentualBolsonaro1T - PercentualLula1T) - (PercentualBolsonaro2T - PercentualLula2T),
            VariacaoVotosBolsoLula = (QtdVotosLula1T - QtdVotosLula2T) + (QtdVotosBolsonaro2T - QtdVotosBolsonaro1T)

    DECLARE @tmpVotosPorMunicipioLulaBolsonaro2T TABLE (
            UFSigla                 char(2),
            CodMunicipio            int,
            QtdVotosLula1T          int,
            QtdVotosBolsonaro1T     int,
            QtdVotosTotal1T         int,
            QtdVotosLulaEBolso1T    int,
            PercentualLula1T        numeric(18,2),
            PercentualBolsonaro1T   numeric(18,2),
            QtdVotosLula2T          int,
            QtdVotosBolsonaro2T     int,
            QtdVotosTotal2T         int,
            QtdVotosLulaEBolso2T    int,
            PercentualLula2T        numeric(18,2),
            PercentualBolsonaro2T   numeric(18,2),
            VariacaoBolsoLula       numeric(18,2),
            VariacaoVotosBolsoLula  int -- Quantos votos o Lula perdeu + Quantos votos o Bolsonaro ganhou do primeiro para o segundo turno
        )

    INSERT INTO @tmpVotosPorMunicipioLulaBolsonaro2T (UFSigla, CodMunicipio, QtdVotosLula1T, QtdVotosBolsonaro1T, QtdVotosTotal1T, QtdVotosLulaEBolso1T, QtdVotosLula2T, QtdVotosBolsonaro2T, QtdVotosTotal2T, QtdVotosLulaEBolso2T)
    SELECT      T.UFSigla,
                T.CodMunicipio,
                SUM(T.QtdVotosLula1T),
                SUM(T.QtdVotosBolsonaro1T),
                SUM(T.QtdVotosTotal1T),
                SUM(T.QtdVotosLulaEBolso1T),
                SUM(T.QtdVotosLula2T),
                SUM(T.QtdVotosBolsonaro2T),
                SUM(T.QtdVotosTotal2T),
                SUM(T.QtdVotosLulaEBolso2T)
    FROM        @tmpVotosPorSecaoLulaBolsonaro2T T
    GROUP BY    T.UFSigla,
                T.CodMunicipio
                
    UPDATE  @tmpVotosPorMunicipioLulaBolsonaro2T
        SET PercentualLula1T      = CASE WHEN (QtdVotosTotal1T) = 0 THEN 0 ELSE (CONVERT(numeric(10,2), QtdVotosLula1T) / QtdVotosTotal1T) * 100 END,
            PercentualBolsonaro1T = CASE WHEN (QtdVotosTotal1T) = 0 THEN 0 ELSE (CONVERT(numeric(10,2), QtdVotosBolsonaro1T) / QtdVotosTotal1T) * 100 END,
            PercentualLula2T      = CASE WHEN (QtdVotosTotal2T) = 0 THEN 0 ELSE (CONVERT(numeric(10,2), QtdVotosLula2T) / QtdVotosTotal2T) * 100 END,
            PercentualBolsonaro2T = CASE WHEN (QtdVotosTotal2T) = 0 THEN 0 ELSE (CONVERT(numeric(10,2), QtdVotosBolsonaro2T) / QtdVotosTotal2T) * 100 END

    UPDATE  @tmpVotosPorMunicipioLulaBolsonaro2T
        SET VariacaoBolsoLula = (PercentualBolsonaro1T - PercentualLula1T) - (PercentualBolsonaro2T - PercentualLula2T),
            VariacaoVotosBolsoLula = (QtdVotosLula1T - QtdVotosBolsonaro1T) - (QtdVotosLula2T - QtdVotosBolsonaro2T)

    SELECT @AuxInt = COUNT(*) FROM @tmpVotosPorMunicipioLulaBolsonaro2T WHERE VariacaoVotosBolsoLula < 0
    PRINT '### Municípios que viraram votos para o Lula do primeiro para o segundo turno: ' + CONVERT(varchar(20), @AuxInt) + '.'

    PRINT '#### Lista dos municípios que viraram (a partir de 10 mil votos)'
    DECLARE C1 CURSOR FOR
        SELECT      '- UF ' + T.UFSigla + ' (' + UF.Nome + '), Municipio ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                    + '), Votos ganhos por Lula: ' + FORMAT(ABS(T.VariacaoVotosBolsoLula),'#,###', 'pt-br') + '.'
        FROM        @tmpVotosPorMunicipioLulaBolsonaro2T T
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = T.CodMunicipio
        INNER JOIN  UnidadeFederativa UF with (NOLOCK)
            ON      UF.Sigla = T.UFSigla
        WHERE       T.VariacaoVotosBolsoLula < -10000
        ORDER BY    T.VariacaoVotosBolsoLula
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

    SELECT @AuxInt = COUNT(*) FROM @tmpVotosPorMunicipioLulaBolsonaro2T WHERE VariacaoVotosBolsoLula > 0
    PRINT '### Municípios que viraram votos para o Bolsonaro do primeiro para o segundo turno: ' + CONVERT(varchar(20), @AuxInt) + '.'

    PRINT '#### Lista dos municípios que viraram (a partir de 10 mil votos)'
    DECLARE C1 CURSOR FOR
        SELECT      '- UF ' + T.UFSigla + ' (' + UF.Nome + '), Municipio ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                    + '), Votos ganhos por Bolsonaro: ' + FORMAT(ABS(T.VariacaoVotosBolsoLula),'#,###', 'pt-br') + '.'
        FROM        @tmpVotosPorMunicipioLulaBolsonaro2T T
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = T.CodMunicipio
        INNER JOIN  UnidadeFederativa UF with (NOLOCK)
            ON      UF.Sigla = T.UFSigla
        WHERE       T.VariacaoVotosBolsoLula > 10000
        ORDER BY    T.VariacaoVotosBolsoLula DESC
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

IF 0=1
BEGIN -- Relatório 5 - Contar os votos para Bolsonaro e Lula nas seções que tem Logs Inconsistentes ou Resultados emitidos pelo Sistema de Apuração (sem Log)
    DECLARE @tmpVotosSecoesDefeituosas TABLE (
        UFSigla             char(2),
        CodMunicipio        int,
        CodZonaEleitoral    smallint,
        CodSecaoEleitoral   smallint,
        QtdVotosLula        int,
        QtdVotosBolsonaro   int,
        QtdVotosTotal       int,
        QtdVotosLulaEBolso  int,
        LogUrnaInconsistente bit,
        PercentualLula      numeric(18,2),
        PercentualBolsonaro numeric(18,2)
    )    
    
    INSERT INTO @tmpVotosSecoesDefeituosas (UFSigla, CodMunicipio, CodZonaEleitoral, CodSecaoEleitoral, QtdVotosLula, QtdVotosBolsonaro, QtdVotosTotal, QtdVotosLulaEBolso, LogUrnaInconsistente)
    SELECT      M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                ISNULL(VS13.QtdVotos,0) as QtdVotosLula,
                ISNULL(VS22.QtdVotos,0) as QtdVotosBolsonaro,
                SE.PR_VotosNominais as QtdVotosTotal,
                ISNULL(VS13.QtdVotos,0) + ISNULL(VS22.QtdVotos,0) as QtdVotosLulaEBolso,
                SE.LogUrnaInconsistente
    FROM        SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  Municipio M with (NOLOCK) ON M.Codigo = SE.MunicipioCodigo 
    LEFT JOIN   VotosSecao      VS13 with (NOLOCK)
        ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS13.CodigoSecao            = SE.CodigoSecao
            AND VS13.Cargo                  = 5
            AND VS13.NumeroCandidato        = 13
    LEFT JOIN   VotosSecao      VS22 with (NOLOCK)
        ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS22.CodigoSecao            = SE.CodigoSecao
            AND VS22.Cargo                  = 5
            AND VS22.NumeroCandidato        = 22
    WHERE       SE.ResultadoSistemaApuracao = 1
            OR  SE.LogUrnaInconsistente     = 1

    UPDATE  @tmpVotosSecoesDefeituosas
        SET PercentualLula      = CASE WHEN (QtdVotosTotal) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosLula) / QtdVotosTotal) * 100 END,
            PercentualBolsonaro = CASE WHEN (QtdVotosTotal) = 0 THEN 0 ELSE (CONVERT(numeric(6,2), QtdVotosBolsonaro) / QtdVotosTotal) * 100 END

    PRINT '## Seções com Logs inconsistentes ou apuradas pelo sistema de apuração (SA)'

    SELECT      @AuxInt = SUM(T.QtdVotosLula), @AuxInt2 = SUM(T.QtdVotosBolsonaro), @AuxInt3 = COUNT(*)
    FROM        @tmpVotosSecoesDefeituosas  T

    PRINT 'Quantidade de seções: ' + FORMAT(@AuxInt3, '#,###','pt-br') + '. Votos para o Lula: ' + FORMAT(@AuxInt, '#,###','pt-br') + ', Votos para o Bolsonaro: ' + FORMAT(@AuxInt2, '#,###','pt-br') + '. Diferença: ' + FORMAT(ABS(@AuxInt - @AuxInt2), '#,###','pt-br') + ' votos.'

    PRINT 'Agrupado por UF:'
    DECLARE C1 CURSOR FOR
        SELECT      '- ' + UF.Sigla + ' (' + UF.Nome + '), Votos Lula: ' + FORMAT(SUM(T.QtdVotosLula), '#,###','pt-br') + ', Votos Bolsonaro: ' + FORMAT(SUM(T.QtdVotosBolsonaro), '#,###','pt-br') + '.' as Query
        FROM        @tmpVotosSecoesDefeituosas  T
        INNER JOIN  UnidadeFederativa UF with (NOLOCK)
            ON      UF.Sigla = T.UFSigla
        GROUP BY    UF.Sigla, UF.Nome
        ORDER BY    UF.Sigla
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

    PRINT 'Seções:'
    DECLARE C1 CURSOR FOR
        SELECT      '- ' + UF.Sigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Lula: ' + FORMAT(T.QtdVotosLula, '#,###','pt-br') + ', Bolsonaro: ' + FORMAT(T.QtdVotosBolsonaro, '#,###','pt-br') + ', Motivo: ' + CASE WHEN T.LogUrnaInconsistente = 1 THEN 'Log de Urna inconsistente' ELSE 'Sistema de apuração' END + '.' as Query
        FROM        @tmpVotosSecoesDefeituosas  T
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = T.CodMunicipio
        INNER JOIN  UnidadeFederativa UF with (NOLOCK)
            ON      UF.Sigla = T.UFSigla
        ORDER BY    UF.Sigla
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1


/*
    PRINT '#### Quantidade de Seções eleitorais que não tiveram votos para o Lula: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
                + ', Qtd Votos Bolsonaro: ' + FORMAT(T.QtdVotosBolsonaro, '#,###', 'pt-br') + ' - ' + FORMAT((CONVERT(numeric(18,2), T.QtdVotosBolsonaro) / T.QtdVotosTotal) * 100, 'N2', 'pt-br') + '% do total.' as ' ' 
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
    */




END

IF 0=1
BEGIN -- Relatório 6 - Votos para candidados por versão de urna
    DECLARE @tmpVersaoUrnaSecoes TABLE (
        UFSigla             char(2),
        CodMunicipio        int,
        CodZonaEleitoral    smallint,
        CodSecaoEleitoral   smallint,
        ModeloUrna          smallint,
        QtdVotosLulaAj      numeric(18,4),
        QtdVotosBolsonaroAj numeric(18,4),
        QtdVotosTotalAj     numeric(18,4),
        QtdVotosTotalModelo bigint,
        QtdVotosTotal       bigint,
        PercentualModelo    numeric(18,4)
    )

    SET @UFSigla = ''
    WHILE EXISTS(SELECT TOP 1 Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla)
    BEGIN
        SELECT TOP 1 @UFSigla = Sigla FROM UnidadeFederativa with (NOLOCK) WHERE Sigla <> 'BR' AND Sigla > @UFSigla
    
        RAISERROR( @UFSigla ,0,1) WITH NOWAIT

        -- Popular a tabela com os votos proporcionais por modelo de urna
        INSERT INTO @tmpVersaoUrnaSecoes (UFSigla, CodMunicipio, CodZonaEleitoral, CodSecaoEleitoral, ModeloUrna, QtdVotosLulaAj, QtdVotosBolsonaroAj, QtdVotosTotalAj, QtdVotosTotal, QtdVotosTotalModelo, PercentualModelo)
        SELECT      M.UFSigla,
                    SE.MunicipioCodigo,
                    SE.CodigoZonaEleitoral,
                    SE.CodigoSecao,
                    VL.ModeloUrnaEletronica,
                    VS13.QtdVotos * (COUNT(*) / CONVERT(numeric(6,2), SE.PR_Total)) as Votos13Ajustado,
                    VS22.QtdVotos * (COUNT(*) / CONVERT(numeric(6,2), SE.PR_Total)) as Votos22Ajustado,
                    (VS13.QtdVotos + VS22.QtdVotos) * (COUNT(*) / CONVERT(numeric(6,2), SE.PR_Total)) as VotosTotalAjustado,
                    SE.PR_Total,
                    COUNT(*) as QtdVotos,
                    (COUNT(*) / CONVERT(numeric(6,2), SE.PR_Total)) * 100 as Percentual
        FROM        SecaoEleitoral  SE with (NOLOCK)
        INNER JOIN  Municipio       M with (NOLOCK)
            ON      M.Codigo        = SE.MunicipioCodigo
                AND M.UFSigla       = @UFSigla
                AND SE.LogUrnaInconsistente = 0
                AND SE.ResultadoSistemaApuracao = 0
        INNER JOIN  VotosLog        VL with (NOLOCK)
            ON      VL.MunicipioCodigo      = SE.MunicipioCodigo
                AND VL.CodigoZonaEleitoral  = SE.CodigoZonaEleitoral
                AND VL.CodigoSecao          = SE.CodigoSecao
                AND VL.VotoComputado        = 1
        LEFT JOIN   VotosSecao      VS13 with (NOLOCK)
            ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
                AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
                AND VS13.CodigoSecao            = SE.CodigoSecao
                AND VS13.Cargo                  = 5
                AND VS13.NumeroCandidato        = 13
        LEFT JOIN   VotosSecao      VS22 with (NOLOCK)
            ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
                AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
                AND VS22.CodigoSecao            = SE.CodigoSecao
                AND VS22.Cargo                  = 5
                AND VS22.NumeroCandidato        = 22
        GROUP BY    M.UFSigla,
                    SE.MunicipioCodigo,
                    SE.CodigoZonaEleitoral,
                    SE.CodigoSecao,
                    VL.ModeloUrnaEletronica,
                    SE.PR_Total,
                    VS13.QtdVotos,
                    VS22.QtdVotos

    END

    -- Extrair da tabela a quantidade de votos do Lula e do Bolsonaro por modelo de urna
    SELECT      T.ModeloUrna, 
                FORMAT(CONVERT(int, SUM(T.QtdVotosLulaAj)), '#,###','pt-br') as QtdVotosLula, 
                FORMAT(CONVERT(int, SUM(T.QtdVotosBolsonaroAj)), '#,###','pt-br') as QtdVotosBolsonaro,
                FORMAT(CONVERT(int, SUM(T.QtdVotosTotalModelo)), '#,###','pt-br') as QtdVotosTotal,
                FORMAT(((SUM(T.QtdVotosLulaAj) / SUM(T.QtdVotosTotalAj)) * 100), '#,###.##','pt-br') as PercentualLula,
                FORMAT(((SUM(T.QtdVotosBolsonaroAj) / SUM(T.QtdVotosTotalAj)) * 100), '#,###.##','pt-br') as PercentualBolsonaro
    FROM        @tmpVersaoUrnaSecoes T
    GROUP BY    T.ModeloUrna
    ORDER BY    T.ModeloUrna

    -- Extrair da tabela a distribuição dos modelos de urna por UF.
    DECLARE @tmpVersaoUrnaUF TABLE (
        UFSigla             char(2),
        ModeloUrna          smallint,
        QtdVotosTotal       bigint,
        PercentualModelo    numeric(18,4)
    )

    INSERT INTO @tmpVersaoUrnaUF (UFSigla, ModeloUrna, QtdVotosTotal)
    SELECT      UF.Sigla,
                T.ModeloUrna,
                ISNULL(SUM(T.QtdVotosTotalModelo),0) as QtdVotos
    FROM        UnidadeFederativa UF with (NOLOCK)
    INNER JOIN  @tmpVersaoUrnaSecoes T ON T.UFSigla = UF.Sigla
    WHERE       UF.Sigla <> 'BR'
    GROUP BY    UF.Sigla, T.ModeloUrna
    ORDER BY    UF.Sigla, T.ModeloUrna

    UPDATE  T
    SET     PercentualModelo = (CONVERT(numeric(18,2), T.QtdVotosTotal) / (SELECT SUM(QtdVotosTotal) FROM @tmpVersaoUrnaUF TG WHERE TG.UFSigla = T.UFSigla )) * 100
    FROM    @tmpVersaoUrnaUF T 

    SELECT * FROM @tmpVersaoUrnaUF T ORDER BY T.UFSigla, T.ModeloUrna

    SELECT      UF.Sigla,
                FORMAT(ISNULL(T2009.QtdVotosTotal,0), '#,###','pt-br') as [2009],
                FORMAT(ISNULL(T2010.QtdVotosTotal,0), '#,###','pt-br') as [2010],
                FORMAT(ISNULL(T2011.QtdVotosTotal,0), '#,###','pt-br') as [2011],
                FORMAT(ISNULL(T2013.QtdVotosTotal,0), '#,###','pt-br') as [2013],
                FORMAT(ISNULL(T2015.QtdVotosTotal,0), '#,###','pt-br') as [2015],
                FORMAT(ISNULL(T2020.QtdVotosTotal,0), '#,###','pt-br') as [2020],
                FORMAT(ISNULL(T2009.PercentualModelo,0), '#,###.##','pt-br') as [P2009],
                FORMAT(ISNULL(T2010.PercentualModelo,0), '#,###.##','pt-br') as [P2010],
                FORMAT(ISNULL(T2011.PercentualModelo,0), '#,###.##','pt-br') as [P2011],
                FORMAT(ISNULL(T2013.PercentualModelo,0), '#,###.##','pt-br') as [P2013],
                FORMAT(ISNULL(T2015.PercentualModelo,0), '#,###.##','pt-br') as [P2015],
                FORMAT(ISNULL(T2020.PercentualModelo,0), '#,###.##','pt-br') as [P2020]
    FROM        UnidadeFederativa UF with (NOLOCK)
    LEFT JOIN   @tmpVersaoUrnaUF T2009 ON T2009.UFSigla = UF.Sigla AND T2009.ModeloUrna = 2009
    LEFT JOIN   @tmpVersaoUrnaUF T2010 ON T2010.UFSigla = UF.Sigla AND T2010.ModeloUrna = 2010
    LEFT JOIN   @tmpVersaoUrnaUF T2011 ON T2011.UFSigla = UF.Sigla AND T2011.ModeloUrna = 2011
    LEFT JOIN   @tmpVersaoUrnaUF T2013 ON T2013.UFSigla = UF.Sigla AND T2013.ModeloUrna = 2013
    LEFT JOIN   @tmpVersaoUrnaUF T2015 ON T2015.UFSigla = UF.Sigla AND T2015.ModeloUrna = 2015
    LEFT JOIN   @tmpVersaoUrnaUF T2020 ON T2020.UFSigla = UF.Sigla AND T2020.ModeloUrna = 2020
    WHERE       UF.Sigla <> 'BR'
    ORDER BY    UF.Sigla
    
    -- SELECT * FROM @tmpVersaoUrnaSecoes T ORDER BY T.UFSigla, T.CodMunicipio, T.CodZonaEleitoral, T.CodSecaoEleitoral, T.ModeloUrna

END

IF 0=1
BEGIN -- Relatório 7 - Obter as seções eleitorais que apresentam diferença na contagem de votações do log comparando com a votação do Boletim de Urna
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + ' - Votações no BU: ' + CONVERT(varchar(20), SE.PR_Total) + ', Votações no Log: ' + CONVERT(varchar(20), COUNT(*)) + '.'
    FROM        SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
--            AND M.UFSigla = 'AL'
    INNER JOIN  UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   VotosLog VLPR with (NOLOCK)
        ON      VLPR.MunicipioCodigo = SE.MunicipioCodigo
            AND VLPR.CodigoZonaEleitoral = SE.CodigoZonaEleitoral
            AND VLPR.CodigoSecao = SE.CodigoSecao
            AND VLPR.VotoComputado = 1
            AND (VLPR.VotouPR = 1 OR VLPR.VotoNuloSuspensaoPR = 1)
    WHERE       SE.ResultadoSistemaApuracao = 0
            AND VLPR.IdVotoLog IS NOT NULL
    GROUP BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.PR_Total
    HAVING      COUNT(*) <> SE.PR_Total AND COUNT(*) > 0
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao


    --SELECT * FROM VotosLog VL WHERE MunicipioCodigo = 99473 AND CodigoZonaEleitoral = 1 and CodigoSecao = 1327 AND VotoComputado = 1 AND VL.VotouPR = 0
    --SELECT * FROM SecaoEleitoral WHERE MunicipioCodigo = 99473 AND CodigoZonaEleitoral = 1 and CodigoSecao = 1327
END

IF 0=1
BEGIN -- Relatório 8 - Obter as seções que não possuem registro de voto

    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.' as Qry
    FROM        SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   VotosSecaoRDV RDV with (NOLOCK)
        ON      RDV.MunicipioCodigo = SE.MunicipioCodigo
            AND RDV.CodigoZonaEleitoral = SE.CodigoZonaEleitoral
            AND RDV.CodigoSecao = SE.CodigoSecao
    WHERE       RDV.IdVotoRDV IS NULL
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

END

IF 0=1
BEGIN -- Relatório 9 - Obter as seções que não possuem informação de Zerésima

    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.' as Qry
    FROM        SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       SE.Zeresima = '0001-01-01'
            AND SE.ResultadoSistemaApuracao = 0
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

END

IF 1=1
BEGIN -- Relatório 10 - Seções com votos computados antes da abertura da urna

    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.' as Qry, MIN(VLPR.InicioVoto) as InicioVoto, SE.AberturaUrnaEletronica, SE.ResultadoSistemaApuracao
    FROM        SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    INNER JOIN   VotosLog VLPR with (NOLOCK)
        ON      VLPR.MunicipioCodigo = SE.MunicipioCodigo
            AND VLPR.CodigoZonaEleitoral = SE.CodigoZonaEleitoral
            AND VLPR.CodigoSecao = SE.CodigoSecao
            AND VLPR.VotoComputado = 1
    WHERE       VLPR.InicioVoto < SE.AberturaUrnaEletronica
            AND SE.ResultadoSistemaApuracao = 0
    GROUP BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.AberturaUrnaEletronica,
                SE.ResultadoSistemaApuracao
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

END



-- Votos Para Lula e Bolsonaro, por Região e por Modelo de Urna
SELECT      R.Nome as Regiao,
            SE.ModeloUrnaEletronica as ModeloUrna,
            SUM(CONVERT(bigint, VS13.QtdVotos)) as QtdVotos13,
            SUM(CONVERT(bigint, VS22.QtdVotos)) as QtdVotos22,
--            SUM(SE.PR_VotosNominais) as PR_VotosNominais,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS13.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual13,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS22.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual22
FROM        Regiao                      R   with (NOLOCK)
INNER JOIN  UnidadeFederativa           UF  with (NOLOCK)
    ON      UF.RegiaoId                 = R.Id
INNER JOIN  Municipio                   M   with (NOLOCK)
    ON      M.UFSigla                   = UF.Sigla
INNER JOIN  SecaoEleitoral              SE  with (NOLOCK)
    ON      SE.MunicipioCodigo          = M.Codigo
        AND SE.ModeloUrnaEletronica     <> 0
LEFT JOIN   VotosSecao                  VS13    with (NOLOCK)
    ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS13.CodigoSecao            = SE.CodigoSecao
        AND VS13.Cargo                  = 5
        AND VS13.NumeroCandidato        = 13
LEFT JOIN   VotosSecao                  VS22    with (NOLOCK)
    ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS22.CodigoSecao            = SE.CodigoSecao
        AND VS22.Cargo                  = 5
        AND VS22.NumeroCandidato        = 22
WHERE       R.ID        > 0
GROUP BY    R.Nome, SE.ModeloUrnaEletronica
ORDER BY    SE.ModeloUrnaEletronica, R.Nome



SELECT      R.Nome as Regiao,
            CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END as ModeloUrna,
            SUM(CONVERT(bigint, VS13.QtdVotos)) as QtdVotos13,
            SUM(CONVERT(bigint, VS22.QtdVotos)) as QtdVotos22,
--            SUM(SE.PR_VotosNominais) as PR_VotosNominais,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS13.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual13,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS22.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual22
FROM        Regiao                      R   with (NOLOCK)
INNER JOIN  UnidadeFederativa           UF  with (NOLOCK)
    ON      UF.RegiaoId                 = R.Id
INNER JOIN  Municipio                   M   with (NOLOCK)
    ON      M.UFSigla                   = UF.Sigla
INNER JOIN  SecaoEleitoral              SE  with (NOLOCK)
    ON      SE.MunicipioCodigo          = M.Codigo
        AND SE.ModeloUrnaEletronica     <> 0
LEFT JOIN   VotosSecao                  VS13    with (NOLOCK)
    ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS13.CodigoSecao            = SE.CodigoSecao
        AND VS13.Cargo                  = 5
        AND VS13.NumeroCandidato        = 13
LEFT JOIN   VotosSecao                  VS22    with (NOLOCK)
    ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS22.CodigoSecao            = SE.CodigoSecao
        AND VS22.Cargo                  = 5
        AND VS22.NumeroCandidato        = 22
WHERE       R.ID        BETWEEN 1 and 5
GROUP BY    R.Nome, CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END
ORDER BY    R.Nome, CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END




SELECT      --R.Nome as Regiao,
            CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END as ModeloUrna,
            SUM(CONVERT(bigint, VS13.QtdVotos)) as QtdVotos13,
            SUM(CONVERT(bigint, VS22.QtdVotos)) as QtdVotos22,
--            SUM(SE.PR_VotosNominais) as PR_VotosNominais,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS13.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual13,
            CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(CONVERT(bigint, VS22.QtdVotos))) / CONVERT(numeric(18,2), SUM(SE.PR_VotosNominais))) * 100.0) as Percentual22
FROM        Regiao                      R   with (NOLOCK)
INNER JOIN  UnidadeFederativa           UF  with (NOLOCK)
    ON      UF.RegiaoId                 = R.Id
INNER JOIN  Municipio                   M   with (NOLOCK)
    ON      M.UFSigla                   = UF.Sigla
INNER JOIN  SecaoEleitoral              SE  with (NOLOCK)
    ON      SE.MunicipioCodigo          = M.Codigo
        AND SE.ModeloUrnaEletronica     <> 0
LEFT JOIN   VotosSecao                  VS13    with (NOLOCK)
    ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS13.CodigoSecao            = SE.CodigoSecao
        AND VS13.Cargo                  = 5
        AND VS13.NumeroCandidato        = 13
LEFT JOIN   VotosSecao                  VS22    with (NOLOCK)
    ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
        AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
        AND VS22.CodigoSecao            = SE.CodigoSecao
        AND VS22.Cargo                  = 5
        AND VS22.NumeroCandidato        = 22
WHERE       R.ID        BETWEEN 1 and 5
GROUP BY    CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END
ORDER BY    CASE SE.ModeloUrnaEletronica WHEN 2020 THEN 'Novas (2020)' ELSE 'Antigas' END




SELECT 12486.0 / 44624.0,
        32138.0 / 44624.0
