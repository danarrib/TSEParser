-- Gerar o Relatório de Defeitos de carga
SET NOCOUNT ON

DECLARE @AuxVarchar varchar(1000),
        @UFSigla    char(2),
        @AuxInt     int,
        @AuxInt2    int,
        @AuxInt3    int;

DECLARE @Relatorio TABLE (
    Linha           int NOT NULL IDENTITY(1,1),
    Texto           varchar(1000) NOT NULL,
    TipoRelatorio   tinyint NOT NULL,
    Turno           tinyint NOT NULL
);


IF 1=1
BEGIN -- Relatórios 1 e 2 - Obter as seções eleitorais que apresentam diferença na contagem de votações do log comparando com a votação do Boletim de Urna

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + ' - Votações no BU: ' + CONVERT(varchar(20), SE.PR_Total) + ', Votações no Log: ' + CONVERT(varchar(20), COUNT(*)) + '.',
                CASE WHEN SE.PR_Total > COUNT(*) THEN 1 ELSE 2 END as TipoRelatorio,
                1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   TSEParser_T1..VotosLog VLPR with (NOLOCK)
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

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + ' - Votações no BU: ' + CONVERT(varchar(20), SE.PR_Total) + ', Votações no Log: ' + CONVERT(varchar(20), COUNT(*)) + '.',
                CASE WHEN SE.PR_Total > COUNT(*) THEN 1 ELSE 2 END as TipoRelatorio,
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   TSEParser_T2..VotosLog VLPR with (NOLOCK)
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

END

