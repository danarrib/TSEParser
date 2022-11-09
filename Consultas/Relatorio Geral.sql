--SELECT * FROM UnidadeFederativa
SET NOCOUNT ON

DECLARE @Turno tinyint;
SELECT @Turno = CASE WHEN DB_NAME() = 'TSEParser_T1' THEN 1 ELSE 2 END

DECLARE @UFSigla    char(2),
        @AuxInt     int,
        @AuxInt2    int,
        @AuxVarchar varchar(200);

IF 1=1
BEGIN -- Relat�rio 1 - Sess�es eleitorais com o maior percentual de votos para Lula (na Compara��o Lula/Bolsonaro)
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

    -- Contar quantas se��es tiveram 0 votos para Lula ou para Bolsonaro
    SELECT @AuxInt = COUNT(*) FROM @tmpVotosPorSecaoLulaBolsonaro WHERE QtdVotosLula = 0
    PRINT '#### Quantidade de Se��es eleitorais que n�o tiveram votos para o Lula: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
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
    PRINT '#### Quantidade de Se��es eleitorais que n�o tiveram votos para o Bolsonaro: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
    DECLARE C1 CURSOR FOR
        SELECT '- UF ' + T.UFSigla + ' (' + U.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), T.CodMunicipio), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), T.CodZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), T.CodSecaoEleitoral), 4)
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
    PRINT '#### Quantidade de votos para o Bolsonaro nas se��es eleitorais que n�o tiveram votos para o Lula: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
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
    PRINT '#### Quantidade de votos para o Lula nas se��es eleitorais que n�o tiveram votos para o Bolsonaro: ' + FORMAT(@AuxInt, '#,###', 'pt-br') + '.'
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
BEGIN -- Relat�rio 2 - Contagem de se��es eleitorais por UF, e votos por se��o eleitoral (comparando com a informa��o dispon�vel no site do TSE)
    PRINT '### Compara��o da carga dos arquivos do TSE com o n�mero informado no site do TSE.'
    
    DECLARE @DadosTSE TABLE(
        UFSigla             char(2),
        QtdSecoes           int,
        QtdVotosPresidente  int
    )

    IF EXISTS(SELECT 1 FROM @DadosTSE T LEFT JOIN UnidadeFederativa UF ON UF.Sigla = T.UFSigla WHERE UF.Sigla IS NULL)
    BEGIN
        PRINT 'Existem UFs que ainda n�o foram carregadas'
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
            PRINT '- UF ' + @UFSigla + ' - Quantidade de se��es eleitorais carregadas (' + CONVERT(varchar(20), @AuxInt) + ') � diferente do TSE (' + CONVERT(varchar(20), @AuxInt2) + ').'
        END

        SELECT      @AuxInt = SUM(CONVERT(int, SE.PR_Total))
        FROM        SecaoEleitoral SE with (NOLOCK) 
        INNER JOIN  Municipio M with (NOLOCK)
            ON      M.Codigo = SE.MunicipioCodigo
                AND M.UFSigla = @UFSigla

        SELECT @AuxInt2 = QtdVotosPresidente FROM @DadosTSE WHERE UFSigla = @UFSigla

        IF @AuxInt <> @AuxInt2
        BEGIN
            PRINT '- UF ' + @UFSigla + ' - Quantidade de votos v�lidos para presidente carregados (' + CONVERT(varchar(20), @AuxInt) + ') � diferente do TSE (' + CONVERT(varchar(20), @AuxInt2) + ').'
        END
    END
END

IF 0=1
BEGIN -- Relat�rio 3 - C�digos de identifica��o de urna eletr�nica repetidos
    PRINT '### Se��es eleitorais com C�digos de Identifica��o de Urna Eletr�nica repetidos'

    SELECT @AuxInt = COUNT(*) FROM (
        SELECT      CodigoIdentificacaoUrnaEletronica
        FROM        SecaoEleitoral with (NOLOCK)
        GROUP BY    CodigoIdentificacaoUrnaEletronica
        HAVING      COUNT(*) > 1
    ) as T

    PRINT 'Existem ' + CONVERT(varchar(20), @AuxInt) + ' C�digos de Identifica��o de Urna Eletr�nica que se repetem para duas ou mais se��es eleitorais. Cada urna n�o deveria ter seu pr�prio n�mero de s�rie �nico?'

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
        PRINT '#### C�digo Identificador de Urna Eletr�nica: ' + CONVERT(varchar(20), @AuxInt) + ' - Quantidade de ocorr�ncias: ' + CONVERT(varchar(20), @AuxInt2) + '.'

        DECLARE C2 CURSOR FOR
        SELECT      '- ' + UF.Sigla + '(' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto
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
BEGIN -- Relat�rio 4 - Se��es eleitorais que tiveram as maiores mudan�as de lado (Viraram de Bolsonaro para Lula e Vice-versa)

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
    PRINT '### Munic�pios que viraram votos para o Lula do primeiro para o segundo turno: ' + CONVERT(varchar(20), @AuxInt) + '.'

    PRINT '#### Lista dos munic�pios que viraram (a partir de 10 mil votos)'
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
    PRINT '### Munic�pios que viraram votos para o Bolsonaro do primeiro para o segundo turno: ' + CONVERT(varchar(20), @AuxInt) + '.'

    PRINT '#### Lista dos munic�pios que viraram (a partir de 10 mil votos)'
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



-- Relat�rio 5 - Votos para candidados por vers�o de urna
