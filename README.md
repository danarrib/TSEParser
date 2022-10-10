# TSE Parser

Programa que interpreta os Boletins de Urna (arquivos `*.imgbu[sa]` e `*.bu[sa]`) e salva os dados em um banco de dados.

O programa foi escrito em C# com [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) usando o [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/pt-br/vs/community/). O banco de dados é o SQL Server 2012 Express Edition.

Este programa é usado em conjunto com o [TSE Crawler](https://github.com/danarrib/TSECrawler), que serve para fazer o download dos arquivos do site do TSE (Tribunal Superior Eleitoral).

Dica: Se tudo o que você quer é um Dump do banco de dados, [vá direto para o final](#banco-de-dados).

### O TSE Parser faz download dos arquivos?

O TSE Parser, embora seja capaz de fazer o download dos arquivos do TSE, só o faz quando o arquivo local está corrompido. Essa decisão foi tomada pois o TSE Parser faz processamento paralelo, e trabalha com dezenas de processos simultaneamente, e realizar operações de leitura e escrita neste modelo de processamento requer complexidade extra, que eu decidi não investir tempo para resolver. Além disso, os servidores do TSE não aceitam responder muitas requisições simultaneamente, então não há qualquer vantagem em realizar o download em multiplos processos.

### O que é um arquivo .imgbu[sa] ?

É uma "Imagem" do Boletim de Urna. Ele é basicamente o mesmo texto que é impresso pela Urna Eletrônica quando a votação é encerrada. Este arquivo pode ser aberto no seu editor de texto favorito e pode ser lido normalmente. Há no entanto, alguns caracteres que não são renderizados corretamente pois são caracteres de controle da impressora de cupons da urna.

A diferença entre o `imgbu` e o `imgbusa` é que este último é gerado por um outro sistema, o "Sistema de Apuração", e não pela urna eletrônica. O formato é o mesmo, apenas a origem é que é diferente.

Um exemplo de arquivo imgbu pode ser visto em [Arquivos de exemplo](https://github.com/danarrib/TSEParser/tree/master/ArquivosExemplo)

### O que é um arquivo .bu[sa]

O arquivo BU é um arquivo binário que contém o Boletim de Urna. Ele contém (ou deveria conter) as mesmas informações do `imgbu`, porém no formato binário.

Decodificar este arquivo é impossível sem a documentação apropriada. Felizmente, o TRE do Mato Grosso disponibilizou a [documentação técnica do software da Urna Eletrônica](https://www.tre-mt.jus.br/eleicoes/eleicoes-2022/documentacao-tecnica-do-software-da-urna-eletronica), e entre estes documentos, estão os que descrevem a estrutura do arquivo BU, e instruções sobre como decodifica-lo.

O TSE Parser contém um decodificador de arquivos BU. Se você procura por uma **biblioteca para decodificar arquivos BU**, não será difícil reutilizar o código do TSE Parser em seus projetos. Mas recomendo que ainda assim leia a documentação e aprenda sobre a [linguagem de definição de interfaces ASN.1](https://pt.wikipedia.org/wiki/ASN.1).

### Funcionamento

O TSE Parser funciona da seguinte forma:

- Especificar o diretório onde os arquivos de boletim de urna estão salvos na constante `diretorioLocalDados`. (por padrão, é `D:\Downloads\Urnas\`)

- Especificar se deseja que o programa realize uma validação adicional, comparando o arquivo `imgbu[sa]` com o `bu[sa]`. Caso o comparador encontre diferenças entre os 2 arquivos, um relatório será gerado no arquivo de Log.

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

O banco de dados do TSE Parser é criado pelo proprio programa, utilizando o Entiry Framework. Para criar o banco de dados, basta ter uma instância local do Microsoft SQL Server Express instalada e rodando, e rodar os seguinte comando no Package Manager Console do Visual Studio 2022:

```
PM> Update-Database
```

Este comando irá criar um banco de dados chamado `Eleicoes2022` na sua instância local do SQL Server, e todas as tabelas necessárias.

### Dump (Backup) do Banco de dados

Este banco de dados levou cerca de 100 horas para ser carregado, entre download e processamento dos arquivos. Baixar um banco de dados pronto para ser utilizado vai representar uma enorme economia de tempo para você. Porém, existem alguns problemas com os dados do TSE, e alguns dados não vão estar iguais aos que são apresentados no site do TSE. Eu vou detalhar os problemas que encontrei durante a carga de dados no final deste artigo.

[Link para o download do Banco de dados](https://drive.google.com/file/d/1JkzsdZ-qiWO6_PnPCIY8TAynOYmTc-kk/view?usp=sharing).

Este arquivo é um .bak de SQL Server 2012 Express Edition (Versão 11.0.2100.60 (X64))

## Problemas ao carregar os dados do TSE

Eu desenvolvi o TSE Parser e o TSE Crawler para poder baixar os dados de todas as urnas eletrônicas e poder realizar meus próprios estudos sobre estes dados. 99% dos dados estão corretos e funcionam sem problemas.

Porém, o site do TSE reporta que existem 472.075 seções no total. O TSE Crawler, no entanto, só encontrou 471.970 seções eleitorais, 105 a menos do que o reportado pelo TSE. Destas 105, 51 são certamente defeitos de carregamento de seções do exterior, mas as 54 seções restantes eu simplesmente não consegui localizar.

Eu consegui corrigir os 2 defeitos em seções do Brasil, então a contagem de votos em todos os estados do Brasil está correta. Porém, nas urnas do Exterior, a contagem não está correta.

#### Erros no Brasil:

1. Unidade Federativa MA (Maranhão), Município São Matheus do Maranhão (09237), Zona Eleitoral 0084, Seção 215. Esta Seção não possui arquivo `imgbu`. Os dados foram recuperados a partir do arquivo `BU`. A contagem de votos deste estado está correta, portanto os dados no banco de dados estão válidos para este estado.

2. Unidade Federativa RS (Rio Grande do Sul), Município Porto Alegre (88013), Zona Eleitoral 0002, Seção 0199. Os arquivos `imgbu` e `BU` não parecem ter sido gerados pela mesma Urna Eletrônica. no arquivo `imgbu` o código de identificação da Urna Eletrônica é "02215062", e no arquivo `BU`, o código é "02228407". Embora o número do município, zona eleitoral, seção e local de votação sejam os mesmos em ambos os arquivos, todos os demais dados são diferentes, incluindo contagem de votos e listas de candidados. **É impossível determinar qual dos dois arquivos é o que, de fato, representa a votação desta seção eleitoral.** A fim de manter o banco de dados fielmente igual ao site do TSE, eu decidi importar para esta seção o arquivo `BU` em vez do `imgbu`.

#### Erros no Exterior (Unidade Federativa "ZZ")

Existem basicamente 3 tipos de defeito: 

- Sem arquivo

- Rejeitado

- Excluído

Nos 3 casos, não há arquivo `BU` nem `imgbu`. **Mesmo pelo site do TSE, estas seções não são exibidas**. Eu sinceramente não sei como que o TSE chegou nos números totais que são apresentados no site para a apuração das urnas do exterior.

1. UF ZZ MUN 29637 DAMASCO ZN 0001 SE 0727 - Hash Situação: Sem arquivo.

2. UF ZZ MUN 29912 LAGOS ZN 0001 SE 0150 - Hash Situação: Rejeitado.

3. UF ZZ MUN 38903 CONACRI ZN 0001 SE 0603 - Hash Situação: Sem arquivo.

4. UF ZZ MUN 99236 PUERTO QUIJARRO ZN 0001 SE 2887 - Hash Situação: Sem arquivo.

5. UF ZZ MUN 99236 PUERTO QUIJARRO ZN 0001 SE 0815 - Hash Situação: Sem arquivo.

6. UF ZZ MUN 29769 HARARE ZN 0001 SE 0126 - Hash Situação: Sem arquivo.

7. UF ZZ MUN 30988 SARAJEVO ZN 0001 SE 1354 - Hash Situação: Sem arquivo.

8. UF ZZ MUN 30988 SARAJEVO ZN 0001 SE 0725 - Hash Situação: Sem arquivo.

9. UF ZZ MUN 38989 IEREVAN ZN 0001 SE 1841 - Hash Situação: Sem arquivo.

10. UF ZZ MUN 39128 BAKU ZN 0001 SE 3208 - Hash Situação: Sem arquivo.

11. UF ZZ MUN 99104 TBILISI ZN 0001 SE 1011 - Hash Situação: Sem arquivo.

12. UF ZZ MUN 99147 SAINT JOHNS ZN 0001 SE 3049 - Hash Situação: Sem arquivo.

13. UF ZZ MUN 99171 BAGDÁ ZN 0001 SE 3126 - Hash Situação: Sem arquivo.

14. UF ZZ MUN 99333 ST GEORGES DE LOYAPOCK ZN 0001 SE 3188 - Hash Situação: Sem arquivo.

15. UF ZZ MUN 29300 ARGEL ZN 0001 SE 0006 - Hash Situação: Sem arquivo.

16. UF ZZ MUN 29424 BRIDGETOWN ZN 0001 SE 0054 - Hash Situação: Sem arquivo.

17. UF ZZ MUN 29815 IAUNDÊ ZN 0001 SE 0140 - Hash Situação: Sem arquivo.

18. UF ZZ MUN 29831 ISLAMABADE ZN 0001 SE 1983 - Hash Situação: Sem arquivo.

19. UF ZZ MUN 29963 LOMÉ ZN 0001 SE 0180 - Hash Situação: Sem arquivo.

20. UF ZZ MUN 30669 GABORONE ZN 0001 SE 0546 - Hash Situação: Sem arquivo.

21. UF ZZ MUN 99210 COBIJA ZN 0001 SE 3013 - Hash Situação: Sem arquivo.

22. UF ZZ MUN 99341 LILONGUE ZN 0001 SE 1308 - Hash Situação: Sem arquivo.

23. UF ZZ MUN 29505 CARACAS ZN 0001 SE 0079 - Hash Situação: Excluído.

24. UF ZZ MUN 29505 CARACAS ZN 0001 SE 0078 - Hash Situação: Excluído.

25. UF ZZ MUN 29521 CHUY ZN 0001 SE 0083 - Hash Situação: Sem arquivo.

26. UF ZZ MUN 29564 CIUDAD GUAYANA ZN 0001 SE 0091 - Hash Situação: Sem arquivo.

27. UF ZZ MUN 38881 BELMOPAN ZN 0001 SE 0548 - Hash Situação: Sem arquivo.

28. UF ZZ MUN 99139 TIRANA ZN 0001 SE 3120 - Hash Situação: Sem arquivo.

29. UF ZZ MUN 29939 LIBREVILLE ZN 0001 SE 0152 - Hash Situação: Sem arquivo.

30. UF ZZ MUN 30686 TRÍPOLI ZN 0001 SE 0443 - Hash Situação: Sem arquivo.

31. UF ZZ MUN 99384 CASTRIES ZN 0001 SE 2876 - Hash Situação: Sem arquivo.

32. UF ZZ MUN 29858 KIEV ZN 0001 SE 1183 - Hash Situação: Sem arquivo.

33. UF ZZ MUN 30929 COLOMBO ZN 0001 SE 0722 - Hash Situação: Sem arquivo.

34. UF ZZ MUN 38920 COTONOU ZN 0001 SE 0638 - Hash Situação: Sem arquivo.

35. UF ZZ MUN 99198 ABUJA ZN 0001 SE 2947 - Hash Situação: Sem arquivo.

36. UF ZZ MUN 99244 RIVERA ZN 0001 SE 3101 - Hash Situação: Sem arquivo.

37. UF ZZ MUN 99244 RIVERA ZN 0001 SE 2917 - Hash Situação: Sem arquivo.

38. UF ZZ MUN 29416 BOSTON ZN 0001 SE 0038 - Hash Situação: Sem arquivo.

39. UF ZZ MUN 39241 ASTANA ZN 0001 SE 0714 - Hash Situação: Sem arquivo.

40. UF ZZ MUN 39263 MALABO ZN 0001 SE 0961 - Hash Situação: Sem arquivo.

41. UF ZZ MUN 39284 UAGADUGU ZN 0001 SE 0773 - Hash Situação: Sem arquivo.

42. UF ZZ MUN 99252 VATICANO ZN 0001 SE 3288 - Hash Situação: Sem arquivo.

43. UF ZZ MUN 99325 ADIS ABEBA ZN 0001 SE 3016 - Hash Situação: Sem arquivo.

44. UF ZZ MUN 99376 YANGON ZN 0001 SE 1315 - Hash Situação: Sem arquivo.

45. UF ZZ MUN 29823 IQUITOS ZN 0001 SE 0141 - Hash Situação: Sem arquivo.

46. UF ZZ MUN 30295 PASO LOS LIBRES ZN 0001 SE 0348 - Hash Situação: Sem arquivo.

47. UF ZZ MUN 39187 BRAZZAVILLE ZN 0001 SE 3011 - Hash Situação: Sem arquivo.

48. UF ZZ MUN 39187 BRAZZAVILLE ZN 0001 SE 1428 - Hash Situação: Sem arquivo.

49. UF ZZ MUN 39187 BRAZZAVILLE ZN 0001 SE 0624 - Hash Situação: Sem arquivo.

50. UF ZZ MUN 39225 SÃO TOMÉ ZN 0001 SE 0931 - Hash Situação: Sem arquivo.

51. UF ZZ MUN 99350 BAMAKO ZN 0001 SE 1236 - Hash Situação: Sem arquivo.


