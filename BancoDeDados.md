# Banco de dados do TSE Parser

O banco de dados tem a seguinte estrutura

```mermaid
erDiagram

Partido ||--|{ Candidato : ""
Regiao ||--|{ UnidadeFederativa  : ""
UnidadeFederativa ||--|{ Municipio : ""
UnidadeFederativa ||--|{ Candidato: ""
Municipio ||--|{ SecaoEleitoral: ""
Municipio ||--|{ VotosMunicipio: ""
Municipio ||--|{ DefeitosSecao: ""
SecaoEleitoral ||--|{ VotosSecao: ""
SecaoEleitoral ||--|{ VotosLog: ""
SecaoEleitoral ||--|{ VotosSecaoRDV: ""
Candidato ||--|{ VotosMunicipio: ""
Candidato ||--|{ VotosSecao: ""

Candidato {
tinyint Cargo
int NumeroCandidato
char UFSigla
varchar Nome
}
DefeitosSecao {
int MunicipioCodigo
smallint CodigoZonaEleitoral
smallint CodigoSecao
bit SemArquivo
bit Rejeitado
bit Excluido
int CodigoIdentificacaoUrnaEletronicaBU
bit ArquivoIMGBUFaltando
bit ArquivoBUFaltando
bit ArquivoRDVFaltando
bit ArquivoLOGJEZFaltando
bit ArquivoBUeIMGBUDiferentes
bit ArquivoIMGBUCorrompido
bit ArquivoBUCorrompido
bit ArquivoRDVCorrompido
bit DiferencaVotosBUeIMGBU
}
Municipio {
int Codigo
varchar Nome
char UFSigla
}
Partido {
tinyint Numero
varchar Nome
}
Regiao {
tinyint Id
varchar Nome
}
SecaoEleitoral {
int MunicipioCodigo
smallint CodigoZonaEleitoral
smallint CodigoSecao
smallint CodigoLocalVotacao
smallint EleitoresAptos
smallint Comparecimento
smallint EleitoresFaltosos
smallint HabilitadosPorAnoNascimento
int CodigoIdentificacaoUrnaEletronica
datetime2 AberturaUrnaEletronica
datetime2 FechamentoUrnaEletronica
datetime2 Zeresima
smallint DF_EleitoresAptos
smallint DF_VotosNominais
smallint DF_VotosLegenda
smallint DF_Brancos
smallint DF_Nulos
smallint DF_Total
smallint DE_EleitoresAptos
smallint DE_VotosNominais
smallint DE_VotosLegenda
smallint DE_Brancos
smallint DE_Nulos
smallint DE_Total
smallint SE_EleitoresAptos
smallint SE_VotosNominais
smallint SE_Brancos
smallint SE_Nulos
smallint SE_Total
smallint GO_EleitoresAptos
smallint GO_VotosNominais
smallint GO_Brancos
smallint GO_Nulos
smallint GO_Total
smallint PR_EleitoresAptos
smallint PR_VotosNominais
smallint PR_Brancos
smallint PR_Nulos
smallint PR_Total
bit LogUrnaInconsistente
smallint ModeloUrnaEletronica
bit ResultadoSistemaApuracao
}
UnidadeFederativa {
char Sigla
varchar Nome
tinyint IdRegiao
}
VotosLog {
int MunicipioCodigo
smallint CodigoZonaEleitoral
smallint CodigoSecao
smallint IdVotoLog
int LinhaLog
int LinhaLogFim
datetime2 InicioVoto
datetime2 HabilitacaoUrna
datetime2 FimVoto
bit PossuiBiometria
tinyint DedoBiometria
smallint ScoreBiometria
bit HabilitacaoCancelada
bit VotouDF
bit VotouDE
bit VotouSE
bit VotouGO
bit VotouPR
bit VotoNuloSuspensaoDF
bit VotoNuloSuspensaoDE
bit VotoNuloSuspensaoSE
bit VotoNuloSuspensaoGO
bit VotoNuloSuspensaoPR
bit VotoComputado
tinyint QtdTeclasIndevidas
bit EleitorSuspenso
smallint ModeloUrnaEletronica
}
VotosMunicipio {
int MunicipioCodigo
tinyint Cargo
int NumeroCandidato
bigint QtdVotos
bit VotoLegenda
}
VotosSecao {
int MunicipioCodigo
smallint CodigoZonaEleitoral
smallint CodigoSecao
tinyint Cargo
int NumeroCandidato
smallint QtdVotos
bit VotoLegenda
}
VotosSecaoRDV {
int MunicipioCodigo
smallint CodigoZonaEleitoral
smallint CodigoSecao
smallint IdVotoRDV
tinyint Cargo
int NumeroCandidato
smallint QtdVotos
bit VotoLegenda
bit VotoNulo
bit VotoBranco
}
```

A tabela `VotosMunicipio` uma tabela consolidada com os votos agrupados por município. 
Embora os mesmos dados pudessem ser obtidos da tabela `VotosSecao`, ter uma tabela com estes dados agrupados agiliza algumas consultas.

### Algumas consultas úteis

Votos para Presidente no PT e no PR, por UF.
```
SELECT      M.UFSigla,
            VM.NumeroCandidato,
            SUM(VM.QtdVotos) as QtdVotos
FROM        Municipio M with (NOLOCK)
INNER JOIN  VotosMunicipio VM with (NOLOCK)
    ON      VM.MunicipioCodigo = M.Codigo
        AND VM.Cargo = 5
        AND VM.NumeroCandidato IN (13, 22)
GROUP BY    M.UFSigla, VM.NumeroCandidato
ORDER BY    M.UFSigla, SUM(VM.QtdVotos) DESC
```

