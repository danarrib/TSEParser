# Defeitos nos arquivos do TSE

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

### Primeiro Turno

- UF AM - Quantidade de seções eleitorais carregadas (7453) é diferente do TSE (7454).
- UF ZZ - Quantidade de seções eleitorais carregadas (1014) é diferente do TSE (1064).
- UF ZZ - Quantidade de votos válidos carregados (304027) é diferente do TSE (304032).

### Segundo Turno

- UF AM - Quantidade de seções eleitorais carregadas (7453) é diferente do TSE (7454).
- UF ZZ - Quantidade de seções eleitorais carregadas (1017) é diferente do TSE (1064).

## Mais votos no Boletim de Urna do que no Log da Urna

O **Boletim de urna** é o documento oficial que comprova quantos votos cada candidato obteve naquela urna específica. E o **Log de Urna** é um arquivo de texto gerado pela urna com cada operação realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Então se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o número de votos do arquivo log precisa ser igual ao número de votos apresentado pelo Boletim de Urna, pois do contrário, não há como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em dúvida.

Abaixo são listadas todas as seções eleitorais em que o Boletim de Urna apresenta MAIS VOTOS do que votos contados no Log da Urna.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF AL (ALAGOAS), Município 27219 (BRANQUINHA), Zona 0009, Seção 0005 - Votações no BU: 242, Votações no Log: 241.
- UF BA (BAHIA), Município 30007 (LUÍS EDUARDO MAGALHÃES), Zona 0205, Seção 0210 - Votações no BU: 189, Votações no Log: 9.
- UF BA (BAHIA), Município 35696 (IGAPORÃ), Zona 0168, Seção 0142 - Votações no BU: 287, Votações no Log: 259.
- UF BA (BAHIA), Município 39594 (VALENTE), Zona 0120, Seção 0024 - Votações no BU: 256, Votações no Log: 255.
- UF CE (CEARÁ), Município 14117 (IGUATU), Zona 0013, Seção 0042 - Votações no BU: 234, Votações no Log: 233.
- UF CE (CEARÁ), Município 15377 (RUSSAS), Zona 0009, Seção 0301 - Votações no BU: 253, Votações no Log: 252.
- UF DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0017, Seção 0090 - Votações no BU: 274, Votações no Log: 261.
- UF ES (ESPÍRITO SANTO), Município 56189 (SOORETAMA), Zona 0041, Seção 0226 - Votações no BU: 230, Votações no Log: 229.
- UF ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0126 - Votações no BU: 251, Votações no Log: 250.
- UF ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0511 - Votações no BU: 289, Votações no Log: 288.
- UF GO (GOIÁS), Município 92916 (CAMPESTRE DE GOIÁS), Zona 0020, Seção 0107 - Votações no BU: 190, Votações no Log: 189.
- UF MA (MARANHÃO), Município 07064 (APICUM-AÇU), Zona 0107, Seção 0157 - Votações no BU: 279, Votações no Log: 278.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0166 - Votações no BU: 182, Votações no Log: 181.
- UF MA (MARANHÃO), Município 07617 (CHAPADINHA), Zona 0042, Seção 0098 - Votações no BU: 301, Votações no Log: 300.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0021 - Votações no BU: 224, Votações no Log: 223.
- UF MA (MARANHÃO), Município 09210 (SÃO LUÍS), Zona 0076, Seção 0048 - Votações no BU: 297, Votações no Log: 296.
- UF MG (MINAS GERAIS), Município 45136 (FELIXLÂNDIA), Zona 0100, Seção 0231 - Votações no BU: 281, Votações no Log: 252.
- UF MG (MINAS GERAIS), Município 50350 (POÇOS DE CALDAS), Zona 0350, Seção 0147 - Votações no BU: 180, Votações no Log: 179.
- UF MS (MATO GROSSO DO SUL), Município 98035 (COSTA RICA), Zona 0038, Seção 0008 - Votações no BU: 193, Votações no Log: 158.
- UF PA (PARÁ), Município 04154 (ANANINDEUA), Zona 0072, Seção 0012 - Votações no BU: 318, Votações no Log: 317.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0024 - Votações no BU: 312, Votações no Log: 119.
- UF PA (PARÁ), Município 05177 (PORTO DE MOZ), Zona 0082, Seção 0062 - Votações no BU: 249, Votações no Log: 248.
- UF PE (PERNAMBUCO), Município 23434 (BOM JARDIM), Zona 0033, Seção 0036 - Votações no BU: 239, Votações no Log: 238.
- UF PR (PARANÁ), Município 76678 (LONDRINA), Zona 0041, Seção 0318 - Votações no BU: 236, Votações no Log: 235.
- UF PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0100 - Votações no BU: 326, Votações no Log: 23.
- UF PR (PARANÁ), Município 84611 (SARANDI), Zona 0206, Seção 0069 - Votações no BU: 263, Votações no Log: 262.
- UF RJ (RIO DE JANEIRO), Município 58394 (ITAGUAÍ), Zona 0105, Seção 0040 - Votações no BU: 208, Votações no Log: 189.
- UF RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0065, Seção 0041 - Votações no BU: 271, Votações no Log: 270.
- UF RJ (RIO DE JANEIRO), Município 58971 (SÃO GONÇALO), Zona 0036, Seção 0007 - Votações no BU: 264, Votações no Log: 243.
- UF RJ (RIO DE JANEIRO), Município 59013 (SÃO JOÃO DE MERITI), Zona 0186, Seção 0147 - Votações no BU: 215, Votações no Log: 8.
- UF RN (RIO GRANDE DO NORTE), Município 18953 (VERA CRUZ), Zona 0007, Seção 0123 - Votações no BU: 267, Votações no Log: 266.
- UF RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0002, Seção 0199 - Votações no BU: 288, Votações no Log: 196.
- UF RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0114, Seção 0336 - Votações no BU: 259, Votações no Log: 27.
- UF RS (RIO GRANDE DO SUL), Município 89052 (SEBERI), Zona 0132, Seção 0063 - Votações no BU: 307, Votações no Log: 305.
- UF RS (RIO GRANDE DO SUL), Município 89192 (SOLEDADE), Zona 0054, Seção 0009 - Votações no BU: 253, Votações no Log: 252.
- UF SC (SANTA CATARINA), Município 81434 (IMBITUBA), Zona 0073, Seção 0134 - Votações no BU: 244, Votações no Log: 243.
- UF SC (SANTA CATARINA), Município 83275 (SÃO JOSÉ), Zona 0084, Seção 0022 - Votações no BU: 241, Votações no Log: 240.
- UF SE (SERGIPE), Município 31690 (LAGARTO), Zona 0012, Seção 0077 - Votações no BU: 209, Votações no Log: 208.
- UF SP (SÃO PAULO), Município 61654 (ARARAS), Zona 0014, Seção 0206 - Votações no BU: 342, Votações no Log: 341.
- UF SP (SÃO PAULO), Município 63070 (CAPELA DO ALTO), Zona 0140, Seção 0002 - Votações no BU: 278, Votações no Log: 277.
- UF SP (SÃO PAULO), Município 64270 (FRANCISCO MORATO), Zona 0367, Seção 0018 - Votações no BU: 259, Votações no Log: 253.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0394, Seção 0152 - Votações no BU: 339, Votações no Log: 338.
- UF SP (SÃO PAULO), Município 64939 (IBITINGA), Zona 0049, Seção 0212 - Votações no BU: 266, Votações no Log: 265.
- UF SP (SÃO PAULO), Município 66397 (LIMEIRA), Zona 0066, Seção 0430 - Votações no BU: 222, Votações no Log: 221.
- UF SP (SÃO PAULO), Município 66850 (MARTINÓPOLIS), Zona 0071, Seção 0023 - Votações no BU: 201, Votações no Log: 200.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0365, Seção 0166 - Votações no BU: 270, Votações no Log: 269.
- UF SP (SÃO PAULO), Município 70335 (SANTA FÉ DO SUL), Zona 0187, Seção 0058 - Votações no BU: 289, Votações no Log: 288.
- UF SP (SÃO PAULO), Município 70718 (SANTOS), Zona 0118, Seção 0314 - Votações no BU: 213, Votações no Log: 212.
- UF SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0412, Seção 0195 - Votações no BU: 304, Votações no Log: 303.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0350, Seção 0033 - Votações no BU: 285, Votações no Log: 284.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0381, Seção 0441 - Votações no BU: 346, Votações no Log: 345.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0417, Seção 0095 - Votações no BU: 264, Votações no Log: 110.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0418, Seção 0395 - Votações no BU: 271, Votações no Log: 255.
- UF SP (SÃO PAULO), Município 71811 (TATUÍ), Zona 0140, Seção 0216 - Votações no BU: 233, Votações no Log: 120.

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

