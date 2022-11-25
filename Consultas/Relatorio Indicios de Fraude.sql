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


    -- Votos depois do horário
    DROP TABLE  #VotosDepoisDoHorario
    SELECT      R.Id as RegiaoId, R.Nome as Regiao, UF.Sigla, UF.Nome as NomeUF, M.Codigo, M.Nome as Municipio, SE.CodigoZonaEleitoral, SE.CodigoSecao,
                COUNT(*) as QtdVotos, MAX(VL.InicioVoto) as UltimoVoto, DATEDIFF(MI, DATEADD(hh, M.FusoHorario, CONVERT(datetime, '2022-10-02 17:00:00')), MAX(VL.InicioVoto)) as MinutosAlemDoHorario,
                1 as Turno
    INTO        #VotosDepoisDoHorario
    FROM        TSEParser_T1..SecaoEleitoral    SE with (NOLOCK)
    INNER JOIN  TSEParser_T1..Municipio         M with (NOLOCK)
        ON      M.Codigo                = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T1..VotosLog          VL with (NOLOCK)
        ON      VL.MunicipioCodigo      = SE.MunicipioCodigo
            AND VL.CodigoZonaEleitoral  = SE.CodigoZonaEleitoral
            AND VL.CodigoSecao          = SE.CodigoSecao
            AND VL.VotoComputado        = 1
            AND VL.InicioVoto           > DATEADD(hh, M.FusoHorario, CONVERT(datetime, '2022-10-02 17:00:00'))
    INNER JOIN  TSEParser_T1..UnidadeFederativa UF with(NOLOCK)
        ON      UF.Sigla                = M.UFSigla
    INNER JOIN  TSEParser_T1..Regiao            R with (NOLOCK)
        ON      R.Id                    = UF.RegiaoId
    GROUP BY    R.Id, R.Nome, UF.Sigla, UF.Nome, M.Codigo, M.FusoHorario, M.Nome, SE.CodigoZonaEleitoral, SE.CodigoSecao

    INSERT INTO #VotosDepoisDoHorario
    SELECT      R.Id as RegiaoId, R.Nome as Regiao, UF.Sigla, UF.Nome as NomeUF, M.Codigo, M.Nome as Municipio, SE.CodigoZonaEleitoral, SE.CodigoSecao,
                COUNT(*) as QtdVotos, MAX(VL.InicioVoto) as UltimoVoto, DATEDIFF(MI, DATEADD(hh, M.FusoHorario, CONVERT(datetime, '2022-10-30 17:00:00')), MAX(VL.InicioVoto)) as MinutosAlemDoHorario,
                2 as Turno
    FROM        TSEParser_T2..SecaoEleitoral    SE with (NOLOCK)
    INNER JOIN  TSEParser_T2..Municipio         M with (NOLOCK)
        ON      M.Codigo                = SE.MunicipioCodigo
    INNER JOIN  TSEParser_T2..VotosLog          VL with (NOLOCK)
        ON      VL.MunicipioCodigo      = SE.MunicipioCodigo
            AND VL.CodigoZonaEleitoral  = SE.CodigoZonaEleitoral
            AND VL.CodigoSecao          = SE.CodigoSecao
            AND VL.VotoComputado        = 1
            AND VL.InicioVoto           > DATEADD(hh, M.FusoHorario, CONVERT(datetime, '2022-10-30 17:00:00'))
    INNER JOIN  TSEParser_T2..UnidadeFederativa UF with(NOLOCK)
        ON      UF.Sigla                = M.UFSigla
    INNER JOIN  TSEParser_T2..Regiao            R with (NOLOCK)
        ON      R.Id                    = UF.RegiaoId
    GROUP BY    R.Id, R.Nome, UF.Sigla, UF.Nome, M.Codigo, M.Nome, M.FusoHorario, SE.CodigoZonaEleitoral, SE.CodigoSecao


END

PRINT '# Indícios de Fraude nas Eleições de 2022

**Este relatório ainda está sendo atualizado - Não utilizar estes dados para propósitos oficiais**