IF 1=1
BEGIN -- Relatório 3 - Contagem de seções eleitorais por UF, e votos por seção eleitoral (comparando com a informação disponível no site do TSE)
    DECLARE @DadosTSE TABLE(
        UFSigla             char(2),
        QtdSecoes           int,
        QtdVotosPresidente  int,
        Turno               tinyint
    )

    INSERT INTO @DadosTSE SELECT 'AC', 2124, 455903, 1
    INSERT INTO @DadosTSE SELECT 'AL', 6626, 1805971, 1
    INSERT INTO @DadosTSE SELECT 'AP', 1740, 442842, 1
    INSERT INTO @DadosTSE SELECT 'AM', 7454, 2113771, 1
    INSERT INTO @DadosTSE SELECT 'BA', 34424, 8874841, 1
    INSERT INTO @DadosTSE SELECT 'CE', 22796, 5628610, 1
    INSERT INTO @DadosTSE SELECT 'DF', 6748, 1819900, 1
    INSERT INTO @DadosTSE SELECT 'ES', 9239, 2315889, 1
    INSERT INTO @DadosTSE SELECT 'GO', 14620, 3812597, 1
    INSERT INTO @DadosTSE SELECT 'MA', 16423, 3920435, 1
    INSERT INTO @DadosTSE SELECT 'MT', 7652, 1892180, 1
    INSERT INTO @DadosTSE SELECT 'MS', 6912, 1555149, 1
    INSERT INTO @DadosTSE SELECT 'MG', 49981, 12655228, 1
    INSERT INTO @DadosTSE SELECT 'PA', 18235, 4789311, 1
    INSERT INTO @DadosTSE SELECT 'PB', 9602, 2557479, 1
    INSERT INTO @DadosTSE SELECT 'PR', 25721, 6828543, 1
    INSERT INTO @DadosTSE SELECT 'PE', 20572, 5738371, 1
    INSERT INTO @DadosTSE SELECT 'PI', 8963, 2115645, 1
    INSERT INTO @DadosTSE SELECT 'RJ', 34068, 9909463, 1
    INSERT INTO @DadosTSE SELECT 'RN', 7674, 2090604, 1
    INSERT INTO @DadosTSE SELECT 'RS', 27201, 6890016, 1
    INSERT INTO @DadosTSE SELECT 'RO', 4198, 926827, 1
    INSERT INTO @DadosTSE SELECT 'RR', 1268, 305404, 1
    INSERT INTO @DadosTSE SELECT 'SC', 16242, 4487474, 1
    INSERT INTO @DadosTSE SELECT 'SP', 101073, 27189714, 1
    INSERT INTO @DadosTSE SELECT 'SE', 5498, 1364724, 1
    INSERT INTO @DadosTSE SELECT 'TO', 3957, 891449, 1
    INSERT INTO @DadosTSE SELECT 'ZZ', 1064, 304032, 1

    INSERT INTO @DadosTSE SELECT 'AC', 2124, 420760, 2
    INSERT INTO @DadosTSE SELECT 'AL', 6626, 1784988, 2
    INSERT INTO @DadosTSE SELECT 'AP', 1740, 400683, 2
    INSERT INTO @DadosTSE SELECT 'AM', 7454, 2067875, 2
    INSERT INTO @DadosTSE SELECT 'BA', 34424, 8971728, 2
    INSERT INTO @DadosTSE SELECT 'CE', 22796, 5649398, 2
    INSERT INTO @DadosTSE SELECT 'DF', 6748, 1838492, 2
    INSERT INTO @DadosTSE SELECT 'ES', 9239, 2322269, 2
    INSERT INTO @DadosTSE SELECT 'GO', 14620, 3860351, 2
    INSERT INTO @DadosTSE SELECT 'MA', 16423, 3854804, 2
    INSERT INTO @DadosTSE SELECT 'MT', 7652, 1913231, 2
    INSERT INTO @DadosTSE SELECT 'MS', 6912, 1549873, 2
    INSERT INTO @DadosTSE SELECT 'MG', 49981, 12866284, 2
    INSERT INTO @DadosTSE SELECT 'PA', 18235, 4701740, 2
    INSERT INTO @DadosTSE SELECT 'PB', 9602, 2574215, 2
    INSERT INTO @DadosTSE SELECT 'PR', 25721, 6900420, 2
    INSERT INTO @DadosTSE SELECT 'PE', 20572, 5800735, 2
    INSERT INTO @DadosTSE SELECT 'PI', 8963, 2088530, 2
    INSERT INTO @DadosTSE SELECT 'RJ', 34068, 9973822, 2
    INSERT INTO @DadosTSE SELECT 'RN', 7674, 2108799, 2
    INSERT INTO @DadosTSE SELECT 'RS', 27201, 6930852, 2
    INSERT INTO @DadosTSE SELECT 'RO', 4198, 926517, 2
    INSERT INTO @DadosTSE SELECT 'RR', 1268, 286269, 2
    INSERT INTO @DadosTSE SELECT 'SC', 16242, 4542817, 2
    INSERT INTO @DadosTSE SELECT 'SP', 101073, 27380491, 2
    INSERT INTO @DadosTSE SELECT 'SE', 5498, 1355467, 2
    INSERT INTO @DadosTSE SELECT 'TO', 3957, 871238, 2
    INSERT INTO @DadosTSE SELECT 'ZZ', 1064, 310148, 2

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de seções eleitorais carregadas (' + CONVERT(varchar(20), COUNT(*)) + ') é diferente do TSE (' + CONVERT(varchar(20), T.QtdSecoes) + ').',
                3 as TipoRelatorio, 
                1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral SE with (NOLOCK) 
    INNER JOIN  TSEParser_T1..Municipio M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  @DadosTSE T
        ON      T.UFSigla = M.UFSigla
            AND T.Turno = 1
    GROUP BY    M.UFSigla,
                T.QtdSecoes
    HAVING      T.QtdSecoes <> COUNT(*)
    ORDER BY    M.UFSigla

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de seções eleitorais carregadas (' + CONVERT(varchar(20), COUNT(*)) + ') é diferente do TSE (' + CONVERT(varchar(20), T.QtdSecoes) + ').',
                3 as TipoRelatorio, 
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral SE with (NOLOCK) 
    INNER JOIN  TSEParser_T2..Municipio M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  @DadosTSE T
        ON      T.UFSigla = M.UFSigla
            AND T.Turno = 2
    GROUP BY    M.UFSigla,
                T.QtdSecoes
    HAVING      T.QtdSecoes <> COUNT(*)
    ORDER BY    M.UFSigla

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de votos válidos carregados (' + CONVERT(varchar(20), SUM(CONVERT(int, SE.PR_Total))) + ') é diferente do TSE (' + CONVERT(varchar(20), T.QtdVotosPresidente) + ').',
                3 as TipoRelatorio, 
                1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral SE with (NOLOCK) 
    INNER JOIN  TSEParser_T1..Municipio M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  @DadosTSE T
        ON      T.UFSigla = M.UFSigla
            AND T.Turno = 1
    GROUP BY    M.UFSigla,
                T.QtdVotosPresidente
    HAVING      T.QtdVotosPresidente <> SUM(CONVERT(int, SE.PR_Total))
    ORDER BY    M.UFSigla

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de votos válidos carregados (' + CONVERT(varchar(20), SUM(CONVERT(int, SE.PR_Total))) + ') é diferente do TSE (' + CONVERT(varchar(20), T.QtdVotosPresidente) + ').',
                3 as TipoRelatorio, 
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral SE with (NOLOCK) 
    INNER JOIN  TSEParser_T2..Municipio M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  @DadosTSE T
        ON      T.UFSigla = M.UFSigla
            AND T.Turno = 2
    GROUP BY    M.UFSigla,
                T.QtdVotosPresidente
    HAVING      T.QtdVotosPresidente <> SUM(CONVERT(int, SE.PR_Total))
    ORDER BY    M.UFSigla