- UF CE (CEARÁ), Município 13404 (BARROQUINHA), Zona 0108, Seção 0042 - Votações no BU: 244, Votações no Log: 243.
- UF DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0006, Seção 0044 - Votações no BU: 259, Votações no Log: 258.
- UF ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0618 - Votações no BU: 276, Votações no Log: 216.
- UF MA (MARANHÃO), Município 07277 (BALSAS), Zona 0022, Seção 0122 - Votações no BU: 298, Votações no Log: 297.
- UF MA (MARANHÃO), Município 07650 (COELHO NETO), Zona 0028, Seção 0030 - Votações no BU: 263, Votações no Log: 262.
- UF MA (MARANHÃO), Município 07668 (GOVERNADOR NUNES FREIRE), Zona 0101, Seção 0098 - Votações no BU: 191, Votações no Log: 179.
- UF MG (MINAS GERAIS), Município 40720 (SÃO JOÃO DO MANHUAÇU), Zona 0167, Seção 0179 - Votações no BU: 339, Votações no Log: 338.
- UF MG (MINAS GERAIS), Município 51152 (RIO POMBA), Zona 0239, Seção 0022 - Votações no BU: 206, Votações no Log: 205.
- UF PA (PARÁ), Município 04154 (ANANINDEUA), Zona 0043, Seção 0914 - Votações no BU: 310, Votações no Log: 309.
- UF PE (PERNAMBUCO), Município 23639 (CAETÉS), Zona 0130, Seção 0006 - Votações no BU: 237, Votações no Log: 236.
- UF PE (PERNAMBUCO), Município 25216 (PETROLINA), Zona 0083, Seção 0416 - Votações no BU: 279, Votações no Log: 278.
- UF PR (PARANÁ), Município 76678 (LONDRINA), Zona 0146, Seção 0273 - Votações no BU: 278, Votações no Log: 277.
- UF RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0200, Seção 0154 - Votações no BU: 308, Votações no Log: 307.
- UF RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0065, Seção 0233 - Votações no BU: 210, Votações no Log: 179.
- UF RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0007, Seção 0479 - Votações no BU: 348, Votações no Log: 347.
- UF SC (SANTA CATARINA), Município 80470 (BLUMENAU), Zona 0003, Seção 0463 - Votações no BU: 290, Votações no Log: 289.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0165 - Votações no BU: 310, Votações no Log: 309.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0394, Seção 0204 - Votações no BU: 306, Votações no Log: 286.
- UF SP (SÃO PAULO), Município 66192 (JUNDIAÍ), Zona 0065, Seção 0451 - Votações no BU: 305, Votações no Log: 304.
- UF SP (SÃO PAULO), Município 66192 (JUNDIAÍ), Zona 0424, Seção 0001 - Votações no BU: 185, Votações no Log: 184.
- UF SP (SÃO PAULO), Município 67938 (OSVALDO CRUZ), Zona 0163, Seção 0023 - Votações no BU: 232, Votações no Log: 231.
- UF SP (SÃO PAULO), Município 67954 (OURINHOS), Zona 0082, Seção 0084 - Votações no BU: 234, Votações no Log: 233.
- UF SP (SÃO PAULO), Município 68535 (PERUÍBE), Zona 0295, Seção 0177 - Votações no BU: 197, Votações no Log: 196.
- UF SP (SÃO PAULO), Município 69876 (ROSEIRA), Zona 0190, Seção 0100 - Votações no BU: 281, Votações no Log: 254.
- UF SP (SÃO PAULO), Município 70572 (SANTO ANDRÉ), Zona 0156, Seção 0309 - Votações no BU: 241, Votações no Log: 240.
- UF SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0127, Seção 0007 - Votações no BU: 172, Votações no Log: 171.
- UF SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0127, Seção 0546 - Votações no BU: 327, Votações no Log: 326.
- UF SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0282, Seção 0126 - Votações no BU: 275, Votações no Log: 274.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0002, Seção 0310 - Votações no BU: 267, Votações no Log: 17.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0258, Seção 0504 - Votações no BU: 294, Votações no Log: 166.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0374, Seção 0478 - Votações no BU: 287, Votações no Log: 286.
- UF SP (SÃO PAULO), Município 71153 (SÃO SEBASTIÃO), Zona 0132, Seção 0129 - Votações no BU: 294, Votações no Log: 278.
- UF TO (TOCANTINS), Município 73440 (PALMAS), Zona 0029, Seção 0229 - Votações no BU: 270, Votações no Log: 269.
- UF ZZ (EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123 - Votações no BU: 145, Votações no Log: 110.

</details>

## Mais votos no Log da Urna do que no Boletim de Urna

O **Boletim de urna** é o documento oficial que comprova quantos votos cada candidato obteve naquela urna específica. E o **Log de Urna** é um arquivo de texto gerado pela urna com cada operação realizada.

Cada voto computado pela urna gera linhas no arquivo de log. Então se contarmos essas linhas, saberemos quantos votos foram computados pela urna.

Obviamente, o número de votos do arquivo log precisa ser igual ao número de votos apresentado pelo Boletim de Urna, pois do contrário, não há como garantir que aquele arquivo foi gerado pela mesma urna que gerou o boletim de urna, e desta forma a credibilidade da urna fica em dúvida.

Abaixo são listadas todas as seções eleitorais em que o Boletim de Urna apresenta MENOS VOTOS do que votos contados no Log da Urna.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF BA (BAHIA), Município 30872 (DIAS D'ÁVILA), Zona 0186, Seção 0094 - Votações no BU: 318, Votações no Log: 319.
- UF BA (BAHIA), Município 34495 (CATU), Zona 0129, Seção 0143 - Votações no BU: 146, Votações no Log: 408.
- UF BA (BAHIA), Município 36196 (ITANAGRA), Zona 0185, Seção 0122 - Votações no BU: 288, Votações no Log: 289.
- UF BA (BAHIA), Município 38075 (PORTO SEGURO), Zona 0121, Seção 0151 - Votações no BU: 272, Votações no Log: 297.
- UF BA (BAHIA), Município 38075 (PORTO SEGURO), Zona 0121, Seção 0191 - Votações no BU: 209, Votações no Log: 218.
- UF ES (ESPÍRITO SANTO), Município 56995 (SERRA), Zona 0053, Seção 0375 - Votações no BU: 270, Votações no Log: 570.
- UF MA (MARANHÃO), Município 09598 (PAULO RAMOS), Zona 0102, Seção 0012 - Votações no BU: 198, Votações no Log: 199.
- UF MG (MINAS GERAIS), Município 44830 (ESPERA FELIZ), Zona 0303, Seção 0070 - Votações no BU: 122, Votações no Log: 220.
- UF MG (MINAS GERAIS), Município 49735 (PEDRA AZUL), Zona 0213, Seção 0036 - Votações no BU: 61, Votações no Log: 105.
- UF MG (MINAS GERAIS), Município 51071 (RIO NOVO), Zona 0235, Seção 0052 - Votações no BU: 122, Votações no Log: 200.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0011 - Votações no BU: 200, Votações no Log: 201.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0014 - Votações no BU: 243, Votações no Log: 245.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0025 - Votações no BU: 211, Votações no Log: 213.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0043 - Votações no BU: 246, Votações no Log: 248.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0044 - Votações no BU: 171, Votações no Log: 173.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0045 - Votações no BU: 270, Votações no Log: 271.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0176 - Votações no BU: 215, Votações no Log: 217.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0218 - Votações no BU: 252, Votações no Log: 254.
- UF PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0224 - Votações no BU: 266, Votações no Log: 267.
- UF PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0064 - Votações no BU: 223, Votações no Log: 225.
- UF PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0080 - Votações no BU: 197, Votações no Log: 199.
- UF PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0099 - Votações no BU: 202, Votações no Log: 204.
- UF PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0157 - Votações no BU: 243, Votações no Log: 245.
- UF PI (PIAUÍ), Município 10316 (BENEDITINOS), Zona 0047, Seção 0017 - Votações no BU: 218, Votações no Log: 273.
- UF PR (PARANÁ), Município 74071 (ALMIRANTE TAMANDARÉ), Zona 0171, Seção 0041 - Votações no BU: 308, Votações no Log: 309.
- UF PR (PARANÁ), Município 78336 (SALGADO FILHO), Zona 0131, Seção 0045 - Votações no BU: 242, Votações no Log: 476.
- UF RJ (RIO DE JANEIRO), Município 58122 (QUEIMADOS), Zona 0138, Seção 0146 - Votações no BU: 230, Votações no Log: 334.
- UF RN (RIO GRANDE DO NORTE), Município 16977 (JAÇANÃ), Zona 0068, Seção 0066 - Votações no BU: 270, Votações no Log: 271.
- UF RN (RIO GRANDE DO NORTE), Município 18139 (RIACHO DE SANTANA), Zona 0065, Seção 0032 - Votações no BU: 249, Votações no Log: 259.
- UF SC (SANTA CATARINA), Município 83054 (SANTA CECÍLIA), Zona 0051, Seção 0059 - Votações no BU: 259, Votações no Log: 260.
- UF SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0035 - Votações no BU: 307, Votações no Log: 563.
- UF SP (SÃO PAULO), Município 64734 (GUARIBA), Zona 0197, Seção 0122 - Votações no BU: 191, Votações no Log: 192.
- UF SP (SÃO PAULO), Município 66230 (JUQUIÁ), Zona 0223, Seção 0023 - Votações no BU: 215, Votações no Log: 216.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0020, Seção 0036 - Votações no BU: 255, Votações no Log: 256.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0351, Seção 0271 - Votações no BU: 214, Votações no Log: 220.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0351, Seção 0545 - Votações no BU: 247, Votações no Log: 268.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0353, Seção 0775 - Votações no BU: 330, Votações no Log: 523.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0529 - Votações no BU: 299, Votações no Log: 591.

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

- UF BA (BAHIA), Município 38652 (SANTANA), Zona 0099, Seção 0046 - Votações no BU: 220, Votações no Log: 230.
- UF BA (BAHIA), Município 39535 (URUÇUCA), Zona 0198, Seção 0043 - Votações no BU: 252, Votações no Log: 253.
- UF ES (ESPÍRITO SANTO), Município 56634 (LINHARES), Zona 0025, Seção 0089 - Votações no BU: 311, Votações no Log: 312.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0149 - Votações no BU: 128, Votações no Log: 129.
- UF PE (PERNAMBUCO), Município 23973 (CUMARU), Zona 0091, Seção 0099 - Votações no BU: 119, Votações no Log: 198.
- UF PE (PERNAMBUCO), Município 24350 (IGARASSU), Zona 0085, Seção 0044 - Votações no BU: 296, Votações no Log: 297.
- UF PR (PARANÁ), Município 78050 (REALEZA), Zona 0130, Seção 0041 - Votações no BU: 286, Votações no Log: 287.
- UF PR (PARANÁ), Município 78859 (SÃO JOSÉ DOS PINHAIS), Zona 0199, Seção 0186 - Votações no BU: 351, Votações no Log: 352.
- UF PR (PARANÁ), Município 84670 (SANTA TEREZINHA DE ITAIPU), Zona 0147, Seção 0417 - Votações no BU: 258, Votações no Log: 259.
- UF RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0128, Seção 0136 - Votações no BU: 323, Votações no Log: 324.
- UF RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0029, Seção 0162 - Votações no BU: 278, Votações no Log: 309.
- UF RS (RIO GRANDE DO SUL), Município 85871 (CANGUÇU), Zona 0014, Seção 0106 - Votações no BU: 256, Votações no Log: 257.
- UF SE (SERGIPE), Município 31690 (LAGARTO), Zona 0012, Seção 0092 - Votações no BU: 246, Votações no Log: 921.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0418, Seção 0261 - Votações no BU: 332, Votações no Log: 629.

</details>

## Sem arquivos, arquivos excluídos ou arquivos rejeitados

Algumas das seções eleitorais simplesmente não possuem arquivos. No arquivo JSON de configuração destas seções, há um campo que justifica a ausência dos arquivos. São 3 possibilidades:
- Excluído
- Sem arquivo
- Rejeitado

O problema, nestes casos, é que os votos dos eleitores destas seções foram simplesmente descartados. Estes eleitores tiveram prejudicadas as suas participações nas eleições.

O voto é um direito e não deve ser vedado a nenhum cidadão o direito de participar do processo democrático.

### Primeiro Turno

- UF AM (AMAZONAS), Município 02550 (MANAUS), Zona 0068, Seção 0797. Motivo: Sem arquivo.
- UF ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0038. Motivo: Sem arquivo.
- UF ZZ (EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150. Motivo: Rejeitado.

### Segundo Turno

- UF AM (AMAZONAS), Município 02550 (MANAUS), Zona 0068, Seção 0797. Motivo: Sem arquivo.
- UF GO (GOIÁS), Município 92886 (COCALZINHO DE GOIÁS), Zona 0026, Seção 0131. Motivo: Rejeitado.

## Não há registro de votos

O **registro de votos** é um arquivo binário, gerado pela Urna Eletrônica, que contém todos os votos que foram computados, incluindo os votos brancos e os nulos.

Cada voto digitado vai gerar um registro neste arquivo. O arquivo, inclusive, salva o número digitado para os votos nulos. Se um eleitor votar, por exemplo, no candidato 99 (que não existe), o RDV vai ter um registro com o número 99 e a informação de que aquele voto foi computado como nulo.

É possível também comparar o registro de votos com o Boletim da Urna. As quantidades dos votos para cada candidato no BU deve ser a mesma que o RDV. Caso algum esteja diferente, significa que os arquivos não foram gerados pela mesma urna ou que a urna não está computando corretamente as informações... E em ambos os casos, a credibilidade do processo eleitoral fica em dúvida.

A ausência do registro de votos é um problema grave, pois impede que o Boletim de Urna seja comparado com outra fonte crível de informação.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF BA (BAHIA), Município 33197 (AMARGOSA), Zona 0036, Seção 0015.
- UF MA (MARANHÃO), Município 09237 (SÃO MATEUS DO MARANHÃO), Zona 0084, Seção 0215.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0146.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0147.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0186.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0192.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0215.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0227.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0234.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0370.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0372.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0531.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0535.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0538.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0539.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0542.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0544.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0553.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0554.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0557.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0559.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0560.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0562.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0566.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0567.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0569.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0570.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0573.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0576.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0579.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0581.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0582.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0592.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0594.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0598.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0599.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0600.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0605.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0606.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0612.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0613.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0617.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0618.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0620.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0621.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0623.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0626.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0628.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0632.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0634.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0635.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0636.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0639.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1420.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3041.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3073.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3080.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3163.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3170.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3176.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3363.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3374.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3386.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3390.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3394.

</details>

### Segundo Turno

- UF RN (RIO GRANDE DO NORTE), Município 17655 (NOVA CRUZ), Zona 0012, Seção 0114.

## Não há informação de Zerésima

A Zerésima é o processo que garante que a urna eletrônica estava "zerada" antes do início da votação. Esta informação aparece no log da urna.

A Zerésima é realizada normalmente alguns minutos antes do início da votação. A urna imprime um comprovante e este comprovante também é disponibilizado para o público e para o TRE e o TSE (assim como o Boletim de Urna).

Se o arquivo de log da urna não faz menção à Zerésima, significa que este processo não foi realizado - o que não pode acontecer.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF BA (BAHIA), Município 30007 (LUÍS EDUARDO MAGALHÃES), Zona 0205, Seção 0210.
- UF BA (BAHIA), Município 34134 (CAMAÇARI), Zona 0171, Seção 0232.
- UF BA (BAHIA), Município 35696 (IGAPORÃ), Zona 0168, Seção 0142.
- UF BA (BAHIA), Município 38490 (SALVADOR), Zona 0002, Seção 0534.
- UF CE (CEARÁ), Município 14192 (IPUEIRAS), Zona 0040, Seção 0050.
- UF DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0017, Seção 0090.
- UF MA (MARANHÃO), Município 09237 (SÃO MATEUS DO MARANHÃO), Zona 0084, Seção 0215.
- UF MG (MINAS GERAIS), Município 45136 (FELIXLÂNDIA), Zona 0100, Seção 0231.
- UF MG (MINAS GERAIS), Município 51390 (SALINAS), Zona 0244, Seção 0091.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0024.
- UF PE (PERNAMBUCO), Município 23892 (CHÃ GRANDE), Zona 0031, Seção 0147.
- UF PR (PARANÁ), Município 75370 (CURIÚVA), Zona 0119, Seção 0016.
- UF PR (PARANÁ), Município 75833 (GUARAPUAVA), Zona 0043, Seção 0074.
- UF PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0100.
- UF RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0167, Seção 0006.
- UF RN (RIO GRANDE DO NORTE), Município 16918 (IPANGUAÇU), Zona 0054, Seção 0090.
- UF RS (RIO GRANDE DO SUL), Município 87912 (PELOTAS), Zona 0060, Seção 0322.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0365, Seção 0166.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0531.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0535.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0538.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0539.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0542.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0544.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0553.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0554.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0557.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0559.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0560.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0562.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0566.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0567.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0569.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0570.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0573.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0576.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0579.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0581.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0582.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0592.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0594.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0598.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0599.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0600.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0605.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0606.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0612.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0613.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0617.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0618.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0620.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0621.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0623.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0626.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0628.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0632.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0634.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0635.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0636.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0639.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1420.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3041.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3073.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3080.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3163.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3170.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3176.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3363.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3374.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3386.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3390.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3394.

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

- UF AM (AMAZONAS), Município 02550 (MANAUS), Zona 0040, Seção 0422.
- UF AM (AMAZONAS), Município 02550 (MANAUS), Zona 0068, Seção 0512.
- UF BA (BAHIA), Município 34134 (CAMAÇARI), Zona 0171, Seção 0232.
- UF ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0618.
- UF PA (PARÁ), Município 04650 (IGARAPÉ-MIRI), Zona 0006, Seção 0059.
- UF PA (PARÁ), Município 04650 (IGARAPÉ-MIRI), Zona 0006, Seção 0060.
- UF PA (PARÁ), Município 04650 (IGARAPÉ-MIRI), Zona 0006, Seção 0072.
- UF PA (PARÁ), Município 04650 (IGARAPÉ-MIRI), Zona 0006, Seção 0109.
- UF PA (PARÁ), Município 04650 (IGARAPÉ-MIRI), Zona 0006, Seção 0111.
- UF RN (RIO GRANDE DO NORTE), Município 17655 (NOVA CRUZ), Zona 0012, Seção 0114.
- UF RS (RIO GRANDE DO SUL), Município 87912 (PELOTAS), Zona 0060, Seção 0322.
- UF SP (SÃO PAULO), Município 69876 (ROSEIRA), Zona 0190, Seção 0100.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0002, Seção 0310.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0258, Seção 0504.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0373, Seção 0734.
- UF SP (SÃO PAULO), Município 71153 (SÃO SEBASTIÃO), Zona 0132, Seção 0129.
- UF ZZ (EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123.

</details>

## Zerésima realizada mais de duas horas antes da abertura da Urna

A Zerésima, como explicado anteriormente, é o processo que garante que a urna eletrônica foi zerada antes da votação ser iniciada.

Normalmente este processo é realizado alguns minutos antes da votação iniciar. Porém, nos casos listados abaixo, a Zerésima foi realizada mais de duas horas antes da votação.

Por si só, isso não é um problema, mas ainda assim é algo estranho. Os mesários normalmente não se apresentam para o trabalho da seção com tanta antecedência assim.

### Primeiro Turno

- UF BA (BAHIA), Município 37630 (OLINDINA), Zona 0081, Seção 0155. Abertura: 02/10/2022 09:31:17, Zerésima: 02/10/2022 07:20:12.
- UF MA (MARANHÃO), Município 07269 (BOM JESUS DAS SELVAS), Zona 0095, Seção 0115. Abertura: 02/10/2022 09:48:14, Zerésima: 02/10/2022 07:42:34.
- UF MA (MARANHÃO), Município 07757 (DUQUE BACELAR), Zona 0028, Seção 0123. Abertura: 02/10/2022 09:39:58, Zerésima: 02/10/2022 07:09:12.
- UF PI (PIAUÍ), Município 12181 (CURRAL NOVO DO PIAUÍ), Zona 0056, Seção 0078. Abertura: 02/10/2022 09:15:24, Zerésima: 02/10/2022 07:07:30.
- UF RJ (RIO DE JANEIRO), Município 58653 (NITERÓI), Zona 0144, Seção 0184. Abertura: 02/10/2022 09:53:03, Zerésima: 02/10/2022 07:40:09.
- UF RJ (RIO DE JANEIRO), Município 59013 (SÃO JOÃO DE MERITI), Zona 0186, Seção 0003. Abertura: 02/10/2022 09:38:00, Zerésima: 02/10/2022 07:18:46.

### Segundo Turno

- Nenhum caso.

## Códigos de Identificação da Urna Eletrônica repetidos

Toda urna eletrônica possui um número de identificação (Código de Identificação da Urna Eletrônica). Este código é uma das informações emitidas pelo Boletim de Urna.

Existem algumas urnas eletrônicas que possuem o mesmo número de identificação... O que é estranho. Eu não posso afirmar, mas acredito que este número deveria ser único para cada Urna eletrônica.

Abaixo as seções eleitorais e suas urnas que possuem códigos repetidos.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- Código Identificador de Urna Eletrônica: **1296316** - Quantidade de ocorrências: **26**.

  - UF ZZ (EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003.
  - UF ZZ (EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495.
  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0590.
  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0662.
  - UF ZZ (EXTERIOR), Município 29386 (BERLIM), Zona 0001, Seção 3057.
  - UF ZZ (EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380.
  - UF ZZ (EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123.
  - UF ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1740.
  - UF ZZ (EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703.
  - UF ZZ (EXTERIOR), Município 29947 (LIMA), Zona 0001, Seção 0154.
  - UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0591.
  - UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1355.
  - UF ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1632.
  - UF ZZ (EXTERIOR), Município 30082 (MANILA), Zona 0001, Seção 0990.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0504.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 3040.
  - UF ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0165.
  - UF ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0247.
  - UF ZZ (EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228.
  - UF ZZ (EXTERIOR), Município 30228 (NOVA YORK), Zona 0001, Seção 0297.
  - UF ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504.
  - UF ZZ (EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384.
  - UF ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1488.
  - UF ZZ (EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3268.
  - UF ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1964.
  - UF ZZ (EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524.
- Código Identificador de Urna Eletrônica: **1273645** - Quantidade de ocorrências: **15**.

  - UF ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0051.
  - UF ZZ (EXTERIOR), Município 29807 (HOUSTON), Zona 0001, Seção 0940.
  - UF ZZ (EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390.
  - UF ZZ (EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150.
  - UF ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1640.
  - UF ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 1029.
  - UF ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0768.
  - UF ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0796.
  - UF ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1858.
  - UF ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1886.
  - UF ZZ (EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353.
  - UF ZZ (EXTERIOR), Município 30562 (SYDNEY), Zona 0001, Seção 1375.
  - UF ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1821.
  - UF ZZ (EXTERIOR), Município 30783 (WASHINGTON), Zona 0001, Seção 0458.
  - UF ZZ (EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004.
- Código Identificador de Urna Eletrônica: **1274462** - Quantidade de ocorrências: **12**.

  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0545.
  - UF ZZ (EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735.
  - UF ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0072.
  - UF ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1909.
  - UF ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127.
  - UF ZZ (EXTERIOR), Município 29998 (LUANDA), Zona 0001, Seção 0199.
  - UF ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0024.
  - UF ZZ (EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490.
  - UF ZZ (EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182.
  - UF ZZ (EXTERIOR), Município 30619 (TEL AVIV), Zona 0001, Seção 0682.
  - UF ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1787.
  - UF ZZ (EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444.
- Código Identificador de Urna Eletrônica: **1246419** - Quantidade de ocorrências: **12**.

  - UF ZZ (EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327.
  - UF ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0053.
  - UF ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0071.
  - UF ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1912.
  - UF ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1671.
  - UF ZZ (EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259.
  - UF ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0026.
  - UF ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0399.
  - UF ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0426.
  - UF ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0168.
  - UF ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1820.
  - UF ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1966.
- Código Identificador de Urna Eletrônica: **1295943** - Quantidade de ocorrências: **11**.

  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0719.
  - UF ZZ (EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558.
  - UF ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1750.
  - UF ZZ (EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494.
  - UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0541.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1125.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1132.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1164.
  - UF ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0248.
  - UF ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0758.
  - UF ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1967.
- Código Identificador de Urna Eletrônica: **1340042** - Quantidade de ocorrências: **9**.

  - UF ZZ (EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001.
  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0643.
  - UF ZZ (EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096.
  - UF ZZ (EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146.
  - UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0540.
  - UF ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0230.
  - UF ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0442.
  - UF ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1818.
  - UF ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1031.
- Código Identificador de Urna Eletrônica: **1252874** - Quantidade de ocorrências: **8**.

  - UF ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0551.
  - UF ZZ (EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028.
  - UF ZZ (EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712.
  - UF ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0394.
  - UF ZZ (EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647.
  - UF ZZ (EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340.
  - UF ZZ (EXTERIOR), Município 30252 (OTTAWA), Zona 0001, Seção 0767.
  - UF ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1765.
- Código Identificador de Urna Eletrônica: **1229330** - Quantidade de ocorrências: **6**.

  - UF ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 1041.
  - UF ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1637.
  - UF ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0432.
  - UF ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0164.
  - UF ZZ (EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764.
  - UF ZZ (EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1690.
- Código Identificador de Urna Eletrônica: **1620697** - Quantidade de ocorrências: **2**.

  - UF RS (RIO GRANDE DO SUL), Município 87378 (MARAU), Zona 0062, Seção 0035.
  - UF RS (RIO GRANDE DO SUL), Município 87181 (NICOLAU VERGUEIRO), Zona 0062, Seção 0076.
- Código Identificador de Urna Eletrônica: **1316810** - Quantidade de ocorrências: **2**.

  - UF MG (MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0041.
  - UF MG (MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0282.
- Código Identificador de Urna Eletrônica: **1268286** - Quantidade de ocorrências: **2**.

  - UF AM (AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0116.
  - UF AM (AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0174.
- Código Identificador de Urna Eletrônica: **1095313** - Quantidade de ocorrências: **2**.

  - UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0524.
  - UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0529.
- Código Identificador de Urna Eletrônica: **1149151** - Quantidade de ocorrências: **2**.

  - UF MT (MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0126.
  - UF MT (MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0136.
- Código Identificador de Urna Eletrônica: **1293146** - Quantidade de ocorrências: **2**.

  - UF MG (MINAS GERAIS), Município 46450 (ITAIPÉ), Zona 0196, Seção 0037.
  - UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173.
- Código Identificador de Urna Eletrônica: **1612929** - Quantidade de ocorrências: **2**.

  - UF RN (RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0028.
  - UF RN (RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0077.

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

- Código Identificador de Urna Eletrônica: **1273426** - Quantidade de ocorrências: **12**.

  - ZZ(EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001.
  - ZZ(EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327.
  - ZZ(EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096.
  - ZZ(EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127.
  - ZZ(EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494.
  - ZZ(EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146.
  - ZZ(EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353.
  - ZZ(EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504.
  - ZZ(EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182.
  - ZZ(EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1231.
  - ZZ(EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444.
  - ZZ(EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1693.
- Código Identificador de Urna Eletrônica: **1273414** - Quantidade de ocorrências: **11**.

  - ZZ(EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003.
  - ZZ(EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495.
  - ZZ(EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028.
  - ZZ(EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558.
  - ZZ(EXTERIOR), Município 30902 (HARTFORD), Zona 0001, Seção 3134.
  - ZZ(EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390.
  - ZZ(EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150.
  - ZZ(EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259.
  - ZZ(EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490.
  - ZZ(EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384.
  - ZZ(EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3030.
- Código Identificador de Urna Eletrônica: **1255254** - Quantidade de ocorrências: **10**.

  - ZZ(EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735.
  - ZZ(EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380.
  - ZZ(EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703.
  - ZZ(EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712.
  - ZZ(EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647.
  - ZZ(EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340.
  - ZZ(EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228.
  - ZZ(EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764.
  - ZZ(EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524.
  - ZZ(EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004.
- Código Identificador de Urna Eletrônica: **1809943** - Quantidade de ocorrências: **5**.

  - PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0017.
  - PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0018.
  - PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0019.
  - PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0037.
  - PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0055.
- Código Identificador de Urna Eletrônica: **1778800** - Quantidade de ocorrências: **2**.

  - SC(SANTA CATARINA), Município 83011 (SALETE), Zona 0046, Seção 0021.
  - SC(SANTA CATARINA), Município 83011 (SALETE), Zona 0046, Seção 0022.
- Código Identificador de Urna Eletrônica: **1791872** - Quantidade de ocorrências: **2**.

  - GO(GOIÁS), Município 92886 (COCALZINHO DE GOIÁS), Zona 0026, Seção 0131.
  - GO(GOIÁS), Município 95435 (PIRENÓPOLIS), Zona 0026, Seção 0111.

</details>

## Códigos de Identificação da Urna Eletrônica são diferentes no IMGBU e no BU

Ambos os arquivos BU e IMGBU são boletins de urna. A diferença é que o arquivo BU é um arquivo binário, feito para ser lido pelos programas de totalização do TSE, enquanto que o IMGBU é um arquivo texto, que pode ser lido sem dificultade usando um editor de textos comum.

Em essência, ambos os arquivos deveriam conter exatamente as mesmas informações, pois foram gerados pela mesma urna eletrônica. Porém abaixo estão seções eleitorais em que o código de identificação da urna eletrônica é diferente no arquivo BU e no IMGBU.

Isso não deveria acontecer, afinal ambos os arquivos teriam sido gerados pela mesma urna, então não haveria possibilidade do código de identificação ser diferente em ambos os arquivos.

Isso abre uma dúvida enorme no processo eleitoral. Afinal, por qual razão os arquivos foram gerados por urnas diferentes?

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF AL (ALAGOAS), Município 27650 (INHAPI), Zona 0039, Seção 0099. Código no BU: 1769281 - Código no IMGBU: 1076434.
- UF BA (BAHIA), Município 33596 (BARRA DO MENDES), Zona 0176, Seção 0032. Código no BU: 1240144 - Código no IMGBU: 1226785.
- UF CE (CEARÁ), Município 13897 (FORTALEZA), Zona 0094, Seção 0935. Código no BU: 2233967 - Código no IMGBU: 2234041.
- UF CE (CEARÁ), Município 14575 (MARCO), Zona 0096, Seção 0162. Código no BU: 1003085 - Código no IMGBU: 1007106.
- UF ES (ESPÍRITO SANTO), Município 56138 (ATÍLIO VIVÁCQUA), Zona 0002, Seção 0021. Código no BU: 2040131 - Código no IMGBU: 2006899.
- UF GO (GOIÁS), Município 93033 (CATURAÍ), Zona 0101, Seção 0036. Código no BU: 1005082 - Código no IMGBU: 1202220.
- UF GO (GOIÁS), Município 93734 (GOIÂNIA), Zona 0001, Seção 0461. Código no BU: 2201801 - Código no IMGBU: 2199601.
- UF MA (MARANHÃO), Município 09679 (ARAME), Zona 0104, Seção 0028. Código no BU: 1618053 - Código no IMGBU: 1662137.
- UF MG (MINAS GERAIS), Município 40215 (AIMORÉS), Zona 0005, Seção 0002. Código no BU: 1836250 - Código no IMGBU: 1754355.
- UF MG (MINAS GERAIS), Município 42994 (CASCALHO RICO), Zona 0110, Seção 0037. Código no BU: 1143085 - Código no IMGBU: 1170320.
- UF MG (MINAS GERAIS), Município 48771 (MURIAÉ), Zona 0187, Seção 0442. Código no BU: 1284763 - Código no IMGBU: 1310508.
- UF MG (MINAS GERAIS), Município 49590 (PATOS DE MINAS), Zona 0330, Seção 0174. Código no BU: 1193544 - Código no IMGBU: 1077196.
- UF MG (MINAS GERAIS), Município 49611 (PATROCÍNIO), Zona 0211, Seção 0011. Código no BU: 1334439 - Código no IMGBU: 1255198.
- UF MG (MINAS GERAIS), Município 53597 (TAIOBEIRAS), Zona 0266, Seção 0175. Código no BU: 1601751 - Código no IMGBU: 1606205.
- UF MG (MINAS GERAIS), Município 53872 (TRÊS PONTAS), Zona 0273, Seção 0154. Código no BU: 2076864 - Código no IMGBU: 2091360.
- UF MS (MATO GROSSO DO SUL), Município 90514 (CAMPO GRANDE), Zona 0054, Seção 0183. Código no BU: 2023409 - Código no IMGBU: 2146273.
- UF PA (PARÁ), Município 04030 (ACARÁ), Zona 0094, Seção 0074. Código no BU: 1629320 - Código no IMGBU: 1274748.
- UF PA (PARÁ), Município 04073 (ALENQUER), Zona 0021, Seção 0116. Código no BU: 1078913 - Código no IMGBU: 1808680.
- UF PB (PARAÍBA), Município 21113 (OLIVEDOS), Zona 0023, Seção 0023. Código no BU: 1244311 - Código no IMGBU: 1294014.
- UF PE (PERNAMBUCO), Município 23035 (AFRÂNIO), Zona 0107, Seção 0024. Código no BU: 1320899 - Código no IMGBU: 1319617.
- UF PE (PERNAMBUCO), Município 25313 (RECIFE), Zona 0001, Seção 0276. Código no BU: 2193066 - Código no IMGBU: 2171611.
- UF PE (PERNAMBUCO), Município 25313 (RECIFE), Zona 0002, Seção 0500. Código no BU: 2044142 - Código no IMGBU: 2043695.
- UF PI (PIAUÍ), Município 11037 (JAICÓS), Zona 0019, Seção 0016. Código no BU: 1335552 - Código no IMGBU: 1335651.
- UF PR (PARANÁ), Município 75698 (GOIOERÊ), Zona 0092, Seção 0046. Código no BU: 1252224 - Código no IMGBU: 1242372.
- UF PR (PARANÁ), Município 76813 (MANOEL RIBAS), Zona 0196, Seção 0155. Código no BU: 1172194 - Código no IMGBU: 1139732.
- UF PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0002. Código no BU: 1251523 - Código no IMGBU: 1262352.
- UF RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0103, Seção 0374. Código no BU: 1079776 - Código no IMGBU: 1143078.
- UF RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0029, Seção 0017. Código no BU: 1265036 - Código no IMGBU: 1233956.
- UF RJ (RIO DE JANEIRO), Município 59013 (SÃO JOÃO DE MERITI), Zona 0187, Seção 0267. Código no BU: 1239312 - Código no IMGBU: 1276807.
- UF RJ (RIO DE JANEIRO), Município 59153 (TERESÓPOLIS), Zona 0038, Seção 0276. Código no BU: 1230490 - Código no IMGBU: 1289247.
- UF RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0119, Seção 0228. Código no BU: 2156817 - Código no IMGBU: 2177741.
- UF RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0180, Seção 0007. Código no BU: 2178352 - Código no IMGBU: 2179235.
- UF RN (RIO GRANDE DO NORTE), Município 17590 (MOSSORÓ), Zona 0033, Seção 0060. Código no BU: 1256052 - Código no IMGBU: 1309624.
- UF RS (RIO GRANDE DO SUL), Município 85111 (ALVORADA), Zona 0074, Seção 0314. Código no BU: 1802139 - Código no IMGBU: 1803675.
- UF RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0002, Seção 0199. Código no BU: 2228407 - Código no IMGBU: 2228407.
- UF RS (RIO GRANDE DO SUL), Município 89575 (VERA CRUZ), Zona 0162, Seção 0328. Código no BU: 2176680 - Código no IMGBU: 2176398.
- UF SC (SANTA CATARINA), Município 81833 (LAGES), Zona 0104, Seção 0155. Código no BU: 2181355 - Código no IMGBU: 2182199.
- UF SC (SANTA CATARINA), Município 83259 (SÃO JOAQUIM), Zona 0028, Seção 0095. Código no BU: 1105360 - Código no IMGBU: 1337227.
- UF SP (SÃO PAULO), Município 61778 (ARUJÁ), Zona 0335, Seção 0021. Código no BU: 1288837 - Código no IMGBU: 1600858.
- UF SP (SÃO PAULO), Município 62138 (BARUERI), Zona 0199, Seção 0273. Código no BU: 1021346 - Código no IMGBU: 1092722.
- UF SP (SÃO PAULO), Município 62936 (CAMPO LIMPO PAULISTA), Zona 0344, Seção 0063. Código no BU: 1092477 - Código no IMGBU: 1626780.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0209. Código no BU: 1113866 - Código no IMGBU: 1342905.
- UF SP (SÃO PAULO), Município 68756 (PIRACICABA), Zona 0093, Seção 0481. Código no BU: 2214488 - Código no IMGBU: 2214722.
- UF SP (SÃO PAULO), Município 70572 (SANTO ANDRÉ), Zona 0263, Seção 0223. Código no BU: 1197381 - Código no IMGBU: 1165648.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0006, Seção 0399. Código no BU: 2046902 - Código no IMGBU: 2049718.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0375, Seção 0064. Código no BU: 1056799 - Código no IMGBU: 1242054.

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

- UF BA (BAHIA), Município 35092 (ENTRE RIOS), Zona 0144, Seção 0044. Código no BU: 1299753 - Código no IMGBU: 1268456.
- UF BA (BAHIA), Município 38792 (SÃO FÉLIX), Zona 0118, Seção 0091. Código no BU: 1268096 - Código no IMGBU: 1256792.
- UF CE (CEARÁ), Município 13692 (CASCAVEL), Zona 0007, Seção 0261. Código no BU: 2235539 - Código no IMGBU: 2206816.
- UF DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0017, Seção 0111. Código no BU: 1247253 - Código no IMGBU: 1681624.
- UF GO (GOIÁS), Município 92363 (MONTIVIDIU), Zona 0140, Seção 0072. Código no BU: 1173317 - Código no IMGBU: 1204212.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0149. Código no BU: 1054856 - Código no IMGBU: 1814347.
- UF MA (MARANHÃO), Município 08699 (PINHEIRO), Zona 0037, Seção 0340. Código no BU: 2136301 - Código no IMGBU: 2093285.
- UF MA (MARANHÃO), Município 08893 (SÃO JOSÉ DE RIBAMAR), Zona 0047, Seção 0508. Código no BU: 2098071 - Código no IMGBU: 2084345.
- UF MG (MINAS GERAIS), Município 52990 (SÃO TIAGO), Zona 0232, Seção 0089. Código no BU: 2121985 - Código no IMGBU: 2078296.
- UF PA (PARÁ), Município 04278 (BELÉM), Zona 0096, Seção 0166. Código no BU: 2193233 - Código no IMGBU: 2193036.
- UF PR (PARANÁ), Município 78930 (SÃO PEDRO DO PARANÁ), Zona 0085, Seção 0083. Código no BU: 1226587 - Código no IMGBU: 1324887.
- UF RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0079, Seção 0637. Código no BU: 1084921 - Código no IMGBU: 1132675.
- UF RJ (RIO DE JANEIRO), Município 58653 (NITERÓI), Zona 0144, Seção 0297. Código no BU: 1249327 - Código no IMGBU: 1235204.
- UF RJ (RIO DE JANEIRO), Município 58696 (NOVA IGUAÇU), Zona 0157, Seção 0069. Código no BU: 2233406 - Código no IMGBU: 2218487.
- UF RJ (RIO DE JANEIRO), Município 58971 (SÃO GONÇALO), Zona 0068, Seção 0282. Código no BU: 1231556 - Código no IMGBU: 1317794.
- UF RJ (RIO DE JANEIRO), Município 59153 (TERESÓPOLIS), Zona 0195, Seção 0002. Código no BU: 1301702 - Código no IMGBU: 1312328.
- UF RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0162, Seção 0057. Código no BU: 2212206 - Código no IMGBU: 2229559.
- UF SP (SÃO PAULO), Município 61654 (ARARAS), Zona 0014, Seção 0035. Código no BU: 2138562 - Código no IMGBU: 2143356.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0351, Seção 0740. Código no BU: 2010729 - Código no IMGBU: 2063512.
- UF SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0373, Seção 0734. Código no BU: 1615695 - Código no IMGBU: 1826321.

</details>

## Votos computados antes do início da votação

O log da urna registra uma linha quando a urna está pronta para receber votos. Esta é a marca que diz que a votação começou.

Portanto, não deveriam haver votos computados antes desta marca. Mas abaixo estão listadas algumas seções eleitorais onde isso ocorreu.

### Primeiro Turno

- UF BA (BAHIA), Município 38075 (PORTO SEGURO), Zona 0121, Seção 0151. Abertura da urna: 02/10/2022 08:07:58, Primeiro voto: 02/10/2022 08:03:23.
- UF ES (ESPÍRITO SANTO), Município 56995 (SERRA), Zona 0053, Seção 0375. Abertura da urna: 02/10/2022 08:02:42, Primeiro voto: 02/10/2022 08:02:28.
- UF MA (MARANHÃO), Município 09210 (SÃO LUÍS), Zona 0003, Seção 0397. Abertura da urna: 02/10/2022 08:35:57, Primeiro voto: 02/10/2022 08:15:24.
- UF MA (MARANHÃO), Município 09598 (PAULO RAMOS), Zona 0102, Seção 0012. Abertura da urna: 02/10/2022 09:56:04, Primeiro voto: 02/10/2022 08:02:23.
- UF RS (RIO GRANDE DO SUL), Município 85111 (ALVORADA), Zona 0124, Seção 0106. Abertura da urna: 02/10/2022 08:31:57, Primeiro voto: 02/10/2022 08:10:11.

### Segundo Turno

- UF BA (BAHIA), Município 38652 (SANTANA), Zona 0099, Seção 0046. Data/hora da abertura da urna: 30/10/2022 08:37:11, Data/hora do primeiro voto: 30/10/2022 08:00:25.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0149. Data/hora da abertura da urna: 30/10/2022 12:33:27, Data/hora do primeiro voto: 30/10/2022 09:03:28.

## Não há arquivo IMGBU

O arquivo IMGBU é a **imagem do boletim de urna**. É o arquivo texto que é impresso pela urna eletrônica ao final da votação. Este é o documento oficial do resultado de cada urna eletrônica.

Este arquivo é gerado pela urna juntamente com os demais arquivos. Ele não poderia estar faltando. Mas para as seções listadas abaixo, não há este arquivo.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

- UF MA (MARANHÃO), Município 09237 (SÃO MATEUS DO MARANHÃO), Zona 0084, Seção 0215.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0531.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0535.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0538.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0539.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0542.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0544.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0553.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0554.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0557.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0559.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0560.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0562.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0566.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0567.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0569.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0570.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0573.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0576.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0579.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0581.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0582.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0592.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0594.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0598.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0599.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0600.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0605.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0606.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0612.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0613.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0617.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0618.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0620.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0621.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0623.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0626.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0628.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0632.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0634.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0635.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0636.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0639.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1420.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3041.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3073.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3080.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3163.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3170.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3176.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3363.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3374.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3386.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3390.
- UF ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 3394.

</details>

### Segundo Turno

- UF RN (RIO GRANDE DO NORTE), Município 17655 (NOVA CRUZ), Zona 0012, Seção 0114.

## Não há arquivo BU

O arquivo BU é o **boletim de urna**. É o arquivo binário que é gerado pela urna eletrônica ao final da votação. Este é o arquivo que é processado pelo TSE para fazer a totalização dos votos.

Este arquivo é gerado pela urna juntamente com os demais arquivos. Ele não poderia estar faltando. Mas para as seções listadas abaixo, não há este arquivo.

### Primeiro Turno

- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0146.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0147.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0186.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0192.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0215.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0227.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0234.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0370.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0372.

### Segundo Turno

- Nenhum caso

## O Boletim de Urna (arquivo BU) está corrompido

O Boletim de Urna é um arquivo binário que contém a totalização dos votos de cada candidato de uma determinada seção eleitoral. Se este arquivo estiver corrompido, as únicas formas de saber como foi a votação da urna são através da imagem do boletim de urna ou do registro de voto.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informação para comparação.

### Primeiro Turno

- Nenhum caso

### Segundo Turno

- Nenhum caso

## O Registro de Votos (arquivo RDV) está corrompido

O Registro de votos é um arquivo binário que contém o detalhamento de cada voto para cada candidato de uma seção eleitoral. Se este arquivo estiver corrompido, as únicas formas de saber como foi a votação da urna são através da imagem do boletim de urna ou do boletim de urna binário.

Ter o arquivo corrompido reduz a margem de auditoria, pois elimina uma importante fonte de informação para comparação.

### Primeiro Turno

- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0146.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0147.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0186.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0192.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0215.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0227.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0234.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0370.
- UF PE (PERNAMBUCO), Município 30015 (FERNANDO DE NORONHA), Zona 0004, Seção 0372.

### Segundo Turno

- Nenhum caso

## Diferença de votos entre o arquivo IMGBU e o arquivo BU

O arquivo IMGBU é um arquivo texto que contém a imagem do boletim de urna, e o arquivo BU é um arquivo binário que contém o boletim de urna. Em essência, são o mesmo arquivo, porém em formatos diferentes. 

A imagem pode ser facilmente lida por uma pessoa, já o arquivo binário depende de um programa especificamente criado para ler este tipo de arquivo.

Quando as informações dos dois arquivos são diferentes entre si, fica explicito que os arquivos foram gerados por urnas eletrônicas distintas, e não pelo mesmo equipamento.

Isso coloca em dúvida a lisura do processo eleitoral como um todo, pois isso não deveria ser possível de realizar.

### Primeiro Turno

- UF RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0002, Seção 0199.

### Segundo Turno

- Nenhum caso

## Seções que receberam votos por mais do que 9 horas

As seções eleitorais normalmente se iniciam as 8:00 e se encerram as 17:00 (horário de Brasília). Portanto são 9 horas em que as seções permanecem abertas e disponíveis para receber votos.

Porém, nas eleições de 2022 várias seções eleitorais ultrapassaram este período. Foram **151.683** seções no primeiro turno e **5.146** no segundo turno.

Diversas seções permaneceram recebendo votos por mais de **12 horas**, 3 horas além do período regular.

### Primeiro Turno

<details>
    <summary>Expandir lista</summary>

| UF | 9 - 10 horas | 10 - 11 horas | 11 - 12 horas | + 12 horas |
| --- | ---: | ---: | ---: | ---: |
| AC (ACRE)  | 262 | 40 | 9 | 0 |
| AL (ALAGOAS)  | 2360 | 1392 | 441 | 58 |
| AM (AMAZONAS)  | 3004 | 899 | 297 | 109 |
| AP (AMAPÁ)  | 667 | 127 | 28 | 5 |
| BA (BAHIA)  | 11678 | 4941 | 1448 | 435 |
| CE (CEARÁ)  | 7699 | 2061 | 428 | 115 |
| DF (DISTRITO FEDERAL)  | 923 | 70 | 2 | 0 |
| ES (ESPÍRITO SANTO)  | 531 | 29 | 3 | 0 |
| GO (GOIÁS)  | 3788 | 590 | 54 | 4 |
| MA (MARANHÃO)  | 5193 | 1985 | 811 | 445 |
| MG (MINAS GERAIS)  | 10757 | 1589 | 229 | 32 |
| MS (MATO GROSSO DO SUL)  | 631 | 69 | 5 | 3 |
| MT (MATO GROSSO)  | 1928 | 312 | 33 | 4 |
| PA (PARÁ)  | 6340 | 2977 | 859 | 242 |
| PB (PARAÍBA)  | 3680 | 1021 | 180 | 17 |
| PE (PERNAMBUCO)  | 7725 | 2789 | 780 | 161 |
| PI (PIAUÍ)  | 2430 | 849 | 363 | 214 |
| PR (PARANÁ)  | 3930 | 507 | 102 | 26 |
| RJ (RIO DE JANEIRO)  | 12099 | 3064 | 495 | 109 |
| RN (RIO GRANDE DO NORTE)  | 2600 | 1242 | 279 | 53 |
| RO (RONDÔNIA)  | 399 | 107 | 42 | 7 |
| RR (RORAIMA)  | 192 | 34 | 12 | 0 |
| RS (RIO GRANDE DO SUL)  | 2876 | 367 | 44 | 7 |
| SC (SANTA CATARINA)  | 3245 | 432 | 46 | 3 |
| SE (SERGIPE)  | 1276 | 299 | 37 | 11 |
| SP (SÃO PAULO)  | 17321 | 965 | 57 | 5 |
| TO (TOCANTINS)  | 907 | 186 | 39 | 5 |
| ZZ (EXTERIOR)  | 160 | 48 | 29 | 0 |

</details>

### Segundo Turno

<details>
    <summary>Expandir lista</summary>

| UF | 9 - 10 horas | 10 - 11 horas | 11 - 12 horas | + 12 horas |
| --- | ---: | ---: | ---: | ---: |
| AC (ACRE)  | 262 | 40 | 9 | 0 |
| AL (ALAGOAS)  | 2360 | 1392 | 441 | 58 |
| AM (AMAZONAS)  | 3004 | 899 | 297 | 109 |
| AP (AMAPÁ)  | 667 | 127 | 28 | 5 |
| BA (BAHIA)  | 11678 | 4941 | 1448 | 435 |
| CE (CEARÁ)  | 7699 | 2061 | 428 | 115 |
| DF (DISTRITO FEDERAL)  | 923 | 70 | 2 | 0 |
| ES (ESPÍRITO SANTO)  | 531 | 29 | 3 | 0 |
| GO (GOIÁS)  | 3788 | 590 | 54 | 4 |
| MA (MARANHÃO)  | 5193 | 1985 | 811 | 445 |
| MG (MINAS GERAIS)  | 10757 | 1589 | 229 | 32 |
| MS (MATO GROSSO DO SUL)  | 631 | 69 | 5 | 3 |
| MT (MATO GROSSO)  | 1928 | 312 | 33 | 4 |
| PA (PARÁ)  | 6340 | 2977 | 859 | 242 |
| PB (PARAÍBA)  | 3680 | 1021 | 180 | 17 |
| PE (PERNAMBUCO)  | 7725 | 2789 | 780 | 161 |
| PI (PIAUÍ)  | 2430 | 849 | 363 | 214 |
| PR (PARANÁ)  | 3930 | 507 | 102 | 26 |
| RJ (RIO DE JANEIRO)  | 12099 | 3064 | 495 | 109 |
| RN (RIO GRANDE DO NORTE)  | 2600 | 1242 | 279 | 53 |
| RO (RONDÔNIA)  | 399 | 107 | 42 | 7 |
| RR (RORAIMA)  | 192 | 34 | 12 | 0 |
| RS (RIO GRANDE DO SUL)  | 2876 | 367 | 44 | 7 |
| SC (SANTA CATARINA)  | 3245 | 432 | 46 | 3 |
| SE (SERGIPE)  | 1276 | 299 | 37 | 11 |
| SP (SÃO PAULO)  | 17321 | 965 | 57 | 5 |
| TO (TOCANTINS)  | 907 | 186 | 39 | 5 |
| ZZ (EXTERIOR)  | 160 | 48 | 29 | 0 |

</details>

## Votos para Deputados Estaduais e Deputados Federais trocados no arquivo .bu

No arquivo `.bu`, cada lista de votos (`TotalVotosVotavel`) está contida em uma série de outras listas que definem o tipo de cargo (majoritário ou proporcional), e o cargo constitucional dos votos daquela lista (Deputado Federal, Estadual ou Distrital, Senador, Governador ou Presidente).

Na Unidade Federativa SP (São Paulo), alguns arquivos `.bu` tiveram votos para um candidato a deputado federal com número **3043**. Ocorre que **não há, neste estado, candidato a deputado federal com este número**. Observando com cuidado, percebe-se que na verdade, os votos deveriam ter sido computados para um deputado estadual com número **43043**.

No TSE a contagem de votos aparece correta, assim como na imagem do boletim de urna (IMGBU). Porém, este defeito levanta dúvidas sobre a confiabilidade do software da urna durante a geração dos arquivos. Estes arquivos não deveriam apresentar problemas, especialmente de consistência. Se fossem arquivos corrompidos, ainda poderia-se justificar alguma falha na mídia de armazenamento ou durante a transmissão. Mas não é este o caso, o arquivo foi assinado e é válido, porém seus dados são inconsistentes.

Este defeito ocorreu em 235 seções eleitorais do estado de São Paulo:

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
- Município 63770 Zona eleitoral 0426 Seção 0204
- Município 63770 Zona eleitoral 0426 Seção 0067
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
- Município 72370 Zona eleitoral 0345 Seção 0198

Fim do relatório.
