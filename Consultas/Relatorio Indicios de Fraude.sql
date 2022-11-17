    DECLARE @AuxInt     int,
            @AuxInt2    int,
            @AuxVarchar varchar(1000);

IF 0=1
BEGIN

    -- DROP TABLE #DadosT1
    SELECT      M.UFSigla,
                SE.MunicipioCodigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.ModeloUrnaEletronica,
                ISNULL(VS13.QtdVotos,0) as Votos13,
                ISNULL(VS22.QtdVotos,0) as Votos22,
                SE.PR_VotosNominais as VotosValidos,
                CONVERT(numeric(18,1), CONVERT(numeric(18,4), ISNULL(VS13.QtdVotos, 1)) / CONVERT(numeric(18,4), ISNULL(VS22.QtdVotos, 1)), VS13.QtdVotos) as ProporcaoVotos
    INTO        #DadosT1
    FROM        TSEParser_T1..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio       M with (NOLOCK)
        ON      M.Codigo        = SE.MunicipioCodigo
            AND SE.LogUrnaInconsistente = 0
            AND SE.ResultadoSistemaApuracao = 0
    LEFT JOIN   TSEParser_T1..VotosSecao      VS13 with (NOLOCK)
        ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS13.CodigoSecao            = SE.CodigoSecao
            AND VS13.Cargo                  = 5
            AND VS13.NumeroCandidato        = 13
    LEFT JOIN   TSEParser_T1..VotosSecao      VS22 with (NOLOCK)
        ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS22.CodigoSecao            = SE.CodigoSecao
            AND VS22.Cargo                  = 5
            AND VS22.NumeroCandidato        = 22
    GROUP BY    M.UFSigla,
                SE.MunicipioCodigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.ModeloUrnaEletronica,
                VS13.QtdVotos,
                VS22.QtdVotos,
                SE.PR_VotosNominais

    -- DROP TABLE #DadosT2
    SELECT      M.UFSigla,
                SE.MunicipioCodigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.ModeloUrnaEletronica,
                ISNULL(VS13.QtdVotos,0) as Votos13,
                ISNULL(VS22.QtdVotos,0) as Votos22,
                SE.PR_VotosNominais as VotosValidos,
                CONVERT(numeric(18,1), CONVERT(numeric(18,4), ISNULL(VS13.QtdVotos, 1)) / CONVERT(numeric(18,4), ISNULL(VS22.QtdVotos, 1)), VS13.QtdVotos) as ProporcaoVotos
    INTO        #DadosT2
    FROM        TSEParser_T2..SecaoEleitoral  SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio       M with (NOLOCK)
        ON      M.Codigo        = SE.MunicipioCodigo
            AND SE.LogUrnaInconsistente = 0
            AND SE.ResultadoSistemaApuracao = 0
    LEFT JOIN   TSEParser_T2..VotosSecao      VS13 with (NOLOCK)
        ON      VS13.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS13.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS13.CodigoSecao            = SE.CodigoSecao
            AND VS13.Cargo                  = 5
            AND VS13.NumeroCandidato        = 13
    LEFT JOIN   TSEParser_T2..VotosSecao      VS22 with (NOLOCK)
        ON      VS22.MunicipioCodigo        = SE.MunicipioCodigo
            AND VS22.CodigoZonaEleitoral    = SE.CodigoZonaEleitoral
            AND VS22.CodigoSecao            = SE.CodigoSecao
            AND VS22.Cargo                  = 5
            AND VS22.NumeroCandidato        = 22
    GROUP BY    M.UFSigla,
                SE.MunicipioCodigo,
                SE.CodigoZonaEleitoral,
                SE.CodigoSecao,
                SE.ModeloUrnaEletronica,
                VS13.QtdVotos,
                VS22.QtdVotos,
                SE.PR_VotosNominais

    -- DROP TABLE #DadosMunicipioT1
    SELECT      T.UFSigla, 
                T.MunicipioCodigo, 
                SUM(T.Votos13) as Votos13Municipio, 
                SUM(T.Votos22) as Votos22Municipio, 
                CONVERT(numeric(18,1), CONVERT(numeric(18,4), SUM(T.Votos13)) / CONVERT(numeric(18,4), SUM(T.Votos22))) as ProporcaoMunicipio,
                CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(T.Votos22)) / SUM(T.VotosValidos)) * 100) as ProporcaoBolsonaro,
                CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(T.Votos13)) / SUM(T.VotosValidos)) * 100) as ProporcaoLula,
                COUNT(*) as QtdSecoesMunicipio,
                SUM(CASE WHEN T.Votos13 > T.Votos22 THEN 1 ELSE 0 END) as QtdSecoesLulaGanhou,
                SUM(CASE WHEN T.Votos13 < T.Votos22 THEN 1 ELSE 0 END) as QtdSecoesBolsonaroGanhou
    INTO        #DadosMunicipioT1
    FROM        #DadosT1 T
    GROUP BY    T.UFSigla, T.MunicipioCodigo

    -- DROP TABLE #DadosMunicipioT2
    SELECT      T.UFSigla, 
                T.MunicipioCodigo, 
                SUM(T.Votos13) as Votos13Municipio, 
                SUM(T.Votos22) as Votos22Municipio, 
                CONVERT(numeric(18,1), CONVERT(numeric(18,4), SUM(T.Votos13)) / CONVERT(numeric(18,4), SUM(T.Votos22))) as ProporcaoMunicipio,
                CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(T.Votos22)) / SUM(T.VotosValidos)) * 100) as ProporcaoBolsonaro,
                CONVERT(numeric(18,2), (CONVERT(numeric(18,2), SUM(T.Votos13)) / SUM(T.VotosValidos)) * 100) as ProporcaoLula,
                COUNT(*) as QtdSecoesMunicipio,
                SUM(CASE WHEN T.Votos13 > T.Votos22 THEN 1 ELSE 0 END) as QtdSecoesLulaGanhou,
                SUM(CASE WHEN T.Votos13 < T.Votos22 THEN 1 ELSE 0 END) as QtdSecoesBolsonaroGanhou
    INTO        #DadosMunicipioT2
    FROM        #DadosT2 T
    GROUP BY    T.UFSigla, T.MunicipioCodigo

