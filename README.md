# TSE Parser

![logo-tseparser](https://user-images.githubusercontent.com/17026744/202034783-e9aad24a-eaf8-47fe-bd5d-7c1a668956f5.png)

Programa que interpreta os arquivos da Urna Eletrônica brasileira (arquivos `*.imgbu`, `*.bu`, `*.rdv` e , `*.logjez`) e do Sistema de Apuração (arquivos `*.imgbusa`, `*.busa` e , `*.logsajez`) e salva os dados em um banco de dados.

![Captura de tela do TSE Parser](https://user-images.githubusercontent.com/17026744/201697053-8869cba5-e060-4628-9d81-e99a5131cd3a.png)

## TL;DR
- Dataset no formato Apache Parquet, Backup do Banco de dados SQL e programa executável podem ser baixados diretamente da [Página de Releases](https://github.com/danarrib/TSEParser/releases)
- Leva cerca de 80 horas para baixar os arquivos do TSE usando o [TSE Crawler](https://github.com/danarrib/TSECrawler). Eles consomem cerca de 130 GB em disco, e o TSE Parser leva 24 horas para processar os arquivos dos dois turnos e montar seu próprio banco de dados. Se você apenas quer os dados, recomendo usar o Dump disponibilizado, apesar dos [defeitos de carga](#problemas-ao-carregar-os-dados-do-tse).

## Sobre o TSE Parser

O programa foi escrito em C# com [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) usando o [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/pt-br/vs/community/). O banco de dados é o SQL Server 2019 Developer Edition. O Express Edition não serve pois ele limita o tamanho dos bancos de dados a 10 GB. O programa também é compatível com Postgres.

Este programa é usado em conjunto com o [TSE Crawler](https://github.com/danarrib/TSECrawler), que serve para fazer o download dos arquivos do site do TSE (Tribunal Superior Eleitoral).

**Dica:** Se tudo o que você quer é um Dump do banco de dados, [vá direto para o final](#banco-de-dados).

### O TSE Parser faz download dos arquivos?

O TSE Parser, embora seja capaz de fazer o download dos arquivos do TSE, só o faz quando o arquivo local está corrompido. Essa decisão foi tomada pois o TSE Parser faz processamento paralelo, e trabalha com dezenas de processos simultaneamente, e realizar operações de leitura e escrita neste modelo de processamento requer complexidade extra, que eu decidi não investir tempo para resolver. Além disso, os servidores do TSE não aceitam responder muitas requisições simultaneamente, então não há qualquer vantagem em realizar o download em multiplos processos.

Portanto, para baixar os arquivos, use o [TSE Crawler](https://github.com/danarrib/TSECrawler).

### O que é um arquivo .imgbu[sa] ?

É uma "Imagem" do Boletim de Urna. Ele é basicamente o mesmo texto que é impresso pela Urna Eletrônica quando a votação é encerrada. Este arquivo pode ser aberto no seu editor de texto favorito (notepad++ por exemplo) e pode ser lido normalmente. Há no entanto, alguns caracteres que não são renderizados corretamente pois são caracteres de controle da impressora da urna.

A diferença entre o `imgbu` e o `imgbusa` é que este último é gerado por um outro programa, o "Sistema de Apuração", e não pela urna eletrônica. O formato é o mesmo, apenas a origem é que é diferente.

Um exemplo completo de arquivo `imgbu` pode ser no link a seguir: [Arquivo de exemplo](https://github.com/danarrib/TSEParser/blob/master/ArquivosExemplo/o00406-0605000020824.imgbu.md).

### O que é um arquivo .bu[sa] ?

O arquivo BU é um arquivo binário que contém o Boletim de Urna. Ele contém (ou deveria conter) as mesmas informações do `imgbu`, porém no formato binário.

Decodificar este arquivo é impossível sem a documentação apropriada. Felizmente, o TRE do Mato Grosso disponibilizou a [documentação técnica do software da Urna Eletrônica](https://www.tre-mt.jus.br/eleicoes/eleicoes-2022/documentacao-tecnica-do-software-da-urna-eletronica), e entre estes documentos, estão os que descrevem a estrutura do arquivo BU, e instruções sobre como decodifica-lo. Há uma cópia desta documentação neste repositório também, no diretório TSE_Docs.

O TSE Parser contém um decodificador de arquivos BU. Se você procura por uma **biblioteca para decodificar arquivos BU**, não será difícil reutilizar o código do TSE Parser em seus projetos. Mas recomendo que ainda assim leia a documentação e aprenda sobre a [linguagem de definição de interfaces ASN.1](https://pt.wikipedia.org/wiki/ASN.1).

### O que é um arquivo .rdv ?

O arquivo RDV é um arquivo binário que contém o Registro de Votos. Ele contém cada voto individualmente digitado na urna, incluindo os votos nulos (ele salva o número digitado mesmo que o voto seja nulo), e votos brancos também. O TSE Parser processa o arquivo RDV para comparar com o Boletim de Urna. A quantidade de votos no RDV deve ser igual a do boletim de urna.

### O que é um arquivo .logjez ?

O arquivo LOGJEZ é um arquivo compactado (formato LZMA - abre no 7zip). Dentro desde arquivo compactado pode haver um ou mais arquivos de log. Esses arquivos de log são arquivos de texto gerados pela urna eletrônica durante o seu funcionamento. Cada etapa do processo eleitoral, desde a carga inicial de candidatos, verificações de hardware, cadastro de mesários, e até mesmo a digitação de títulos de eleitor e os votos para cada um dos cargos do pleito, TUDO fica registrado no log.

## Como usar o TSE Parser

1. Se você ainda não tem um servidor SQL instalado. Instale o [Microsoft SQL Server 2019 Developer Edition](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) no seu computador.

2. Faça o download da [última release](https://github.com/danarrib/TSEParser/releases) do programa.

3. Descompacte o pacote onde achar mais conveniente.

4. Abra um terminal (prompt de comando) e navegue até o diretório onde descompactou o pacote.

5. Execute o comando `TSEParser.exe -ajuda` para saber todos os parametros disponíveis na aplicação.

6. Certifique-se dos valores dos parâmetros relacionados a banco de dados (`-instancia`, `-banco`, `-usuario` e `-senha`) foram informados (se forem necessários). Se não informar, o programa tentará conectar na instância local (`.\SQL2019DEV`) usando autenticação do Windows e criará um banco de dados chamado `TSEParser_T1`.

7. Todos os arquivos .imgbu e .bu serão processados a partir do mesmo diretório onde o executável está. Caso queira especificar outro diretório, informe no parâmetro `-dir`

## Funcionamento

O TSE Parser funciona da seguinte forma:

- Especificar o diretório onde os arquivos de boletim de urna estão salvos no parâmetro `-dir`. (por padrão, é o mesmo diretório do programa)

- Especificar se deseja evitar que o programa realize uma validação adicional, comparando o arquivo `imgbu[sa]` com o `bu[sa]`. Caso o comparador encontre diferenças entre os 2 arquivos, um relatório será gerado no arquivo de Log.

- O programa tem uma lista de UFs (Unidades da Federação, estados como SP, RJ, PR, etc). Para cada UF, o programa irá:
  
  - Procurar localmente pelo JSON de configuração desta UF. Este JSON contém todos os Municípios, todas as Zonas Eleitorais e todas as Seções Eleitorais.
  
  - Cada Seção Eleitoral é uma Urna.
  
  - Para cada Seção Eleitoral, de cada Zona Eleitoral, de cada Município:
    
    - O programa irá criar uma instância de um pequeno processo "Trabalhador", que será responsável por processar os dados dessa Seção. Ver mais adiante o "Processo do Trabalhador".
    
    - O programa irá disparar todos os processos de uma mesma zona eleitoral de uma só vez, e cada um irá executar de forma assíncrona a medida que a máquina tiver recursos para executar cada processo.
    
    - Se houverem mensagens de log a salvar, o programa irá salvar agora.
    
    - Quanto todas as seções desta Zona Eleitoral estiverem processadas, o programa irá salvar as seções no banco de dados.

### Processo do trabalhador

Cada trabalhador (Worker) é responsável por processar uma única Seção Eleitoral (uma única Urna Eletrônica). O processo funciona da seguinte forma:

- Procurar localmente o JSON de configuração da Seção, que contém a lista de arquivos da Urna.

- Nesta lista de arquivos, deve haver um arquivo `imgbu[sa]`. Se não houver, o trabalhador retornará uma mensagem de erro pelo log e irá sair.

- O programa irá processar o arquivo `imgbu` a fim de obter um objeto estruturado com os dados de votação desta Seção.

- Caso a Comparação com arquivos `bu[sa]` esteja ativada, o programa irá processar o arquivo `bu[sa]` e gerar um outro objeto estruturado com a mesma estrutura do objeto anterior, e irá passar ambos os objetos (um gerado a partir do arquivo `imgbu[sa]` e o outro gerado a partir do arquivo `bu[sa]`) para uma rotina de comparação. Esta rotina compara a maioria das propriedades dos objetos (como quantidade de votos, quantidade de eleitores aptos, código de identificação da urna, etc).

- Caso a comparação retorna mensagens de inconsistência, estas mensagens serão enviadas para a rotina principal, para que sejam salvas no arquivo de log.

- Ao final do processo do trabalhador, um objeto de Boletim de Urna estruturado é retornado para o processo principal.

## Banco de dados

O banco de dados do TSE Parser é criado pelo proprio programa, utilizando o Entiry Framework. Para criar o banco de dados, basta ter uma instância local do Microsoft SQL Server 2019 Developer instalada e rodando. Ao iniciar o programa, o banco de dados será automaticamente criado e/ou atualizado.

(O programa também é compatível com Postgres)

[Documentação do Banco de dados](https://github.com/danarrib/TSEParser/blob/master/BancoDeDados.md)

### Dump (Backup) do Banco de dados

Este banco de dados levou cerca de 100 horas para ser carregado, entre download e processamento dos arquivos. Baixar um banco de dados pronto para ser utilizado vai representar uma enorme economia de tempo para você. Porém, existem alguns problemas com os dados do TSE, e alguns dados não vão estar iguais aos que são apresentados no site do TSE. Eu vou detalhar os problemas que encontrei durante a carga de dados no final deste artigo.

[Link para o download do Banco de dados](https://github.com/danarrib/TSEParser/releases).

Este arquivo é um .bak de SQL Server 2019 Developer Edition.

### Arquivo Parquet

Os dados de votação também estão disponíveis para no formato Apache Parquet, e podem ser baixados diretamente da página de [Releases](https://github.com/danarrib/TSEParser/releases).

## Problemas ao carregar os dados do TSE

Eu desenvolvi o TSE Parser e o TSE Crawler para poder baixar os dados de todas as urnas eletrônicas e poder realizar meus próprios estudos sobre estes dados. 99% dos dados estão corretos e funcionam sem problemas.

Porém, a contagem de votos para as seções eleitorais do Exterior não é igual a do TSE. A contagem de votos para os estados Brasileiros está correta.
Há um documento com os detalhes das falhas de carga. [Clique aqui para acessar](https://github.com/danarrib/TSEParser/blob/master/DefeitosCarga.md).