END

IF 1=1
BEGIN -- Relatório 4 - Códigos de identificação de urna eletrônica repetidos
    DECLARE C1 CURSOR FOR
    SELECT      CodigoIdentificacaoUrnaEletronica, 
                COUNT(*) as QtdRepetidos
    FROM        TSEParser_T1..SecaoEleitoral with (NOLOCK)
    GROUP BY    CodigoIdentificacaoUrnaEletronica
    HAVING      COUNT(*) > 1
    ORDER BY    COUNT(*) DESC

    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO @Relatorio
        SELECT '- Código Identificador de Urna Eletrônica: **' + CONVERT(varchar(20), @AuxInt) + '** - Quantidade de ocorrências: **' + CONVERT(varchar(20), @AuxInt2) + '**.' + CHAR(13),
        4 as TipoRelatorio, 1 as Turno

        INSERT INTO @Relatorio
        SELECT      '  - UF ' + UF.Sigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto,
        4 as TipoRelatorio, 1 as Turno
        FROM        TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        INNER JOIN  TSEParser_T1..Municipio M with (NOLOCK) ON M.Codigo = SE.MunicipioCodigo
        INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK) ON UF.Sigla = M.UFSigla
        WHERE       SE.CodigoIdentificacaoUrnaEletronica = @AuxInt
        ORDER BY    UF.Sigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

        FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    END
    CLOSE C1
    DEALLOCATE C1

    DECLARE C1 CURSOR FOR
    SELECT      CodigoIdentificacaoUrnaEletronica, 
                COUNT(*) as QtdRepetidos
    FROM        TSEParser_T2..SecaoEleitoral with (NOLOCK)
    GROUP BY    CodigoIdentificacaoUrnaEletronica
    HAVING      COUNT(*) > 1
    ORDER BY    COUNT(*) DESC

    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO @Relatorio
        SELECT '- Código Identificador de Urna Eletrônica: **' + CONVERT(varchar(20), @AuxInt) + '** - Quantidade de ocorrências: **' + CONVERT(varchar(20), @AuxInt2) + '**.' + CHAR(13),
        4 as TipoRelatorio, 2 as Turno

        INSERT INTO @Relatorio
        SELECT      '  - ' + UF.Sigla + '(' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto,
        4 as TipoRelatorio, 2 as Turno
        FROM        TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        INNER JOIN  TSEParser_T2..Municipio M with (NOLOCK) ON M.Codigo = SE.MunicipioCodigo
        INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK) ON UF.Sigla = M.UFSigla
        WHERE       SE.CodigoIdentificacaoUrnaEletronica = @AuxInt
        ORDER BY    UF.Sigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

        FETCH NEXT FROM C1 INTO @AuxInt, @AuxInt2
    END
    CLOSE C1
    DEALLOCATE C1
END

IF 1=1
BEGIN -- Relatório 5 - Obter as seções que não possuem registro de voto
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.',
                5 as TipoRelatorio,
                1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   TSEParser_T1..VotosSecaoRDV RDV with (NOLOCK)
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

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.',
                5 as TipoRelatorio,
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    LEFT JOIN   TSEParser_T2..VotosSecaoRDV RDV with (NOLOCK)
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