END

PRINT '# Indícios de Fraude nas Eleições de 2022

**Este relatório ainda está sendo atualizado - Não utilizar estes dados para propósitos oficiais**

Este relatório tem por objetivo demonstrar alguns indícios de fraude eleitoral nas Eleições de 2022. Algumas análises que expõe situações que são difíceis de explicar senão pela ocorrência de algum tipo de fraude.

Este artigo não irá tratar dos defeitos encontrados nos arquivos das urnas. Para isso, [há outro artigo específico](https://github.com/danarrib/TSEParser/blob/master/DefeitosCarga.md).

## Seções eleitorais que apresentam comportamento muito diferente da média do município

Quando os votos válidos de um município somam 70% para um candidato X e 30% para um candidato Y, é estranho perceber que em algumas seções eleitorais deste município a situação se inverta, tendo comparativamente mais votos para o candidato que perdeu.

### Primeiro Turno
'

DECLARE @FatorDeDiferenca int = 10

SELECT @AuxInt = SUM(DiferencaVotosSecao), @AuxInt2 = Count(*) FROM (
    SELECT      SE.Votos13 - SE.Votos22 as DiferencaVotosSecao
    FROM        #DadosT1 SE
    INNER JOIN  #DadosMunicipioT1 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos > (MU.ProporcaoMunicipio * @FatorDeDiferenca)
    WHERE       SE.ProporcaoVotos > 1 AND MU.ProporcaoMunicipio < 1
) as T


PRINT '
Considerando apenas os municípios em que o Bolsonaro teve mais votos, Lula teve desempenho pelo menos ' + CONVERT(varchar(20), @FatorDeDiferenca) + ' vezes melhor do que o Bolsonaro em ' + CONVERT(varchar(20), @AuxInt2) + ' seções eleitorais, lhe garantindo ' + FORMAT(@AuxInt, '#,###', 'pt-br') + ' votos apenas nestas seções.

<details>
  <summary>Detalhamento das seções</summary>

'
DECLARE C1 CURSOR FOR
    SELECT      '- Em ' + M.Nome + ' (' + SE.UFSigla + '), Bolsonaro teve ' + FORMAT(MU.Votos22Municipio, '#,###.##', 'pt-br') + ' votos e o Lula ' + FORMAT(MU.Votos13Municipio, '#,###.##', 'pt-br') + 
                '. Bolsonaro venceu com ' + FORMAT(MU.ProporcaoBolsonaro, '#.##', 'pt-br') + '%. Mas na Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral),4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao),4) + 
                ', o Lula teve ' + CONVERT(varchar(20), SE.Votos13) + ' votos e Bolsonaro ' + CONVERT(varchar(20), SE.Votos22) + '. Neste município há ' + CONVERT(varchar(20), MU.QtdSecoesMunicipio) + 
                ' seções eleitorais, e o Lula venceu em ' + CONVERT(varchar(20), MU.QtdSecoesLulaGanhou) + '. Município código ' + RIGHT('0000' + CONVERT(varchar(20), SE.MunicipioCodigo),5) + '.' as Texto
    FROM        #DadosT1 SE
    INNER JOIN  #DadosMunicipioT1 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos > (MU.ProporcaoMunicipio * @FatorDeDiferenca)
    INNER JOIN  TSEParser_T1..Municipio M with (NOLOCK)
        ON      M.Codigo = MU.MunicipioCodigo
    WHERE       SE.ProporcaoVotos > 1 AND MU.ProporcaoMunicipio < 1
    ORDER BY    SE.UFSigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1

PRINT '</details>

'

SELECT @AuxInt = SUM(DiferencaVotosSecao), @AuxInt2 = Count(*) FROM (
    SELECT      SE.Votos22 - SE.Votos13 as DiferencaVotosSecao
    FROM        #DadosT1 SE
    INNER JOIN  #DadosMunicipioT1 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos < (MU.ProporcaoMunicipio / @FatorDeDiferenca)
    WHERE       SE.ProporcaoVotos < 1 AND MU.ProporcaoMunicipio > 1
) as T


PRINT '
Considerando apenas os municípios em que o Lula teve mais votos, Bolsonaro teve desempenho pelo menos ' + CONVERT(varchar(20), @FatorDeDiferenca) + ' vezes melhor do que o Lula em ' + CONVERT(varchar(20), @AuxInt2) + ' seções eleitorais, lhe garantindo ' + FORMAT(@AuxInt, '#,###', 'pt-br') + ' votos apenas nestas seções.

<details>
  <summary>Detalhamento das seções</summary>

'
DECLARE C1 CURSOR FOR
    SELECT      '- Em ' + M.Nome + ' (' + SE.UFSigla + '), Bolsonaro teve ' + FORMAT(MU.Votos22Municipio, '#,###.##', 'pt-br') + ' votos e o Lula ' + FORMAT(MU.Votos13Municipio, '#,###.##', 'pt-br') + 
                '. Lula venceu com ' + FORMAT(MU.ProporcaoLula, '#.##', 'pt-br') + '%. Mas na Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral),4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao),4) + 
                ', o Lula teve ' + CONVERT(varchar(20), SE.Votos13) + ' votos e Bolsonaro ' + CONVERT(varchar(20), SE.Votos22) + '. Neste município há ' + CONVERT(varchar(20), MU.QtdSecoesMunicipio) + 
                ' seções eleitorais, e o Bolsonaro venceu em ' + CONVERT(varchar(20), MU.QtdSecoesBolsonaroGanhou) + '. Município código ' + RIGHT('0000' + CONVERT(varchar(20), SE.MunicipioCodigo),5) + '.' as Texto /*,
                ' UF | Município | Zona | Seção | Votos 13 Município | Votos 22 Município | Votos 13 Seção | Votos 22 Seção | Qtd Seções Município | Qtd Seções' as Tabela */
    FROM        #DadosT1 SE
    INNER JOIN  #DadosMunicipioT1 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos < (MU.ProporcaoMunicipio / @FatorDeDiferenca)
    INNER JOIN  TSEParser_T1..Municipio M with (NOLOCK)
        ON      M.Codigo = MU.MunicipioCodigo
    WHERE       SE.ProporcaoVotos < 1 AND MU.ProporcaoMunicipio > 1
    ORDER BY    SE.UFSigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1

PRINT '</details>

### Segundo Turno
'

SELECT @AuxInt = SUM(DiferencaVotosSecao), @AuxInt2 = Count(*) FROM (
    SELECT      SE.Votos13 - SE.Votos22 as DiferencaVotosSecao
    FROM        #DadosT2 SE
    INNER JOIN  #DadosMunicipioT2 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos > (MU.ProporcaoMunicipio * @FatorDeDiferenca)
    WHERE       SE.ProporcaoVotos > 1 AND MU.ProporcaoMunicipio < 1
) as T


PRINT '
Considerando apenas os municípios em que o Bolsonaro teve mais votos, Lula teve desempenho pelo menos ' + CONVERT(varchar(20), @FatorDeDiferenca) + ' vezes melhor do que o Bolsonaro em ' + CONVERT(varchar(20), @AuxInt2) + ' seções eleitorais, lhe garantindo ' + FORMAT(@AuxInt, '#,###', 'pt-br') + ' votos apenas nestas seções.

<details>
  <summary>Detalhamento das seções</summary>

'
DECLARE C1 CURSOR FOR
    SELECT      '- Em ' + M.Nome + ' (' + SE.UFSigla + '), Bolsonaro teve ' + FORMAT(MU.Votos22Municipio, '#,###.##', 'pt-br') + ' votos e o Lula ' + FORMAT(MU.Votos13Municipio, '#,###.##', 'pt-br') + 
                '. Bolsonaro venceu com ' + FORMAT(MU.ProporcaoBolsonaro, '#.##', 'pt-br') + '%. Mas na Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral),4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao),4) + 
                ', o Lula teve ' + CONVERT(varchar(20), SE.Votos13) + ' votos e Bolsonaro ' + CONVERT(varchar(20), SE.Votos22) + '. Neste município há ' + CONVERT(varchar(20), MU.QtdSecoesMunicipio) + 
                ' seções eleitorais, e o Lula venceu em ' + CONVERT(varchar(20), MU.QtdSecoesLulaGanhou) + '. Município código ' + RIGHT('0000' + CONVERT(varchar(20), SE.MunicipioCodigo),5) + '.' as Texto
    FROM        #DadosT2 SE
    INNER JOIN  #DadosMunicipioT2 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos > (MU.ProporcaoMunicipio * @FatorDeDiferenca)
    INNER JOIN  TSEParser_T2..Municipio M with (NOLOCK)
        ON      M.Codigo = MU.MunicipioCodigo
    WHERE       SE.ProporcaoVotos > 1 AND MU.ProporcaoMunicipio < 1
    ORDER BY    SE.UFSigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1

PRINT '</details>

'

SELECT @AuxInt = SUM(DiferencaVotosSecao), @AuxInt2 = Count(*) FROM (
    SELECT      SE.Votos22 - SE.Votos13 as DiferencaVotosSecao
    FROM        #DadosT1 SE
    INNER JOIN  #DadosMunicipioT1 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos < (MU.ProporcaoMunicipio / @FatorDeDiferenca)
    WHERE       SE.ProporcaoVotos < 1 AND MU.ProporcaoMunicipio > 1
) as T


PRINT '
Considerando apenas os municípios em que o Lula teve mais votos, Bolsonaro teve desempenho pelo menos ' + CONVERT(varchar(20), @FatorDeDiferenca) + ' vezes melhor do que o Lula em ' + CONVERT(varchar(20), @AuxInt2) + ' seções eleitorais, lhe garantindo ' + FORMAT(@AuxInt, '#,###', 'pt-br') + ' votos apenas nestas seções.

<details>
  <summary>Detalhamento das seções</summary>

'
DECLARE C1 CURSOR FOR
    SELECT      '- Em ' + M.Nome + ' (' + SE.UFSigla + '), Bolsonaro teve ' + FORMAT(MU.Votos22Municipio, '#,###.##', 'pt-br') + ' votos e o Lula ' + FORMAT(MU.Votos13Municipio, '#,###.##', 'pt-br') + 
                '. Lula venceu com ' + FORMAT(MU.ProporcaoLula, '#.##', 'pt-br') + '%. Mas na Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral),4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao),4) + 
                ', o Lula teve ' + CONVERT(varchar(20), SE.Votos13) + ' votos e Bolsonaro ' + CONVERT(varchar(20), SE.Votos22) + '. Neste município há ' + CONVERT(varchar(20), MU.QtdSecoesMunicipio) + 
                ' seções eleitorais, e o Bolsonaro venceu em ' + CONVERT(varchar(20), MU.QtdSecoesBolsonaroGanhou) + '. Município código ' + RIGHT('0000' + CONVERT(varchar(20), SE.MunicipioCodigo),5) + '.' as Texto /*,
                ' UF | Município | Zona | Seção | Votos 13 Município | Votos 22 Município | Votos 13 Seção | Votos 22 Seção | Qtd Seções Município | Qtd Seções' as Tabela */
    FROM        #DadosT2 SE
    INNER JOIN  #DadosMunicipioT2 MU
        ON      MU.UFSigla = SE.UFSigla
            AND MU.MunicipioCodigo = SE.MunicipioCodigo
            AND SE.ProporcaoVotos < (MU.ProporcaoMunicipio / @FatorDeDiferenca)
    INNER JOIN  TSEParser_T2..Municipio M with (NOLOCK)
        ON      M.Codigo = MU.MunicipioCodigo
    WHERE       SE.ProporcaoVotos < 1 AND MU.ProporcaoMunicipio > 1
    ORDER BY    SE.UFSigla, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1

PRINT '</details>'