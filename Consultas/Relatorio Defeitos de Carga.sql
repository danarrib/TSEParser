-- Gerar o Relat�rio de Defeitos de carga
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
BEGIN -- Relat�rios 1 e 2 - Obter as se��es eleitorais que apresentam diferen�a na contagem de vota��es do log comparando com a vota��o do Boletim de Urna

    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + ' - Vota��es no BU: ' + CONVERT(varchar(20), SE.PR_Total) + ', Vota��es no Log: ' + CONVERT(varchar(20), COUNT(*)) + '.',
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + ' - Vota��es no BU: ' + CONVERT(varchar(20), SE.PR_Total) + ', Vota��es no Log: ' + CONVERT(varchar(20), COUNT(*)) + '.',
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

    PRINT 'Relat�rios 1 e 2 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 3 - Contagem de se��es eleitorais por UF, e votos por se��o eleitoral (comparando com a informa��o dispon�vel no site do TSE)
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
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de se��es eleitorais carregadas (' + CONVERT(varchar(20), COUNT(*)) + ') � diferente do TSE (' + CONVERT(varchar(20), T.QtdSecoes) + ').',
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
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de se��es eleitorais carregadas (' + CONVERT(varchar(20), COUNT(*)) + ') � diferente do TSE (' + CONVERT(varchar(20), T.QtdSecoes) + ').',
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
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de votos v�lidos carregados (' + CONVERT(varchar(20), SUM(CONVERT(int, SE.PR_Total))) + ') � diferente do TSE (' + CONVERT(varchar(20), T.QtdVotosPresidente) + ').',
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
    SELECT      '- UF ' + M.UFSigla + ' - Quantidade de votos v�lidos carregados (' + CONVERT(varchar(20), SUM(CONVERT(int, SE.PR_Total))) + ') � diferente do TSE (' + CONVERT(varchar(20), T.QtdVotosPresidente) + ').',
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

    PRINT 'Relat�rio 3 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 4 - C�digos de identifica��o de urna eletr�nica repetidos
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
        SELECT '- C�digo Identificador de Urna Eletr�nica: **' + CONVERT(varchar(20), @AuxInt) + '** - Quantidade de ocorr�ncias: **' + CONVERT(varchar(20), @AuxInt2) + '**.' + CHAR(13),
        4 as TipoRelatorio, 1 as Turno

        INSERT INTO @Relatorio
        SELECT      '  - UF ' + UF.Sigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto,
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
        SELECT '- C�digo Identificador de Urna Eletr�nica: **' + CONVERT(varchar(20), @AuxInt) + '** - Quantidade de ocorr�ncias: **' + CONVERT(varchar(20), @AuxInt2) + '**.' + CHAR(13),
        4 as TipoRelatorio, 2 as Turno

        INSERT INTO @Relatorio
        SELECT      '  - ' + UF.Sigla + '(' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome + 
                    '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4) + '.' as Texto,
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

    PRINT 'Relat�rio 4 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 5 - Obter as se��es que n�o possuem registro de voto
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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

    PRINT 'Relat�rio 5 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 6 - Obter as se��es que n�o possuem informa��o de Zer�sima
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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

    PRINT 'Relat�rio 6 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 7 - Se��es com votos computados antes da abertura da urna
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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

    PRINT 'Relat�rio 7 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 8 - Zer�sima realizada mais de duas horas antes da abertura da urna
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
                + '. Abertura: ' + FORMAT(SE.AberturaUrnaEletronica, 'dd/MM/yyyy HH:mm:ss') + ', Zer�sima: ' + FORMAT(SE.Zeresima, 'dd/MM/yyyy HH:mm:ss') + '.', 8 as TipoRelatorio, 1 as Turno
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao), 4)
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

    PRINT 'Relat�rio 8 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 9 - Sem arquivos, arquivos exclu�dos ou arquivos rejeitados
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Motivo: ' + CASE WHEN DS.SemArquivo = 1 THEN 'Sem arquivo' WHEN DS.Excluido = 1 THEN 'Exclu�do' WHEN DS.Rejeitado = 1 THEN 'Rejeitado' ELSE 'Desconhecido' END + '.', 
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. Motivo: ' + CASE WHEN DS.SemArquivo = 1 THEN 'Sem arquivo' WHEN DS.Excluido = 1 THEN 'Exclu�do' WHEN DS.Rejeitado = 1 THEN 'Rejeitado' ELSE 'Desconhecido' END + '.', 
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

    PRINT 'Relat�rio 9 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 10 - C�digo de Identifica��o da UE diferente no BU e no IMGBU
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. C�digo no BU: ' + CONVERT(varchar(20), DS.CodigoIdentificacaoUrnaEletronicaBU) + ' - C�digo no IMGBU: ' + CONVERT(varchar(20), SE.CodigoIdentificacaoUrnaEletronica) + '.', 
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
                + '. C�digo no BU: ' + CONVERT(varchar(20), DS.CodigoIdentificacaoUrnaEletronicaBU) + ' - C�digo no IMGBU: ' + CONVERT(varchar(20), SE.CodigoIdentificacaoUrnaEletronica) + '.', 
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

    PRINT 'Relat�rio 10 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 11 - Arquivo IMGBU Faltando
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    
    PRINT 'Relat�rio 11 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 12 - Arquivo BU Corrompido
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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

    PRINT 'Relat�rio 12 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 13 - Arquivo RDV Corrompido
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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

    PRINT 'Relat�rio 13 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 14 - Diferen�a de votos entre BU e IMGBU
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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

    PRINT 'Relat�rio 14 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 15 - Arquivo BU Faltando
    INSERT INTO @Relatorio
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    SELECT      '- UF ' + M.UFSigla + ' (' + UF.Nome + '), Munic�pio ' + RIGHT('0000' + CONVERT(varchar(20), M.Codigo), 5) + ' (' + M.Nome 
                + '), Zona ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoZonaEleitoral), 4) + ', Se��o ' + RIGHT('000' + CONVERT(varchar(20), DS.CodigoSecao), 4)
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
    
    PRINT 'Relat�rio 15 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END