IF 1=1
BEGIN -- Relatório 6 - Obter as seções que não possuem informação de Zerésima
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.', 6 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       SE.Zeresima = '0001-01-01'
            AND SE.ResultadoSistemaApuracao = 0
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.', 6 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
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
BEGIN -- Relatório 7 - Seções com votos computados antes da abertura da urna
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '. Abertura da urna: ' + FORMAT(SE.AberturaUrnaEletronica, 'dd/MM/yyyy HH:mm:ss') + ', Primeiro voto: ' + FORMAT(MIN(VLPR.InicioVoto), 'dd/MM/yyyy HH:mm:ss') +  '.',
                7 as TipoRel, 1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    INNER JOIN  TSEParser_T1..VotosLog VLPR with (NOLOCK)
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

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '. Data/hora da abertura da urna: ' + FORMAT(SE.AberturaUrnaEletronica, 'dd/MM/yyyy HH:mm:ss') + ', Data/hora do primeiro voto: ' + FORMAT(MIN(VLPR.InicioVoto), 'dd/MM/yyyy HH:mm:ss') +  '.',
                7 as TipoRel, 2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    INNER JOIN  TSEParser_T2..VotosLog VLPR with (NOLOCK)
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

IF 1=1
BEGIN -- Relatório 8 - Zerésima realizada mais de duas horas antes da abertura da urna
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '. Abertura: ' + FORMAT(SE.AberturaUrnaEletronica, 'dd/MM/yyyy HH:mm:ss') + ', Zerésima: ' + FORMAT(SE.Zeresima, 'dd/MM/yyyy HH:mm:ss') + '.', 8 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       SE.Zeresima <> '0001-01-01'
            AND SE.ResultadoSistemaApuracao = 0
            AND SE.Zeresima < DATEADD(hh, -2, SE.AberturaUrnaEletronica)
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 8, 1
    END
    
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '.', 8 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       SE.Zeresima <> '0001-01-01'
            AND SE.ResultadoSistemaApuracao = 0
            AND SE.Zeresima < DATEADD(hh, -2, SE.AberturaUrnaEletronica)
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso.', 8, 2
    END
END



PRINT '# Defeitos nos arquivos do TSE

**Este relatório ainda está sendo atualizado - Não utilizar estes dados para propósitos oficiais**

Explicação

## Diferença em quantidade de votos e quantidade de seções dos arquivos do TSE com o número informado no site do TSE

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 3 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 3 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Mais votos no Boletim de Urna do que no Log da Urna

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 1 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 1 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '## Mais votos no Log da Urna do que no Boletim de Urna

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 2 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 2 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1
    
PRINT '
## Sem arquivos

Explicação

### Primeiro Turno

### Segundo Turno

## Arquivos Excluídos

Explicação

### Primeiro Turno

### Segundo Turno

## Arquivos Rejeitados

Explicação

### Primeiro Turno

### Segundo Turno

## Não há registro de votos

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 5 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 5 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Não há informação de Zerésima

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 6 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 6 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Códigos de Identificação da Urna Eletrônica repetidos

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 4 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 4 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Códigos de Identificação da Urna Eletrônica são diferentes no IMGBU e no BU

Explicação

### Primeiro Turno

### Segundo Turno

## Votos computados antes do início da votação

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 7 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 7 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Zerésima realizada mais de duas horas antes da abertura da Urna

Explicação

### Primeiro Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 8 AND Turno = 1 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
### Segundo Turno

'
    DECLARE C1 CURSOR FOR
        SELECT Texto FROM @Relatorio WHERE TipoRelatorio = 8 AND Turno = 2 ORDER BY Linha
    OPEN C1
    FETCH NEXT FROM C1 INTO @AuxVarchar
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @AuxVarchar
        FETCH NEXT FROM C1 INTO @AuxVarchar
    END
    CLOSE C1
    DEALLOCATE C1

PRINT '
## Não há arquivo IMGBU

Explicação

### Primeiro Turno

### Segundo Turno

## O Boletim de Urna (arquivo BU) está corrompido

Explicação

### Primeiro Turno

### Segundo Turno

## O Registro de Votos (arquivo RDV) está corrompido

Explicação

### Primeiro Turno

### Segundo Turno

## Imagem do Boletim de Urna (arquivo IMGBU) completamente diferente do Boletim de Urna (arquivo BU)

Explicação

### Primeiro Turno

### Segundo Turno

'


