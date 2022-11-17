-- Gerar o Relatório de Defeitos de carga
SET NOCOUNT ON

DECLARE @AuxVarchar             varchar(1000),
        @UFSigla                char(2),
        @AuxInt                 int,
        @AuxInt2                int,
        @AuxInt3                int,
        @NumRelatorio           int,
        @Turno                  tinyint,
        @InicioProcessamento    datetime2,
        @IniciarDetalhes        varchar(100),
        @FinalizarDetalhes      varchar(100),
        @PrimeiroTurno          varchar(100),
        @SegundoTurno           varchar(100);

SET @InicioProcessamento = GETDATE();
SET @IniciarDetalhes = '<details>
    <summary>Expandir lista</summary>

';
SET @FinalizarDetalhes = '
</details>';
SET @PrimeiroTurno = '
### Primeiro Turno

';
SET @SegundoTurno = '
### Segundo Turno

';
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

    PRINT 'Relatórios 1 e 2 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    PRINT 'Relatório 3 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    PRINT 'Relatório 4 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 5, 1
    END

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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 5, 2
    END

    PRINT 'Relatório 5 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 6, 1
    END

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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 6, 2
    END

    PRINT 'Relatório 6 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 7, 1
    END

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

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 7, 2
    END

    PRINT 'Relatório 7 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
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

    PRINT 'Relatório 8 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 9 - Sem arquivos, arquivos excluídos ou arquivos rejeitados
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Motivo: ' + CASE WHEN DS.SemArquivo = 1 THEN 'Sem arquivo' WHEN DS.Excluido = 1 THEN 'Excluído' WHEN DS.Rejeitado = 1 THEN 'Rejeitado' ELSE 'Desconhecido' END + '.', 
                9 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       (DS.SemArquivo = 1 OR DS.Excluido = 1 OR DS.Rejeitado = 1)
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 9, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Motivo: ' + CASE WHEN DS.SemArquivo = 1 THEN 'Sem arquivo' WHEN DS.Excluido = 1 THEN 'Excluído' WHEN DS.Rejeitado = 1 THEN 'Rejeitado' ELSE 'Desconhecido' END + '.', 
                9 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       (DS.SemArquivo = 1 OR DS.Excluido = 1 OR DS.Rejeitado = 1)
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 9, 2
    END

    PRINT 'Relatório 9 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 10 - Código de Identificação da UE diferente no BU e no IMGBU
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Código no BU: ' + CONVERT(varchar(20), DS.CodigoIdentificacaoUrnaEletronicaBU) + ' - Código no IMGBU: ' + CONVERT(varchar(20), SE.CodigoIdentificacaoUrnaEletronica) + '.', 
                10 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.CodigoIdentificacaoUrnaEletronicaBU <> 0
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 10, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Código no BU: ' + CONVERT(varchar(20), DS.CodigoIdentificacaoUrnaEletronicaBU) + ' - Código no IMGBU: ' + CONVERT(varchar(20), SE.CodigoIdentificacaoUrnaEletronica) + '.', 
                10 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.CodigoIdentificacaoUrnaEletronicaBU <> 0
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 10, 2
    END

    PRINT 'Relatório 10 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 11 - Arquivo IMGBU Faltando
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                11 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoIMGBUFaltando = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 11, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                11 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoIMGBUFaltando = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 11, 2
    END
    
    PRINT 'Relatório 11 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 12 - Arquivo BU Corrompido
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                12 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoBUCorrompido = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 12, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                12 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoBUCorrompido = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 12, 2
    END

    PRINT 'Relatório 12 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 13 - Arquivo RDV Corrompido
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                13 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoRDVCorrompido = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 13, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                13 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoRDVCorrompido = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 13, 2
    END

    PRINT 'Relatório 13 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 14 - Diferença de votos entre BU e IMGBU
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                14 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.DiferencaVotosBUeIMGBU = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 14, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                14 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.DiferencaVotosBUeIMGBU = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 14, 2
    END

    PRINT 'Relatório 14 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 15 - Arquivo BU Faltando
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                15 as TipoRelatorio, 1 as Turno
    FROM        TSEParser_T1..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T1..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoBUFaltando = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 15, 1
    END

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Município ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '.', 
                15 as TipoRelatorio, 2 as Turno
    FROM        TSEParser_T2..DefeitosSecao  DS with (NOLOCK)
    INNER JOIN  TSEParser_T2..SecaoEleitoral SE with (NOLOCK)
        ON      SE.MunicipioCodigo  = DS.MunicipioCodigo
            AND SE.CodigoZonaEleitoral = DS.CodigoZonaEleitoral
            AND SE.CodigoSecao = DS.CodigoSecao
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = DS.MunicipioCodigo
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with (NOLOCK)
        ON      UF.Sigla = M.UFSigla
    WHERE       DS.ArquivoBUFaltando = 1
    ORDER BY    M.UFSigla,
                UF.Nome,
                M.Codigo,
                M.Nome,
                DS.CodigoZonaEleitoral,
                DS.CodigoSecao

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO @Relatorio SELECT '- Nenhum caso', 15, 2
    END
    
    PRINT 'Relatório 15 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relatório 16 - Seções eleitorais que receberam votos por mais de 9 horas.
    DECLARE @TempoVotos TABLE (
        Id int NOT NULL IDENTITY(1,1),
        UFSigla char(2),
        CodMunicipio int,
        CodZona smallint,
        CodSecao smallint,
        Abertura datetime2,
        PrimeiroVoto datetime2,
        Fechamento datetime2,
        UltimoVoto datetime2,
        DuracaoVotacao smallint,
        Turno tinyint
    )

    INSERT INTO @TempoVotos
    SELECT      M.UFSigla,
                M.Codigo as CodMunicipio,
                SE.CodigoZonaEleitoral as CodZona,
                SE.CodigoSecao as CodSecao,
                SE.AberturaUrnaEletronica as Abertura, 
                MIN(VLPR.InicioVoto) as PrimeiroVoto, 
                SE.FechamentoUrnaEletronica as Fechamento, 
                MAX(VLPR.InicioVoto) UltimoVoto, 
                DATEDIFF(MI, MIN(VLPR.InicioVoto), MAX(VLPR.InicioVoto)) as DuracaoVotacao,
                1 as Turno
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..VotosLog VLPR with (NOLOCK)
        ON      VLPR.MunicipioCodigo = SE.MunicipioCodigo
            AND VLPR.CodigoZonaEleitoral = SE.CodigoZonaEleitoral
            AND VLPR.CodigoSecao = SE.CodigoSecao
            AND VLPR.VotoComputado = 1
    WHERE       SE.ResultadoSistemaApuracao = 0
    GROUP BY    M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.AberturaUrnaEletronica,
                SE.FechamentoUrnaEletronica
    HAVING      DATEDIFF(MI, MIN(VLPR.InicioVoto), MAX(VLPR.InicioVoto)) > (9 * 60)
    ORDER BY    M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao

    INSERT INTO @TempoVotos
    SELECT      M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.AberturaUrnaEletronica, 
                MIN(VLPR.InicioVoto) as PrimeiroVoto, 
                SE.FechamentoUrnaEletronica, 
                MAX(VLPR.InicioVoto) UltimoVoto, 
                DATEDIFF(MI, MIN(VLPR.InicioVoto), MAX(VLPR.InicioVoto)) as DuracaoVotacaoMinutos,
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio   M with (NOLOCK)
        ON      M.Codigo = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..VotosLog VLPR with (NOLOCK)
        ON      VLPR.MunicipioCodigo = SE.MunicipioCodigo
            AND VLPR.CodigoZonaEleitoral = SE.CodigoZonaEleitoral
            AND VLPR.CodigoSecao = SE.CodigoSecao
            AND VLPR.VotoComputado = 1
    WHERE       SE.ResultadoSistemaApuracao = 0
    GROUP BY    M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.AberturaUrnaEletronica,
                SE.FechamentoUrnaEletronica
    HAVING      DATEDIFF(MI, MIN(VLPR.InicioVoto), MAX(VLPR.InicioVoto)) > (9 * 60)
    ORDER BY    M.UFSigla,
                M.Codigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao


    INSERT INTO @Relatorio
    SELECT      '| UF | 9 - 10 horas | 10 - 11 horas | 11 - 12 horas | + 12 horas |', 16, 1

    INSERT INTO @Relatorio
    SELECT      '| --- | ---: | ---: | ---: | ---: |', 16, 1

    INSERT INTO @Relatorio
    SELECT      '| ' + UF.Sigla + ' (' + UF.Nome + ') ' + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (9 * 60) AND (10 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (10 * 60) AND (11 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (11 * 60) AND (12 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao > (12 * 60) AND T1.Turno = 1)) +
                ' |', 16, 1
    FROM        TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
    WHERE       UF.Sigla <> 'BR'
    ORDER BY    UF.Sigla
    
    INSERT INTO @Relatorio
    SELECT      '| UF | 9 - 10 horas | 10 - 11 horas | 11 - 12 horas | + 12 horas |', 16, 2

    INSERT INTO @Relatorio
    SELECT      '| --- | ---: | ---: | ---: | ---: |', 16, 2

    INSERT INTO @Relatorio
    SELECT      '| ' + UF.Sigla + ' (' + UF.Nome + ') ' + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (9 * 60) AND (10 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (10 * 60) AND (11 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao BETWEEN (11 * 60) AND (12 * 60) AND T1.Turno = 1)) + 
                ' | ' + CONVERT(varchar(20), (SELECT COUNT(*) FROM @TempoVotos T1 WHERE T1.UFSigla = UF.Sigla AND T1.DuracaoVotacao > (12 * 60) AND T1.Turno = 1)) +
                ' |', 16, 2
    FROM        TSEParser_T1..UnidadeFederativa UF with (NOLOCK)
    WHERE       UF.Sigla <> 'BR'
    ORDER BY    UF.Sigla

    PRINT 'Relatório 16 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END


PRINT '# Defeitos nos arquivos do TSE

**Este relatório ainda está sendo atualizado - Não utilizar estes dados para propósitos oficiais**

Os arquivos disponibilizados pelo TSE apresentam alguns defeitos, que serão relacionados a seguir. As Urnas Eletrônicas produzem vários tipos diferentes de arquivos. Para o contexto desta análise, utilizamos apenas tipos de arquivos. São eles:
- Arquivo IMGBU (ou IMGBUSA) - É a **imagem do boletim de urna**. É um arquivo texto que representa exatamente o mesmo texto do Boletim de Urna, que é impresso pela urna eletrônica e fixado na seção eleitoral ao final da votação. Este é um documento oficial, que mostra quantos votos cada candidato deve, além de outras informações importantes.
- Arquivo BU (ou BUSA) - Trata-se do **boletim de urna**, em formato binário. Este arquivo é o arquivo que o TSE usa para totalizar os votos. Ele contém (ou deveria conter) exatamente as mesmas informações que o arquivo IMGBU.
- Arquivo RDV - Este é o **registro de voto**, em formato binário. É um arquivo que lista todos os votos computados pela urna, inclusive brancos e nulos.
- Arquivo LOGJEZ (ou LOGSAJEZ) - É um arquivo compactado (formato LZMA - abre com o programa 7zip) que contém um ou mais arquivos de **log de urna**. É um arquivo texto que descreve detalhadamente cada operação realizada pela urna eletrônica. Cada vez que um título de eleitor é digitado, cada vez que uma impressão digital é conferida, cada voto que é digitado na urna, tudo fica registrado neste arquivo.

Para os arquivos que possuem **SA** na extensão, a diferença é que estes arquivos foram gerados pelo "Sistema de Apuração" e não pela Urna Eletrônica. Isso ocorre nos casos em que a urna eletrônica não foi usada e, em seu lugar, foi usada uma urna convencional com cédulas de papel, e os votos foram contabilizados manualmente depois do encerramento da votação.

## Diferença em quantidade de votos e quantidade de seções dos arquivos do TSE com o número informado no site do TSE

O [site de resultados do TSE](https://resultados.tse.jus.br/) disponibiliza todos os resultados das eleições para que a população consulte. Essas informações podem ser agregadas pelo Brasil todo, por estado, e também por seção única.

Este relatório mostra a diferença entre a quantidade de votos informada no site do TSE, e os votos que estão disponíveis nos arquivos das urnas.

E também mostra a diferença entre a quantidade de seções eleitorais informadas no site do TSE e a quantidade de seções disponíveis em arquivo.
'

SET @NumRelatorio = 3;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Mais votos no Boletim de Urna do que no Log da Urna

O **Boletim de urna** é o documento oficial que comprova quantos votos cada candidato obteve naquela urna específica. E o **Log de Urna** é um arquivo de texto gerado pela urna com cada operação realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Então se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o número de votos do arquivo log precisa ser igual ao número de votos apresentado pelo Boletim de Urna, pois do contrário, não há como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em dúvida.

Abaixo são listadas todas as seções eleitorais em que o Boletim de Urna apresenta MAIS VOTOS do que votos contados no Log da Urna.
'

SET @NumRelatorio = 1;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Mais votos no Log da Urna do que no Boletim de Urna

O **Boletim de urna** é o documento oficial que comprova quantos votos cada candidato obteve naquela urna específica. E o **Log de Urna** é um arquivo de texto gerado pela urna com cada operação realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Então se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o número de votos do arquivo log precisa ser igual ao número de votos apresentado pelo Boletim de Urna, pois do contrário, não há como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em dúvida.

Abaixo são listadas todas as seções eleitorais em que o Boletim de Urna apresenta MENOS VOTOS do que votos contados no Log da Urna.
'

SET @NumRelatorio = 2;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Sem arquivos, arquivos excluídos ou arquivos rejeitados

Algumas das seções eleitorais simplesmente não possuem arquivos. No arquivo JSON de configuração destas seções, há um campo que justifica a ausência dos arquivos. São 3 possibilidades:
- Excluído
- Sem arquivo
- Rejeitado

O problema, nestes casos, é que os votos dos eleitores destas seções foram simplesmente descartados. Estes eleitores tiveram prejudicadas as suas participações nas eleições.

O voto é um direito e não deve ser vedado a nenhum cidadão o direito de participar do processo democrático.
'

SET @NumRelatorio = 9;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Não há registro de votos

O **registro de votos** é um arquivo binário, gerado pela Urna Eletrônica, que contém todos os votos que foram computados, incluindo os votos brancos e os nulos.

Cada voto digitado vai gerar um registro neste arquivo. O arquivo, inclusive, salva o número digitado para os votos nulos. Se um eleitor votar, por exemplo, no candidato 99 (que não existe), o RDV vai ter um registro com o número 99 e a informação de que aquele voto foi computado como nulo.

É possível também comparar o registro de votos com o Boletim da Urna. As quantidades dos votos para cada candidato no BU deve ser a mesma que o RDV. Caso algum esteja diferente, significa que os arquivos não foram gerados pela mesma urna ou que a urna não está computando corretamente as informações... E em ambos os casos, a credibilidade do processo eleitoral fica em dúvida.

A ausência do registro de votos é um problema grave, pois impede que o Boletim de Urna seja comparado com outra fonte crível de informação.
'

SET @NumRelatorio = 5;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Não há informação de Zerésima

A Zerésima é o processo que garante que a urna eletrônica estava "zerada" antes do início da votação. Esta informação aparece no log da urna.

A Zerésima é realizada normalmente alguns minutos antes do início da votação. A urna imprime um comprovante e este comprovante também é disponibilizado para o público e para o TRE e o TSE (assim como o Boletim de Urna).

Se o arquivo de log da urna não faz menção à Zerésima, significa que este processo não foi realizado - o que não pode acontecer.
'

SET @NumRelatorio = 6;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Zerésima realizada mais de duas horas antes da abertura da Urna

A Zerésima, como explicado anteriormente, é o processo que garante que a urna eletrônica foi zerada antes da votação ser iniciada.

Normalmente este processo é realizado alguns minutos antes da votação iniciar. Porém, nos casos listados abaixo, a Zerésima foi realizada mais de duas horas antes da votação.

Por si só, isso não é um problema, mas ainda assim é algo estranho. Os mesários normalmente não se apresentam para o trabalho da seção com tanta antecedência assim.
'

SET @NumRelatorio = 8;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Códigos de Identificação da Urna Eletrônica repetidos

Toda urna eletrônica possui um número de identificação (Código de Identificação da Urna Eletrônica). Este código é uma das informações emitidas pelo Boletim de Urna.

Existem algumas urnas eletrônicas que possuem o mesmo número de identificação... O que é estranho. Eu não posso afirmar, mas acredito que este número deveria ser único para cada Urna eletrônica.

Abaixo as seções eleitorais e suas urnas que possuem códigos repetidos.
'

SET @NumRelatorio = 4;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Códigos de Identificação da Urna Eletrônica são diferentes no IMGBU e no BU

Ambos os arquivos BU e IMGBU são boletins de urna. A diferença é que o arquivo BU é um arquivo binário, feito para ser lido pelos programas de totalização do TSE, enquanto que o IMGBU é um arquivo texto, que pode ser lido sem dificultade usando um editor de textos comum.

Em essência, ambos os arquivos deveriam conter exatamente as mesmas informações, pois foram gerados pela mesma urna eletrônica. Porém abaixo estão seções eleitorais em que o código de identificação da urna eletrônica é diferente no arquivo BU e no IMGBU.

Isso não deveria acontecer, afinal ambos os arquivos teriam sido gerados pela mesma urna, então não haveria possibilidade do código de identificação ser diferente em ambos os arquivos.

Isso abre uma dúvida enorme no processo eleitoral. Afinal, por qual razão os arquivos foram gerados por urnas diferentes?
'

SET @NumRelatorio = 10;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Votos computados antes do início da votação

O log da urna registra uma linha quando a urna está pronta para receber votos. Esta é a marca que diz que a votação começou.

Portanto, não deveriam haver votos computados antes desta marca. Mas abaixo estão listadas algumas seções eleitorais onde isso ocorreu.
'

SET @NumRelatorio = 7;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Não há arquivo IMGBU

O arquivo IMGBU é a **imagem do boletim de urna**. É o arquivo texto que é impresso pela urna eletrônica ao final da votação. Este é o documento oficial do resultado de cada urna eletrônica.

Este arquivo é gerado pela urna juntamente com os demais arquivos. Ele não poderia estar faltando. Mas para as seções listadas abaixo, não há este arquivo.
'

SET @NumRelatorio = 11;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Não há arquivo BU

O arquivo BU é o **boletim de urna**. É o arquivo binário que é gerado pela urna eletrônica ao final da votação. Este é o arquivo que é processado pelo TSE para fazer a totalização dos votos.

Este arquivo é gerado pela urna juntamente com os demais arquivos. Ele não poderia estar faltando. Mas para as seções listadas abaixo, não há este arquivo.
'

SET @NumRelatorio = 15;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## O Boletim de Urna (arquivo BU) está corrompido

O Boletim de Urna é um arquivo binário que contém a totalização dos votos de cada candidato de uma determinada seção eleitoral. Se este arquivo estiver corrompido, as únicas formas de saber como foi a votação da urna são através da imagem do boletim de urna ou do registro de voto.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informação para comparação.
'

SET @NumRelatorio = 12;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## O Registro de Votos (arquivo RDV) está corrompido

O Registro de votos é um arquivo binário que contém o detalhamento de cada voto para cada candidato de uma seção eleitoral. Se este arquivo estiver corrompido, as únicas formas de saber como foi a votação da urna são através da imagem do boletim de urna ou do boletim de urna binário.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informação para comparação.
'

SET @NumRelatorio = 13;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Diferença de votos entre o arquivo IMGBU e o arquivo BU

O arquivo IMGBU é um arquivo texto que contém a imagem do boletim de urna, e o arquivo BU é um arquivo binário que contém o boletim de urna. Em essência, são o mesmo arquivo, porém em formatos diferentes. 

A imagem pode ser facilmente lida por uma pessoa, já o arquivo binário depende de um programa especificamente criado para ler este tipo de arquivo.

Quando as informações dos dois arquivos são diferentes entre si, fica explicito que os arquivos foram gerados por urnas eletrônicas distintas, e não pelo mesmo equipamento.

Isso coloca em dúvida a lisura do processo eleitoral como um todo, pois isso não deveria ser possível de realizar.
'

SET @NumRelatorio = 14;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Seções que receberam votos por mais do que 9 horas

As seções eleitorais normalmente se iniciam as 8:00 e se encerram as 17:00 (horário de Brasília). Portanto são 9 horas em que as seções permanecem abertas e disponíveis para receber votos.

Porém, nas eleições de 2022 várias seções eleitorais ultrapassaram este período. Foram **151.683** seções no primeiro turno e **5.146** no segundo turno.

Diversas seções permaneceram recebendo votos por mais de **12 horas**, 3 horas além do período regular.
'

SET @NumRelatorio = 16;
SET @Turno = 1;
WHILE @Turno < 3
BEGIN
    IF @Turno = 1
        PRINT @PrimeiroTurno
    ELSE
        PRINT @SegundoTurno

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @IniciarDetalhes

        DECLARE C1 CURSOR FOR
            SELECT Texto FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno ORDER BY Linha
        OPEN C1
        FETCH NEXT FROM C1 INTO @AuxVarchar
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @AuxVarchar
            FETCH NEXT FROM C1 INTO @AuxVarchar
        END
        CLOSE C1
        DEALLOCATE C1

    IF (SELECT COUNT(*) FROM @Relatorio WHERE TipoRelatorio = @NumRelatorio AND Turno = @Turno) > 10
    PRINT @FinalizarDetalhes

    SET @Turno = @Turno + 1
END

PRINT '
## Votos para Deputados Estaduais e Deputados Federais trocados no arquivo .bu

No arquivo `.bu`, cada lista de votos (`TotalVotosVotavel`) está contida em uma série de outras listas que definem o tipo de cargo (majoritário ou proporcional), e o cargo constitucional dos votos daquela lista (Deputado Federal, Estadual ou Distrital, Senador, Governador ou Presidente).

Na Unidade Federativa SP (São Paulo), alguns arquivos `.bu` tiveram votos para um candidato a deputado federal com número **3043**. Ocorre que **não há, neste estado, candidato a deputado federal com este número**. Observando com cuidado, percebe-se que na verdade, os votos deveriam ter sido computados para um deputado estadual com número **43043**.

No TSE a contagem de votos aparece correta, assim como na imagem do boletim de urna (IMGBU). Porém, este defeito levanta dúvidas sobre a confiabilidade do software da urna durante a geração dos arquivos. Estes arquivos não deveriam apresentar problemas, especialmente de consistência. Se fossem arquivos corrompidos, ainda poderia-se justificar alguma falha na mídia de armazenamento ou durante a transmissão. Mas não é este o caso, o arquivo foi assinado e é válido, porém seus dados são inconsistentes.

Este defeito ocorreu em 235 seções eleitorais do estado de São Paulo:
'

PRINT @IniciarDetalhes

PRINT'
- Município 63134 Zona eleitoral 0303 Seção 0087
- Município 64017 Zona eleitoral 0391 Seção 0166
- Município 65897 Zona eleitoral 0396 Seção 0067
- Município 68730 Zona eleitoral 0092 Seção 0039
- Município 70718 Zona eleitoral 0272 Seção 0273
- Município 70718 Zona eleitoral 0272 Seção 0227
- Município 70718 Zona eleitoral 0272 Seção 0063
- Município 70718 Zona eleitoral 0273 Seção 0157
- Município 70718 Zona eleitoral 0273 Seção 0095
- Município 70718 Zona eleitoral 0273 Seção 0013
- Município 70971 Zona eleitoral 0267 Seção 0010
- Município 61638 Zona eleitoral 0385 Seção 0028
- Município 61816 Zona eleitoral 0016 Seção 0439
- Município 62138 Zona eleitoral 0386 Seção 0207
- Município 64270 Zona eleitoral 0367 Seção 0096
- Município 64270 Zona eleitoral 0367 Seção 0103
- Município 65439 Zona eleitoral 0189 Seção 0123
- Município 61565 Zona eleitoral 0190 Seção 0075
- Município 62910 Zona eleitoral 0033 Seção 0319
- Município 62910 Zona eleitoral 0274 Seção 0633
- Município 62910 Zona eleitoral 0275 Seção 0480
- Município 62910 Zona eleitoral 0275 Seção 0247
- Município 62910 Zona eleitoral 0275 Seção 0598
- Município 62910 Zona eleitoral 0380 Seção 0172
- Município 62910 Zona eleitoral 0380 Seção 0230
- Município 62910 Zona eleitoral 0380 Seção 0232
- Município 62910 Zona eleitoral 0380 Seção 0045
- Município 62910 Zona eleitoral 0423 Seção 0236
- Município 65110 Zona eleitoral 0211 Seção 0439
- Município 67032 Zona eleitoral 0153 Seção 0102
- Município 67032 Zona eleitoral 0153 Seção 0077
- Município 67032 Zona eleitoral 0153 Seção 0058
- Município 67032 Zona eleitoral 0153 Seção 0021
- Município 67695 Zona eleitoral 0292 Seção 0131
- Município 67695 Zona eleitoral 0292 Seção 0056
- Município 69795 Zona eleitoral 0288 Seção 0123
- Município 70939 Zona eleitoral 0018 Seção 0027
- Município 63096 Zona eleitoral 0038 Seção 0001
- Município 66710 Zona eleitoral 0237 Seção 0196
- Município 66710 Zona eleitoral 0237 Seção 0144
- Município 66710 Zona eleitoral 0237 Seção 0015
- Município 67377 Zona eleitoral 0358 Seção 0001
- Município 68616 Zona eleitoral 0090 Seção 0132
- Município 68616 Zona eleitoral 0090 Seção 0208
- Município 69019 Zona eleitoral 0099 Seção 0047
- Município 69019 Zona eleitoral 0099 Seção 0054
- Município 70572 Zona eleitoral 0156 Seção 0128
- Município 70572 Zona eleitoral 0264 Seção 0023
- Município 70572 Zona eleitoral 0306 Seção 0122
- Município 70572 Zona eleitoral 0307 Seção 0401
- Município 70572 Zona eleitoral 0383 Seção 0089
- Município 70572 Zona eleitoral 0383 Seção 0243
- Município 71498 Zona eleitoral 0230 Seção 0231
- Município 61310 Zona eleitoral 0158 Seção 0207
- Município 61310 Zona eleitoral 0158 Seção 0192
- Município 61310 Zona eleitoral 0158 Seção 0273
- Município 61310 Zona eleitoral 0158 Seção 0267
- Município 61310 Zona eleitoral 0158 Seção 0240
- Município 61310 Zona eleitoral 0158 Seção 0291
- Município 61310 Zona eleitoral 0158 Seção 0012
- Município 61310 Zona eleitoral 0158 Seção 0006
- Município 61310 Zona eleitoral 0158 Seção 0215
- Município 61310 Zona eleitoral 0158 Seção 0132
- Município 61310 Zona eleitoral 0158 Seção 0116
- Município 61310 Zona eleitoral 0158 Seção 0115
- Município 61310 Zona eleitoral 0158 Seção 0314
- Município 61310 Zona eleitoral 0158 Seção 0308
- Município 61310 Zona eleitoral 0158 Seção 0242
- Município 61310 Zona eleitoral 0158 Seção 0227
- Município 61310 Zona eleitoral 0158 Seção 0226
- Município 61310 Zona eleitoral 0158 Seção 0140
- Município 61310 Zona eleitoral 0158 Seção 0134
- Município 61310 Zona eleitoral 0158 Seção 0118
- Município 61310 Zona eleitoral 0158 Seção 0113
- Município 61310 Zona eleitoral 0158 Seção 0080
- Município 61310 Zona eleitoral 0158 Seção 0034
- Município 61310 Zona eleitoral 0158 Seção 0024
- Município 61310 Zona eleitoral 0158 Seção 0001
- Município 61310 Zona eleitoral 0384 Seção 0204
- Município 61310 Zona eleitoral 0384 Seção 0202
- Município 61310 Zona eleitoral 0384 Seção 0196
- Município 61310 Zona eleitoral 0384 Seção 0162
- Município 61310 Zona eleitoral 0384 Seção 0028
- Município 61310 Zona eleitoral 0384 Seção 0242
- Município 61310 Zona eleitoral 0384 Seção 0240
- Município 61310 Zona eleitoral 0384 Seção 0228
- Município 61310 Zona eleitoral 0384 Seção 0226
- Município 61310 Zona eleitoral 0384 Seção 0274
- Município 61310 Zona eleitoral 0384 Seção 0273
- Município 61310 Zona eleitoral 0384 Seção 0097
- Município 61310 Zona eleitoral 0384 Seção 0207
- Município 61310 Zona eleitoral 0384 Seção 0120
- Município 61310 Zona eleitoral 0384 Seção 0149
- Município 61310 Zona eleitoral 0384 Seção 0101
- Município 61310 Zona eleitoral 0384 Seção 0016
- Município 61310 Zona eleitoral 0384 Seção 0187
- Município 61310 Zona eleitoral 0384 Seção 0179
- Município 61310 Zona eleitoral 0384 Seção 0044
- Município 61310 Zona eleitoral 0384 Seção 0042
- Município 61310 Zona eleitoral 0384 Seção 0078
- Município 61310 Zona eleitoral 0384 Seção 0137
- Município 61310 Zona eleitoral 0384 Seção 0067
- Município 61310 Zona eleitoral 0384 Seção 0017
- Município 61310 Zona eleitoral 0384 Seção 0008
- Município 61310 Zona eleitoral 0384 Seção 0263
- Município 61310 Zona eleitoral 0384 Seção 0250
- Município 63770 Zona eleitoral 0329 Seção 0381
- Município 63770 Zona eleitoral 0426 Seção 0204'
PRINT '- Município 63770 Zona eleitoral 0426 Seção 0067
- Município 66370 Zona eleitoral 0161 Seção 0134
- Município 68756 Zona eleitoral 0093 Seção 0110
- Município 70750 Zona eleitoral 0283 Seção 0354
- Município 71218 Zona eleitoral 0340 Seção 0199
- Município 72257 Zona eleitoral 0034 Seção 0041
- Município 72257 Zona eleitoral 0034 Seção 0139
- Município 62316 Zona eleitoral 0319 Seção 0013
- Município 63070 Zona eleitoral 0140 Seção 0001
- Município 63711 Zona eleitoral 0119 Seção 0162
- Município 69698 Zona eleitoral 0108 Seção 0124
- Município 69698 Zona eleitoral 0305 Seção 0348
- Município 72338 Zona eleitoral 0242 Seção 0069
- Município 61204 Zona eleitoral 0368 Seção 0001
- Município 62952 Zona eleitoral 0035 Seção 0128
- Município 63118 Zona eleitoral 0206 Seção 0118
- Município 63614 Zona eleitoral 0227 Seção 0401
- Município 63614 Zona eleitoral 0227 Seção 0569
- Município 65099 Zona eleitoral 0132 Seção 0216
- Município 66397 Zona eleitoral 0066 Seção 0031
- Município 66397 Zona eleitoral 0066 Seção 0326
- Município 68314 Zona eleitoral 0323 Seção 0099
- Município 70777 Zona eleitoral 0166 Seção 0089
- Município 70777 Zona eleitoral 0269 Seção 0107
- Município 71137 Zona eleitoral 0131 Seção 0084
- Município 71137 Zona eleitoral 0131 Seção 0244
- Município 71579 Zona eleitoral 0324 Seção 0298
- Município 71579 Zona eleitoral 0416 Seção 0107
- Município 61778 Zona eleitoral 0335 Seção 0001
- Município 62391 Zona eleitoral 0369 Seção 0107
- Município 62391 Zona eleitoral 0369 Seção 0157
- Município 62391 Zona eleitoral 0369 Seção 0073
- Município 64297 Zona eleitoral 0192 Seção 0119
- Município 64297 Zona eleitoral 0192 Seção 0121
- Município 64777 Zona eleitoral 0393 Seção 0264
- Município 65692 Zona eleitoral 0058 Seção 0235
- Município 65870 Zona eleitoral 0061 Seção 0032
- Município 66192 Zona eleitoral 0281 Seção 0290
- Município 69531 Zona eleitoral 0172 Seção 0146
- Município 69957 Zona eleitoral 0163 Seção 0047
- Município 70173 Zona eleitoral 0186 Seção 0147
- Município 70173 Zona eleitoral 0186 Seção 0241
- Município 70173 Zona eleitoral 0186 Seção 0050
- Município 70173 Zona eleitoral 0186 Seção 0273
- Município 70173 Zona eleitoral 0186 Seção 0149
- Município 70173 Zona eleitoral 0186 Seção 0124
- Município 70173 Zona eleitoral 0186 Seção 0085
- Município 70173 Zona eleitoral 0186 Seção 0067
- Município 70173 Zona eleitoral 0186 Seção 0364
- Município 70173 Zona eleitoral 0186 Seção 0254
- Município 70173 Zona eleitoral 0186 Seção 0073
- Município 70173 Zona eleitoral 0186 Seção 0053
- Município 70998 Zona eleitoral 0127 Seção 0616
- Município 71072 Zona eleitoral 0001 Seção 0388
- Município 71072 Zona eleitoral 0001 Seção 0057
- Município 71072 Zona eleitoral 0002 Seção 0330
- Município 71072 Zona eleitoral 0002 Seção 0339
- Município 71072 Zona eleitoral 0002 Seção 0337
- Município 71072 Zona eleitoral 0002 Seção 0268
- Município 71072 Zona eleitoral 0002 Seção 0280
- Município 71072 Zona eleitoral 0002 Seção 0042
- Município 71072 Zona eleitoral 0002 Seção 0212
- Município 71072 Zona eleitoral 0002 Seção 0059
- Município 71072 Zona eleitoral 0002 Seção 0055
- Município 71072 Zona eleitoral 0002 Seção 0149
- Município 71072 Zona eleitoral 0004 Seção 0089
- Município 71072 Zona eleitoral 0005 Seção 0206
- Município 71072 Zona eleitoral 0005 Seção 0070
- Município 71072 Zona eleitoral 0005 Seção 0151
- Município 71072 Zona eleitoral 0005 Seção 0025
- Município 71072 Zona eleitoral 0005 Seção 0214
- Município 71072 Zona eleitoral 0006 Seção 0259
- Município 71072 Zona eleitoral 0006 Seção 0135
- Município 71072 Zona eleitoral 0006 Seção 0169
- Município 71072 Zona eleitoral 0006 Seção 0166
- Município 71072 Zona eleitoral 0246 Seção 0205
- Município 71072 Zona eleitoral 0246 Seção 0247
- Município 71072 Zona eleitoral 0246 Seção 0244
- Município 71072 Zona eleitoral 0247 Seção 0333
- Município 71072 Zona eleitoral 0247 Seção 0332
- Município 71072 Zona eleitoral 0247 Seção 0331
- Município 71072 Zona eleitoral 0247 Seção 0563
- Município 71072 Zona eleitoral 0249 Seção 0627
- Município 71072 Zona eleitoral 0251 Seção 0183
- Município 71072 Zona eleitoral 0251 Seção 0104
- Município 71072 Zona eleitoral 0251 Seção 0318
- Município 71072 Zona eleitoral 0251 Seção 0211
- Município 71072 Zona eleitoral 0254 Seção 0470
- Município 71072 Zona eleitoral 0256 Seção 0306
- Município 71072 Zona eleitoral 0256 Seção 0230
- Município 71072 Zona eleitoral 0257 Seção 0173
- Município 71072 Zona eleitoral 0258 Seção 0405
- Município 71072 Zona eleitoral 0258 Seção 0158
- Município 71072 Zona eleitoral 0258 Seção 0529
- Município 71072 Zona eleitoral 0258 Seção 0513
- Município 71072 Zona eleitoral 0259 Seção 0063
- Município 71072 Zona eleitoral 0260 Seção 0012
- Município 71072 Zona eleitoral 0260 Seção 0013
- Município 71072 Zona eleitoral 0280 Seção 0836
- Município 71072 Zona eleitoral 0320 Seção 0451
- Município 71072 Zona eleitoral 0320 Seção 0394
- Município 71072 Zona eleitoral 0325 Seção 0646
- Município 71072 Zona eleitoral 0325 Seção 0168
- Município 71072 Zona eleitoral 0327 Seção 0377
- Município 71072 Zona eleitoral 0346 Seção 0085
- Município 71072 Zona eleitoral 0346 Seção 0645
- Município 71072 Zona eleitoral 0346 Seção 0053
- Município 71072 Zona eleitoral 0348 Seção 0160
- Município 71072 Zona eleitoral 0349 Seção 0193
- Município 71072 Zona eleitoral 0350 Seção 0444
- Município 71072 Zona eleitoral 0352 Seção 0193
- Município 71072 Zona eleitoral 0352 Seção 0053
- Município 71072 Zona eleitoral 0372 Seção 0727
- Município 71072 Zona eleitoral 0374 Seção 0453
- Município 71072 Zona eleitoral 0374 Seção 0533
- Município 71072 Zona eleitoral 0374 Seção 0233
- Município 71072 Zona eleitoral 0398 Seção 0176
- Município 71072 Zona eleitoral 0405 Seção 0301
- Município 71072 Zona eleitoral 0413 Seção 0304
- Município 71072 Zona eleitoral 0413 Seção 0313
- Município 71072 Zona eleitoral 0418 Seção 0297
- Município 71072 Zona eleitoral 0421 Seção 0092
- Município 71072 Zona eleitoral 0422 Seção 0392
- Município 71072 Zona eleitoral 0422 Seção 0036
- Município 71072 Zona eleitoral 0422 Seção 0356
- Município 71072 Zona eleitoral 0422 Seção 0160
- Município 72370 Zona eleitoral 0345 Seção 0198'

PRINT @FinalizarDetalhes

PRINT '
Fim do relatório.
'