IF 1=1
BEGIN -- Relat�rio 16 - Se��es eleitorais que receberam votos por mais de 9 horas.
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

    PRINT 'Relat�rio 16 - Tempo decorrido: ' + CONVERT(varchar(20), DATEDIFF(ss, @InicioProcessamento, GETDATE())) + ' segundos.'
END


PRINT '# Defeitos nos arquivos do TSE

**Este relat�rio ainda est� sendo atualizado - N�o utilizar estes dados para prop�sitos oficiais**

Os arquivos disponibilizados pelo TSE apresentam alguns defeitos, que ser�o relacionados a seguir. As Urnas Eletr�nicas produzem v�rios tipos diferentes de arquivos. Para o contexto desta an�lise, utilizamos apenas tipos de arquivos. S�o eles:
- Arquivo IMGBU (ou IMGBUSA) - � a **imagem do boletim de urna**. � um arquivo texto que representa exatamente o mesmo texto do Boletim de Urna, que � impresso pela urna eletr�nica e fixado na se��o eleitoral ao final da vota��o. Este � um documento oficial, que mostra quantos votos cada candidato deve, al�m de outras informa��es importantes.
- Arquivo BU (ou BUSA) - Trata-se do **boletim de urna**, em formato bin�rio. Este arquivo � o arquivo que o TSE usa para totalizar os votos. Ele cont�m (ou deveria conter) exatamente as mesmas informa��es que o arquivo IMGBU.
- Arquivo RDV - Este � o **registro de voto**, em formato bin�rio. � um arquivo que lista todos os votos computados pela urna, inclusive brancos e nulos.
- Arquivo LOGJEZ (ou LOGSAJEZ) - � um arquivo compactado (formato LZMA - abre com o programa 7zip) que cont�m um ou mais arquivos de **log de urna**. � um arquivo texto que descreve detalhadamente cada opera��o realizada pela urna eletr�nica. Cada vez que um t�tulo de eleitor � digitado, cada vez que uma impress�o digital � conferida, cada voto que � digitado na urna, tudo fica registrado neste arquivo.

