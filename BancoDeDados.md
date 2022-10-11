# Banco de dados do TSE Parser

O banco de dados tem a seguinte estrutura

```mermaid
erDiagram
Partido ||--|{ Candidato : ""
UnidadeFederativa ||--|{ Municipio : ""
UnidadeFederativa ||--|{ Candidato: ""
Municipio ||--|{ SecaoEleitoral: ""
Municipio ||--|{ VotosMunicipio: ""
Municipio ||--|{ VotosSecao: ""
Candidato ||--|{ VotosMunicipio: ""
Candidato ||--|{ VotosSecao: ""
Candidato {
tinyint Cargo PK
int NumeroCandidato PK
char UFSigla PK
varchar Nome
}
Municipio {
int Codigo PK
varchar Nome
char UFSigla
}
Partido {
tinyint Numero PK
varchar Nome
}
SecaoEleitoral {
int MunicipioCodigo PK
smallint CodigoZonaEleitoral PK
smallint CodigoSecao PK
smallint CodigoLocalVotacao
smallint EleitoresAptos
smallint Comparecimento
smallint EleitoresFaltosos
smallint HabilitadosPorAnoNascimento
int CodigoIdentificacaoUrnaEletronica
datetime2 AberturaUrnaEletronica
datetime2 FechamentoUrnaEletronica
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
}
UnidadeFederativa {
char Sigla PK
varchar Nome
}
VotosMunicipio {
int MunicipioCodigo PK
tinyint Cargo PK
int NumeroCandidato PK
bigint QtdVotos
bit VotoLegenda
}
VotosSecao {
int MunicipioCodigo PK
smallint CodigoZonaEleitoral PK
smallint CodigoSecao PK
tinyint Cargo PK
int NumeroCandidato PK
smallint QtdVotos
bit VotoLegenda
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