Este relatório tem por objetivo demonstrar alguns indícios de fraude eleitoral nas Eleições de 2022. Algumas análises que expõe situações que são difíceis de explicar senão pela ocorrência de algum tipo de fraude.

Este artigo não irá tratar dos defeitos encontrados nos arquivos das urnas. Para isso, [há outro artigo específico](https://github.com/danarrib/TSEParser/blob/master/DefeitosCarga.md).
'
/*
PRINT '## Seções eleitorais que apresentam comportamento muito diferente da média do município

Quando os votos válidos de um município somam 70% para um candidato X e 30% para um candidato Y, é estranho perceber que em algumas seções eleitorais deste município a situação se inverta, tendo comparativamente mais votos para o candidato que perdeu.

Isso não quer dizer, necessariamente, que houve uma fraude. É apenas uma situação atípica. 

Por exemplo: Em **Confresa, no Mato Grosso**, no primeiro turno, Bolsonaro teve **10.371** e Lula teve **4.802**.

Bolsonaro venceu nesta cidade com **65,71%** dos votos válidos. Lula teve **30,17%** dos votos válidos.

Das 64 seções eleitorais desta cidade, em uma única seção o Lula teve mais votos que o Bolsonaro. Foi a Zona 0028, Seção 0158. Nesta seção eleitoral **TODOS os 375 votos para presidente foram para o Lula**. Nenhum outro candidato recebeu votos e não houve votos brancos nem nulos para presidente nesta seção.

No segundo turno, a situação se reperiu. Foram 383 votos para o Lula, e 1 voto nulo.

É importante observar, no entanto, que esta seção eleitoral em particular fica na Aldeia indígena Urubu Branco ([fonte](https://www.confresa.org/portal/noticias/0/3/468/confresa-tem-22-mil-eleitores-aptos-a-votar-nas-eleicoes-2022-saiba-os-locais-de-votacao), [arquivo](https://archive.ph/MKuc3)), o que pode explicar a desconexão entre os votos observados no resto da cidade.

Infelizmente não há uma tabela com os locais de votação disponível. Se houvesse, seria possível cruzar os dados e tentar explicar a razão de certas seções eleitorais apresentarem desvios tão grandes em relação a média do município.

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
                '. Bolsonaro venceu com ' + FORMAT(MU.ProporcaoBolsonaro, '#.##', 'pt-br') + '% (contra ' + FORMAT(MU.ProporcaoLula, '#.##', 'pt-br') + '% do Lula). Mas na Zona ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoZonaEleitoral),4) + ', Seção ' + RIGHT('000' + CONVERT(varchar(20), SE.CodigoSecao),4) + 
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

PRINT '</details>
'
*/
SELECT @AuxInt = SUM(QtdVotos) FROM #VotosDepoisDoHorario WHERE Turno = 1
SELECT @AuxInt2 = SUM(QtdVotos) FROM #VotosDepoisDoHorario WHERE Turno = 2

PRINT '
## Votos computados após o horário regular

O horário regular para a abertura da votação é as 8:00, e o encerramento da votação é as 17:00, horário de brasília. 

Este horário deve ser respeitado inclusive para as localidades que tem fuso-horário diferente, como é o caso do Acre e oeste do Amazonas (Brasilia -2 horas), Amazonas, Rondônia, Roraima, Mato Grosso e Mato Grosso do Sul (Brasília -1 hora) e Fernando de Noronha (Brasília +1 hora).

'

PRINT 'No entanto, **' + FORMAT(@AuxInt, '#,###', 'pt-br') + '** de votos no primeiro turno e **' + FORMAT(@AuxInt2, '#,###', 'pt-br') + '** votos no segundo turno foram computados após as 17:00.

### Seções e votos computados após o horário regular, por região, primeiro e segundo turnos:

| Região | Qtd Seções T1 | Qtd Seções T2 | Qtd Votos T1 | Qtd Votos T2 |
| --- | ---: | ---: | ---: | ---: |
'

DECLARE C1 CURSOR FOR
    SELECT      '| ' + R.Nome + ' | ' + FORMAT(S1.QtdSecoes, '#,###', 'pt-br') + ' | ' + FORMAT(S2.QtdSecoes, '#,###', 'pt-br') + ' | ' + FORMAT(S1.QtdVotos, '#,###', 'pt-br') + ' | ' + FORMAT(S2.QtdVotos, '#,###', 'pt-br') + ' |' as Tabela
    FROM        TSEParser_T1..Regiao R with (NOLOCK)
    INNER JOIN  (SELECT T1.RegiaoId, COUNT(*) As QtdSecoes, SUM(T1.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T1 WHERE T1.Turno = 1 GROUP BY T1.RegiaoId) S1
        ON      S1.RegiaoId = R.Id
    INNER JOIN  (SELECT T2.RegiaoId, COUNT(*) As QtdSecoes, SUM(T2.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T2 WHERE T2.Turno = 2 GROUP BY T2.RegiaoId) S2
        ON      S2.RegiaoId = R.Id
    ORDER BY    R.Nome
OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1

DECLARE C1 CURSOR FOR
    SELECT      '| **' + R.Nome + '** | **' + FORMAT(S1.QtdSecoes, '#,###', 'pt-br') + '** | **' + FORMAT(S2.QtdSecoes, '#,###', 'pt-br') + '** | **' + FORMAT(S1.QtdVotos, '#,###', 'pt-br') + '** | **' + FORMAT(S2.QtdVotos, '#,###', 'pt-br') + '** |' as Tabela
    FROM        (VALUES(0, 'Total')) AS R(Id, Nome)
    INNER JOIN  (SELECT 0 as RegiaoId, COUNT(*) As QtdSecoes, SUM(T1.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T1 WHERE T1.Turno = 1) S1
        ON      S1.RegiaoId = R.Id
    INNER JOIN  (SELECT 0 as RegiaoId, COUNT(*) As QtdSecoes, SUM(T2.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T2 WHERE T2.Turno = 2) S2
        ON      S2.RegiaoId = R.Id
OPEN C1
FETCH NEXT FROM C1 INTO @AuxVarchar

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT @AuxVarchar
    FETCH NEXT FROM C1 INTO @AuxVarchar
END
CLOSE C1
DEALLOCATE C1


SELECT      R.Nome, 
            S1.QtdSecoes     as QtdSecoesT1, 
            S2.QtdSecoes     as QtdSecoesT2, 
            S1.QtdVotos     as QtdVotosT1, 
            S2.QtdVotos     as QtdVotosT2
FROM        (VALUES(0, 'Total')) AS R(Id, Nome)
LEFT JOIN  (SELECT 0 as RegiaoId, COUNT(*) As QtdSecoes, SUM(T1.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T1 WHERE T1.Turno = 1) S1
    ON      S1.RegiaoId = R.Id
LEFT JOIN  (SELECT 0 as RegiaoId, COUNT(*) As QtdSecoes, SUM(T2.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T2 WHERE T2.Turno = 2) S2
    ON      S2.RegiaoId = R.Id
WHERE       R.Nome <> 'Brasil'

SELECT      R.Nome, 
            S1.QtdSecoes     as QtdSecoesT1, 
            S2.QtdSecoes     as QtdSecoesT2, 
            S1.QtdVotos     as QtdVotosT1, 
            S2.QtdVotos     as QtdVotosT2
FROM        TSEParser_T1..Regiao R with (NOLOCK)
LEFT JOIN  (SELECT T1.RegiaoId, COUNT(*) As QtdSecoes, SUM(T1.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T1 WHERE T1.Turno = 1 GROUP BY T1.RegiaoId) S1
    ON      S1.RegiaoId = R.Id
LEFT JOIN  (SELECT T2.RegiaoId, COUNT(*) As QtdSecoes, SUM(T2.QtdVotos) as QtdVotos FROM #VotosDepoisDoHorario T2 WHERE T2.Turno = 2 GROUP BY T2.RegiaoId) S2
    ON      S2.RegiaoId = R.Id
WHERE       R.Nome <> 'Brasil'
ORDER BY    R.Nome