Para os arquivos que possuem **SA** na extens�o, a diferen�a � que estes arquivos foram gerados pelo "Sistema de Apura��o" e n�o pela Urna Eletr�nica. Isso ocorre nos casos em que a urna eletr�nica n�o foi usada e, em seu lugar, foi usada uma urna convencional com c�dulas de papel, e os votos foram contabilizados manualmente depois do encerramento da vota��o.

## Diferen�a em quantidade de votos e quantidade de se��es dos arquivos do TSE com o n�mero informado no site do TSE

O [site de resultados do TSE](https://resultados.tse.jus.br/) disponibiliza todos os resultados das elei��es para que a popula��o consulte. Essas informa��es podem ser agregadas pelo Brasil todo, por estado, e tamb�m por se��o �nica.

Este relat�rio mostra a diferen�a entre a quantidade de votos informada no site do TSE, e os votos que est�o dispon�veis nos arquivos das urnas.

E tamb�m mostra a diferen�a entre a quantidade de se��es eleitorais informadas no site do TSE e a quantidade de se��es dispon�veis em arquivo.
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

O **Boletim de urna** � o documento oficial que comprova quantos votos cada candidato obteve naquela urna espec�fica. E o **Log de Urna** � um arquivo de texto gerado pela urna com cada opera��o realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Ent�o se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o n�mero de votos do arquivo log precisa ser igual ao n�mero de votos apresentado pelo Boletim de Urna, pois do contr�rio, n�o h� como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em d�vida.

Abaixo s�o listadas todas as se��es eleitorais em que o Boletim de Urna apresenta MAIS VOTOS do que votos contados no Log da Urna.
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

O **Boletim de urna** � o documento oficial que comprova quantos votos cada candidato obteve naquela urna espec�fica. E o **Log de Urna** � um arquivo de texto gerado pela urna com cada opera��o realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Ent�o se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o n�mero de votos do arquivo log precisa ser igual ao n�mero de votos apresentado pelo Boletim de Urna, pois do contr�rio, n�o h� como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em d�vida.

Abaixo s�o listadas todas as se��es eleitorais em que o Boletim de Urna apresenta MENOS VOTOS do que votos contados no Log da Urna.
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
## Sem arquivos, arquivos exclu�dos ou arquivos rejeitados

Algumas das se��es eleitorais simplesmente n�o possuem arquivos. No arquivo JSON de configura��o destas se��es, h� um campo que justifica a aus�ncia dos arquivos. S�o 3 possibilidades:
- Exclu�do
- Sem arquivo
- Rejeitado

O problema, nestes casos, � que os votos dos eleitores destas se��es foram simplesmente descartados. Estes eleitores tiveram prejudicadas as suas participa��es nas elei��es.

O voto � um direito e n�o deve ser vedado a nenhum cidad�o o direito de participar do processo democr�tico.
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
## N�o h� registro de votos

O **registro de votos** � um arquivo bin�rio, gerado pela Urna Eletr�nica, que cont�m todos os votos que foram computados, incluindo os votos brancos e os nulos.

Cada voto digitado vai gerar um registro neste arquivo. O arquivo, inclusive, salva o n�mero digitado para os votos nulos. Se um eleitor votar, por exemplo, no candidato 99 (que n�o existe), o RDV vai ter um registro com o n�mero 99 e a informa��o de que aquele voto foi computado como nulo.

� poss�vel tamb�m comparar o registro de votos com o Boletim da Urna. As quantidades dos votos para cada candidato no BU deve ser a mesma que o RDV. Caso algum esteja diferente, significa que os arquivos n�o foram gerados pela mesma urna ou que a urna n�o est� computando corretamente as informa��es... E em ambos os casos, a credibilidade do processo eleitoral fica em d�vida.

A aus�ncia do registro de votos � um problema grave, pois impede que o Boletim de Urna seja comparado com outra fonte cr�vel de informa��o.
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
## N�o h� informa��o de Zer�sima

A Zer�sima � o processo que garante que a urna eletr�nica estava "zerada" antes do in�cio da vota��o. Esta informa��o aparece no log da urna.

A Zer�sima � realizada normalmente alguns minutos antes do in�cio da vota��o. A urna imprime um comprovante e este comprovante tamb�m � disponibilizado para o p�blico e para o TRE e o TSE (assim como o Boletim de Urna).

Se o arquivo de log da urna n�o faz men��o � Zer�sima, significa que este processo n�o foi realizado - o que n�o pode acontecer.
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
## Zer�sima realizada mais de duas horas antes da abertura da Urna

A Zer�sima, como explicado anteriormente, � o processo que garante que a urna eletr�nica foi zerada antes da vota��o ser iniciada.

Normalmente este processo � realizado alguns minutos antes da vota��o iniciar. Por�m, nos casos listados abaixo, a Zer�sima foi realizada mais de duas horas antes da vota��o.

Por si s�, isso n�o � um problema, mas ainda assim � algo estranho. Os mes�rios normalmente n�o se apresentam para o trabalho da se��o com tanta anteced�ncia assim.
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
## C�digos de Identifica��o da Urna Eletr�nica repetidos

Toda urna eletr�nica possui um n�mero de identifica��o (C�digo de Identifica��o da Urna Eletr�nica). Este c�digo � uma das informa��es emitidas pelo Boletim de Urna.

Existem algumas urnas eletr�nicas que possuem o mesmo n�mero de identifica��o... O que � estranho. Eu n�o posso afirmar, mas acredito que este n�mero deveria ser �nico para cada Urna eletr�nica.

Abaixo as se��es eleitorais e suas urnas que possuem c�digos repetidos.
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
## C�digos de Identifica��o da Urna Eletr�nica s�o diferentes no IMGBU e no BU

Ambos os arquivos BU e IMGBU s�o boletins de urna. A diferen�a � que o arquivo BU � um arquivo bin�rio, feito para ser lido pelos programas de totaliza��o do TSE, enquanto que o IMGBU � um arquivo texto, que pode ser lido sem dificultade usando um editor de textos comum.

Em ess�ncia, ambos os arquivos deveriam conter exatamente as mesmas informa��es, pois foram gerados pela mesma urna eletr�nica. Por�m abaixo est�o se��es eleitorais em que o c�digo de identifica��o da urna eletr�nica � diferente no arquivo BU e no IMGBU.

Isso n�o deveria acontecer, afinal ambos os arquivos teriam sido gerados pela mesma urna, ent�o n�o haveria possibilidade do c�digo de identifica��o ser diferente em ambos os arquivos.

Isso abre uma d�vida enorme no processo eleitoral. Afinal, por qual raz�o os arquivos foram gerados por urnas diferentes?
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
## Votos computados antes do in�cio da vota��o

O log da urna registra uma linha quando a urna est� pronta para receber votos. Esta � a marca que diz que a vota��o come�ou.

Portanto, n�o deveriam haver votos computados antes desta marca. Mas abaixo est�o listadas algumas se��es eleitorais onde isso ocorreu.
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
## N�o h� arquivo IMGBU

O arquivo IMGBU � a **imagem do boletim de urna**. � o arquivo texto que � impresso pela urna eletr�nica ao final da vota��o. Este � o documento oficial do resultado de cada urna eletr�nica.

Este arquivo � gerado pela urna juntamente com os demais arquivos. Ele n�o poderia estar faltando. Mas para as se��es listadas abaixo, n�o h� este arquivo.
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
## N�o h� arquivo BU

O arquivo BU � o **boletim de urna**. � o arquivo bin�rio que � gerado pela urna eletr�nica ao final da vota��o. Este � o arquivo que � processado pelo TSE para fazer a totaliza��o dos votos.

Este arquivo � gerado pela urna juntamente com os demais arquivos. Ele n�o poderia estar faltando. Mas para as se��es listadas abaixo, n�o h� este arquivo.
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
## O Boletim de Urna (arquivo BU) est� corrompido

O Boletim de Urna � um arquivo bin�rio que cont�m a totaliza��o dos votos de cada candidato de uma determinada se��o eleitoral. Se este arquivo estiver corrompido, as �nicas formas de saber como foi a vota��o da urna s�o atrav�s da imagem do boletim de urna ou do registro de voto.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informa��o para compara��o.
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
## O Registro de Votos (arquivo RDV) est� corrompido

O Registro de votos � um arquivo bin�rio que cont�m o detalhamento de cada voto para cada candidato de uma se��o eleitoral. Se este arquivo estiver corrompido, as �nicas formas de saber como foi a vota��o da urna s�o atrav�s da imagem do boletim de urna ou do boletim de urna bin�rio.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informa��o para compara��o.
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
## Diferen�a de votos entre o arquivo IMGBU e o arquivo BU

O arquivo IMGBU � um arquivo texto que cont�m a imagem do boletim de urna, e o arquivo BU � um arquivo bin�rio que cont�m o boletim de urna. Em ess�ncia, s�o o mesmo arquivo, por�m em formatos diferentes. 

A imagem pode ser facilmente lida por uma pessoa, j� o arquivo bin�rio depende de um programa especificamente criado para ler este tipo de arquivo.

Quando as informa��es dos dois arquivos s�o diferentes entre si, fica explicito que os arquivos foram gerados por urnas eletr�nicas distintas, e n�o pelo mesmo equipamento.

Isso coloca em d�vida a lisura do processo eleitoral como um todo, pois isso n�o deveria ser poss�vel de realizar.
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
## Se��es que receberam votos por mais do que 9 horas

As se��es eleitorais normalmente se iniciam as 8:00 e se encerram as 17:00 (hor�rio de Bras�lia). Portanto s�o 9 horas em que as se��es permanecem abertas e dispon�veis para receber votos.

Por�m, nas elei��es de 2022 v�rias se��es eleitorais ultrapassaram este per�odo. Foram **151.683** se��es no primeiro turno e **5.146** no segundo turno.

Diversas se��es permaneceram recebendo votos por mais de **12 horas**, 3 horas al�m do per�odo regular.
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

No arquivo `.bu`, cada lista de votos (`TotalVotosVotavel`) est� contida em uma s�rie de outras listas que definem o tipo de cargo (majorit�rio ou proporcional), e o cargo constitucional dos votos daquela lista (Deputado Federal, Estadual ou Distrital, Senador, Governador ou Presidente).

Na Unidade Federativa SP (S�o Paulo), alguns arquivos `.bu` tiveram votos para um candidato a deputado federal com n�mero **3043**. Ocorre que **n�o h�, neste estado, candidato a deputado federal com este n�mero**. Observando com cuidado, percebe-se que na verdade, os votos deveriam ter sido computados para um deputado estadual com n�mero **43043**.

No TSE a contagem de votos aparece correta, assim como na imagem do boletim de urna (IMGBU). Por�m, este defeito levanta d�vidas sobre a confiabilidade do software da urna durante a gera��o dos arquivos. Estes arquivos n�o deveriam apresentar problemas, especialmente de consist�ncia. Se fossem arquivos corrompidos, ainda poderia-se justificar alguma falha na m�dia de armazenamento ou durante a transmiss�o. Mas n�o � este o caso, o arquivo foi assinado e � v�lido, por�m seus dados s�o inconsistentes.

Este defeito ocorreu em 235 se��es eleitorais do estado de S�o Paulo:
'

PRINT @IniciarDetalhes

PRINT'
- Munic�pio 63134 Zona eleitoral 0303 Se��o 0087
- Munic�pio 64017 Zona eleitoral 0391 Se��o 0166
- Munic�pio 65897 Zona eleitoral 0396 Se��o 0067
- Munic�pio 68730 Zona eleitoral 0092 Se��o 0039
- Munic�pio 70718 Zona eleitoral 0272 Se��o 0273
- Munic�pio 70718 Zona eleitoral 0272 Se��o 0227
- Munic�pio 70718 Zona eleitoral 0272 Se��o 0063
- Munic�pio 70718 Zona eleitoral 0273 Se��o 0157
- Munic�pio 70718 Zona eleitoral 0273 Se��o 0095
- Munic�pio 70718 Zona eleitoral 0273 Se��o 0013
- Munic�pio 70971 Zona eleitoral 0267 Se��o 0010
- Munic�pio 61638 Zona eleitoral 0385 Se��o 0028
- Munic�pio 61816 Zona eleitoral 0016 Se��o 0439
- Munic�pio 62138 Zona eleitoral 0386 Se��o 0207
- Munic�pio 64270 Zona eleitoral 0367 Se��o 0096
- Munic�pio 64270 Zona eleitoral 0367 Se��o 0103
- Munic�pio 65439 Zona eleitoral 0189 Se��o 0123
- Munic�pio 61565 Zona eleitoral 0190 Se��o 0075
- Munic�pio 62910 Zona eleitoral 0033 Se��o 0319
- Munic�pio 62910 Zona eleitoral 0274 Se��o 0633
- Munic�pio 62910 Zona eleitoral 0275 Se��o 0480
- Munic�pio 62910 Zona eleitoral 0275 Se��o 0247
- Munic�pio 62910 Zona eleitoral 0275 Se��o 0598
- Munic�pio 62910 Zona eleitoral 0380 Se��o 0172
- Munic�pio 62910 Zona eleitoral 0380 Se��o 0230
- Munic�pio 62910 Zona eleitoral 0380 Se��o 0232
- Munic�pio 62910 Zona eleitoral 0380 Se��o 0045
- Munic�pio 62910 Zona eleitoral 0423 Se��o 0236
- Munic�pio 65110 Zona eleitoral 0211 Se��o 0439
- Munic�pio 67032 Zona eleitoral 0153 Se��o 0102
- Munic�pio 67032 Zona eleitoral 0153 Se��o 0077
- Munic�pio 67032 Zona eleitoral 0153 Se��o 0058
- Munic�pio 67032 Zona eleitoral 0153 Se��o 0021
- Munic�pio 67695 Zona eleitoral 0292 Se��o 0131
- Munic�pio 67695 Zona eleitoral 0292 Se��o 0056
- Munic�pio 69795 Zona eleitoral 0288 Se��o 0123
- Munic�pio 70939 Zona eleitoral 0018 Se��o 0027
- Munic�pio 63096 Zona eleitoral 0038 Se��o 0001
- Munic�pio 66710 Zona eleitoral 0237 Se��o 0196
- Munic�pio 66710 Zona eleitoral 0237 Se��o 0144
- Munic�pio 66710 Zona eleitoral 0237 Se��o 0015
- Munic�pio 67377 Zona eleitoral 0358 Se��o 0001
- Munic�pio 68616 Zona eleitoral 0090 Se��o 0132
- Munic�pio 68616 Zona eleitoral 0090 Se��o 0208
- Munic�pio 69019 Zona eleitoral 0099 Se��o 0047
- Munic�pio 69019 Zona eleitoral 0099 Se��o 0054
- Munic�pio 70572 Zona eleitoral 0156 Se��o 0128
- Munic�pio 70572 Zona eleitoral 0264 Se��o 0023
- Munic�pio 70572 Zona eleitoral 0306 Se��o 0122
- Munic�pio 70572 Zona eleitoral 0307 Se��o 0401
- Munic�pio 70572 Zona eleitoral 0383 Se��o 0089
- Munic�pio 70572 Zona eleitoral 0383 Se��o 0243
- Munic�pio 71498 Zona eleitoral 0230 Se��o 0231
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0207
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0192
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0273
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0267
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0240
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0291
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0012
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0006
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0215
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0132
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0116
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0115
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0314
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0308
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0242
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0227
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0226
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0140
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0134
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0118
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0113
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0080
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0034
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0024
- Munic�pio 61310 Zona eleitoral 0158 Se��o 0001
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0204
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0202
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0196
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0162
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0028
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0242
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0240
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0228
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0226
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0274
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0273
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0097
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0207
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0120
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0149
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0101
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0016
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0187
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0179
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0044
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0042
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0078
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0137
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0067
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0017
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0008
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0263
- Munic�pio 61310 Zona eleitoral 0384 Se��o 0250
- Munic�pio 63770 Zona eleitoral 0329 Se��o 0381
- Munic�pio 63770 Zona eleitoral 0426 Se��o 0204'
PRINT '- Munic�pio 63770 Zona eleitoral 0426 Se��o 0067
- Munic�pio 66370 Zona eleitoral 0161 Se��o 0134
- Munic�pio 68756 Zona eleitoral 0093 Se��o 0110
- Munic�pio 70750 Zona eleitoral 0283 Se��o 0354
- Munic�pio 71218 Zona eleitoral 0340 Se��o 0199
- Munic�pio 72257 Zona eleitoral 0034 Se��o 0041
- Munic�pio 72257 Zona eleitoral 0034 Se��o 0139
- Munic�pio 62316 Zona eleitoral 0319 Se��o 0013
- Munic�pio 63070 Zona eleitoral 0140 Se��o 0001
- Munic�pio 63711 Zona eleitoral 0119 Se��o 0162
- Munic�pio 69698 Zona eleitoral 0108 Se��o 0124
- Munic�pio 69698 Zona eleitoral 0305 Se��o 0348
- Munic�pio 72338 Zona eleitoral 0242 Se��o 0069
- Munic�pio 61204 Zona eleitoral 0368 Se��o 0001
- Munic�pio 62952 Zona eleitoral 0035 Se��o 0128
- Munic�pio 63118 Zona eleitoral 0206 Se��o 0118
- Munic�pio 63614 Zona eleitoral 0227 Se��o 0401
- Munic�pio 63614 Zona eleitoral 0227 Se��o 0569
- Munic�pio 65099 Zona eleitoral 0132 Se��o 0216
- Munic�pio 66397 Zona eleitoral 0066 Se��o 0031
- Munic�pio 66397 Zona eleitoral 0066 Se��o 0326
- Munic�pio 68314 Zona eleitoral 0323 Se��o 0099
- Munic�pio 70777 Zona eleitoral 0166 Se��o 0089
- Munic�pio 70777 Zona eleitoral 0269 Se��o 0107
- Munic�pio 71137 Zona eleitoral 0131 Se��o 0084
- Munic�pio 71137 Zona eleitoral 0131 Se��o 0244
- Munic�pio 71579 Zona eleitoral 0324 Se��o 0298
- Munic�pio 71579 Zona eleitoral 0416 Se��o 0107
- Munic�pio 61778 Zona eleitoral 0335 Se��o 0001
- Munic�pio 62391 Zona eleitoral 0369 Se��o 0107
- Munic�pio 62391 Zona eleitoral 0369 Se��o 0157
- Munic�pio 62391 Zona eleitoral 0369 Se��o 0073
- Munic�pio 64297 Zona eleitoral 0192 Se��o 0119
- Munic�pio 64297 Zona eleitoral 0192 Se��o 0121
- Munic�pio 64777 Zona eleitoral 0393 Se��o 0264
- Munic�pio 65692 Zona eleitoral 0058 Se��o 0235
- Munic�pio 65870 Zona eleitoral 0061 Se��o 0032
- Munic�pio 66192 Zona eleitoral 0281 Se��o 0290
- Munic�pio 69531 Zona eleitoral 0172 Se��o 0146
- Munic�pio 69957 Zona eleitoral 0163 Se��o 0047
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0147
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0241
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0050
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0273
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0149
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0124
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0085
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0067
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0364
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0254
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0073
- Munic�pio 70173 Zona eleitoral 0186 Se��o 0053
- Munic�pio 70998 Zona eleitoral 0127 Se��o 0616
- Munic�pio 71072 Zona eleitoral 0001 Se��o 0388
- Munic�pio 71072 Zona eleitoral 0001 Se��o 0057
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0330
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0339
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0337
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0268
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0280
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0042
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0212
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0059
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0055
- Munic�pio 71072 Zona eleitoral 0002 Se��o 0149
- Munic�pio 71072 Zona eleitoral 0004 Se��o 0089
- Munic�pio 71072 Zona eleitoral 0005 Se��o 0206
- Munic�pio 71072 Zona eleitoral 0005 Se��o 0070
- Munic�pio 71072 Zona eleitoral 0005 Se��o 0151
- Munic�pio 71072 Zona eleitoral 0005 Se��o 0025
- Munic�pio 71072 Zona eleitoral 0005 Se��o 0214
- Munic�pio 71072 Zona eleitoral 0006 Se��o 0259
- Munic�pio 71072 Zona eleitoral 0006 Se��o 0135
- Munic�pio 71072 Zona eleitoral 0006 Se��o 0169
- Munic�pio 71072 Zona eleitoral 0006 Se��o 0166
- Munic�pio 71072 Zona eleitoral 0246 Se��o 0205
- Munic�pio 71072 Zona eleitoral 0246 Se��o 0247
- Munic�pio 71072 Zona eleitoral 0246 Se��o 0244
- Munic�pio 71072 Zona eleitoral 0247 Se��o 0333
- Munic�pio 71072 Zona eleitoral 0247 Se��o 0332
- Munic�pio 71072 Zona eleitoral 0247 Se��o 0331
- Munic�pio 71072 Zona eleitoral 0247 Se��o 0563
- Munic�pio 71072 Zona eleitoral 0249 Se��o 0627
- Munic�pio 71072 Zona eleitoral 0251 Se��o 0183
- Munic�pio 71072 Zona eleitoral 0251 Se��o 0104
- Munic�pio 71072 Zona eleitoral 0251 Se��o 0318
- Munic�pio 71072 Zona eleitoral 0251 Se��o 0211
- Munic�pio 71072 Zona eleitoral 0254 Se��o 0470
- Munic�pio 71072 Zona eleitoral 0256 Se��o 0306
- Munic�pio 71072 Zona eleitoral 0256 Se��o 0230
- Munic�pio 71072 Zona eleitoral 0257 Se��o 0173
- Munic�pio 71072 Zona eleitoral 0258 Se��o 0405
- Munic�pio 71072 Zona eleitoral 0258 Se��o 0158
- Munic�pio 71072 Zona eleitoral 0258 Se��o 0529
- Munic�pio 71072 Zona eleitoral 0258 Se��o 0513
- Munic�pio 71072 Zona eleitoral 0259 Se��o 0063
- Munic�pio 71072 Zona eleitoral 0260 Se��o 0012
- Munic�pio 71072 Zona eleitoral 0260 Se��o 0013
- Munic�pio 71072 Zona eleitoral 0280 Se��o 0836
- Munic�pio 71072 Zona eleitoral 0320 Se��o 0451
- Munic�pio 71072 Zona eleitoral 0320 Se��o 0394
- Munic�pio 71072 Zona eleitoral 0325 Se��o 0646
- Munic�pio 71072 Zona eleitoral 0325 Se��o 0168
- Munic�pio 71072 Zona eleitoral 0327 Se��o 0377
- Munic�pio 71072 Zona eleitoral 0346 Se��o 0085
- Munic�pio 71072 Zona eleitoral 0346 Se��o 0645
- Munic�pio 71072 Zona eleitoral 0346 Se��o 0053
- Munic�pio 71072 Zona eleitoral 0348 Se��o 0160
- Munic�pio 71072 Zona eleitoral 0349 Se��o 0193
- Munic�pio 71072 Zona eleitoral 0350 Se��o 0444
- Munic�pio 71072 Zona eleitoral 0352 Se��o 0193
- Munic�pio 71072 Zona eleitoral 0352 Se��o 0053
- Munic�pio 71072 Zona eleitoral 0372 Se��o 0727
- Munic�pio 71072 Zona eleitoral 0374 Se��o 0453
- Munic�pio 71072 Zona eleitoral 0374 Se��o 0533
- Munic�pio 71072 Zona eleitoral 0374 Se��o 0233
- Munic�pio 71072 Zona eleitoral 0398 Se��o 0176
- Munic�pio 71072 Zona eleitoral 0405 Se��o 0301
- Munic�pio 71072 Zona eleitoral 0413 Se��o 0304
- Munic�pio 71072 Zona eleitoral 0413 Se��o 0313
- Munic�pio 71072 Zona eleitoral 0418 Se��o 0297
- Munic�pio 71072 Zona eleitoral 0421 Se��o 0092
- Munic�pio 71072 Zona eleitoral 0422 Se��o 0392
- Munic�pio 71072 Zona eleitoral 0422 Se��o 0036
- Munic�pio 71072 Zona eleitoral 0422 Se��o 0356
- Munic�pio 71072 Zona eleitoral 0422 Se��o 0160
- Munic�pio 72370 Zona eleitoral 0345 Se��o 0198'

PRINT @FinalizarDetalhes

PRINT '
Fim do relat�rio.
'


