# Relatório das Eleições de 2022

Este relatório tem por objetivo demonstrar alguns defeitos nos dados de urna disponibilizados pelo TSE, e também obter algumas análises interessantes.

## Considerações sobre defeitos nos arquivos do TSE.

Alguns dos dados apresentados abaixo podem estar incorretos pois os próprios arquivos disponibilizados pelo TSE estão incorretos. É importante detalhar quais são estes defeitos para que os dados possam ser corretamente interpretados.

Tanto no primeiro quanto no segundo turno há uma diferença na **quantidade de seções**. 
No estado do Amazonas o TSE reporta que há **7.454** seções, porém só há **7.453** arquivos de seção para carregar, um a menos do que o esperado.
Como não houve diferença de votos, conclui-se que o TSE simplesmente ignorou os votos de uma seção no Amazonas.
A mesma situação ocorre com a UF "ZZ" (que trata das seções eleitorais fora do Brasil). Neste caso, foram **50 seções** faltando no primeiro turno e **47 seções** faltando no segundo turno.
São seções que não tiveram seus votos considerados no resultado final.

No primeiro turno há, ainda, uma diferença na contagem de votos. Somando os votos dos arquivos da UF "ZZ", são reportados **304.027** votos, mas o site do TSE apresenta uma contagem de **304.032**. São **5** votos a mais.

A ausência desta seções e a diferença de votos não mudaria o resultado final, porém isso afeta a credibilidade e a lisura do processo eleitoral. Não deveria haver nenhum tipo de falha injustificada na apuração dos votos.

### Primeiro Turno

- UF AM - Quantidade de seções eleitorais carregadas (7.453) é diferente do TSE (7.454).
- UF ZZ - Quantidade de seções eleitorais carregadas (1.014) é diferente do TSE (1.064).
- UF ZZ - Quantidade de votos válidos para presidente carregados (304.027) é diferente do TSE (304.032).

### Segundo Turno

- UF AM - Quantidade de seções eleitorais carregadas (7.453) é diferente do TSE (7.454).
- UF ZZ - Quantidade de seções eleitorais carregadas (1.017) é diferente do TSE (1.064).

## Seções eleitorais com Códigos de Identificação de Urna Eletrônica repetidos

Existem **15** Códigos de Identificação de Urna Eletrônica que se repetem para duas ou mais seções eleitorais no primeiro turno, e **6** no segundo turno.

Cada urna não deveria ter seu próprio número de série único?

### Primeiro Turno

#### Código Identificador de Urna Eletrônica: 1296316 - Quantidade de ocorrências: 26.

- ZZ(EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003.
- ZZ(EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495.
- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0590.
- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0662.
- ZZ(EXTERIOR), Município 29386 (BERLIM), Zona 0001, Seção 3057.
- ZZ(EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380.
- ZZ(EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123.
- ZZ(EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1740.
- ZZ(EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703.
- ZZ(EXTERIOR), Município 29947 (LIMA), Zona 0001, Seção 0154.
- ZZ(EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0591.
- ZZ(EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1355.
- ZZ(EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1632.
- ZZ(EXTERIOR), Município 30082 (MANILA), Zona 0001, Seção 0990.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0504.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 3040.
- ZZ(EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0165.
- ZZ(EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0247.
- ZZ(EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228.
- ZZ(EXTERIOR), Município 30228 (NOVA YORK), Zona 0001, Seção 0297.
- ZZ(EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504.
- ZZ(EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384.
- ZZ(EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1488.
- ZZ(EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3268.
- ZZ(EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1964.
- ZZ(EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524.

#### Código Identificador de Urna Eletrônica: 1273645 - Quantidade de ocorrências: 15.

- ZZ(EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0051.
- ZZ(EXTERIOR), Município 29807 (HOUSTON), Zona 0001, Seção 0940.
- ZZ(EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390.
- ZZ(EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150.
- ZZ(EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1640.
- ZZ(EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 1029.
- ZZ(EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0768.
- ZZ(EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0796.
- ZZ(EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1858.
- ZZ(EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1886.
- ZZ(EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353.
- ZZ(EXTERIOR), Município 30562 (SYDNEY), Zona 0001, Seção 1375.
- ZZ(EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1821.
- ZZ(EXTERIOR), Município 30783 (WASHINGTON), Zona 0001, Seção 0458.
- ZZ(EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004.

#### Código Identificador de Urna Eletrônica: 1274462 - Quantidade de ocorrências: 12.

- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0545.
- ZZ(EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735.
- ZZ(EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0072.
- ZZ(EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1909.
- ZZ(EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127.
- ZZ(EXTERIOR), Município 29998 (LUANDA), Zona 0001, Seção 0199.
- ZZ(EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0024.
- ZZ(EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490.
- ZZ(EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182.
- ZZ(EXTERIOR), Município 30619 (TEL AVIV), Zona 0001, Seção 0682.
- ZZ(EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1787.
- ZZ(EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444.

#### Código Identificador de Urna Eletrônica: 1246419 - Quantidade de ocorrências: 12.

- ZZ(EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327.
- ZZ(EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0053.
- ZZ(EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0071.
- ZZ(EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1912.
- ZZ(EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1671.
- ZZ(EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259.
- ZZ(EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0026.
- ZZ(EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0399.
- ZZ(EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0426.
- ZZ(EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0168.
- ZZ(EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1820.
- ZZ(EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1966.

#### Código Identificador de Urna Eletrônica: 1295943 - Quantidade de ocorrências: 11.

- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0719.
- ZZ(EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558.
- ZZ(EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1750.
- ZZ(EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494.
- ZZ(EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0541.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1125.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1132.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1164.
- ZZ(EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0248.
- ZZ(EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0758.
- ZZ(EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1967.

#### Código Identificador de Urna Eletrônica: 1340042 - Quantidade de ocorrências: 9.

- ZZ(EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001.
- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0643.
- ZZ(EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096.
- ZZ(EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146.
- ZZ(EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0540.
- ZZ(EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0230.
- ZZ(EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0442.
- ZZ(EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1818.
- ZZ(EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1031.

#### Código Identificador de Urna Eletrônica: 1252874 - Quantidade de ocorrências: 8.

- ZZ(EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0551.
- ZZ(EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028.
- ZZ(EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712.
- ZZ(EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0394.
- ZZ(EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647.
- ZZ(EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340.
- ZZ(EXTERIOR), Município 30252 (OTTAWA), Zona 0001, Seção 0767.
- ZZ(EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1765.

#### Código Identificador de Urna Eletrônica: 1229330 - Quantidade de ocorrências: 6.

- ZZ(EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 1041.
- ZZ(EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1637.
- ZZ(EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0432.
- ZZ(EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0164.
- ZZ(EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764.
- ZZ(EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1690.

#### Código Identificador de Urna Eletrônica: 1620697 - Quantidade de ocorrências: 2.

- RS(RIO GRANDE DO SUL), Município 87378 (MARAU), Zona 0062, Seção 0035.
- RS(RIO GRANDE DO SUL), Município 87181 (NICOLAU VERGUEIRO), Zona 0062, Seção 0076.

#### Código Identificador de Urna Eletrônica: 1316810 - Quantidade de ocorrências: 2.

- MG(MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0041.
- MG(MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0282.

#### Código Identificador de Urna Eletrônica: 1268286 - Quantidade de ocorrências: 2.

- AM(AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0116.
- AM(AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0174.

#### Código Identificador de Urna Eletrônica: 1095313 - Quantidade de ocorrências: 2.

- SP(SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0524.
- SP(SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0529.

#### Código Identificador de Urna Eletrônica: 1149151 - Quantidade de ocorrências: 2.

- MT(MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0126.
- MT(MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0136.

#### Código Identificador de Urna Eletrônica: 1293146 - Quantidade de ocorrências: 2.

- MG(MINAS GERAIS), Município 46450 (ITAIPÉ), Zona 0196, Seção 0037.
- MG(MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173.

#### Código Identificador de Urna Eletrônica: 1612929 - Quantidade de ocorrências: 2.

- RN(RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0028.
- RN(RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0077.

### Segundo Turno

#### Código Identificador de Urna Eletrônica: 1273426 - Quantidade de ocorrências: 12.

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

#### Código Identificador de Urna Eletrônica: 1273414 - Quantidade de ocorrências: 11.

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

#### Código Identificador de Urna Eletrônica: 1255254 - Quantidade de ocorrências: 10.

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

#### Código Identificador de Urna Eletrônica: 1809943 - Quantidade de ocorrências: 5.

- PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0017.
- PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0018.
- PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0019.
- PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0037.
- PA(PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0055.

#### Código Identificador de Urna Eletrônica: 1778800 - Quantidade de ocorrências: 2.

- SC(SANTA CATARINA), Município 83011 (SALETE), Zona 0046, Seção 0021.
- SC(SANTA CATARINA), Município 83011 (SALETE), Zona 0046, Seção 0022.

#### Código Identificador de Urna Eletrônica: 1791872 - Quantidade de ocorrências: 2.

- GO(GOIÁS), Município 92886 (COCALZINHO DE GOIÁS), Zona 0026, Seção 0131.
- GO(GOIÁS), Município 95435 (PIRENÓPOLIS), Zona 0026, Seção 0111.

## Candidatos a presidente com 0 votos em seções eleitorais.

A seguinte lista faz uma comparação entre os 2 maiores candidatos a presidente nas eleições de 2020 (Bolsonaro e Lula), e mostra em quais seções cada um deles não obteve nenhum voto.

### Primeiro Turno

#### Quantidade de votos para o Bolsonaro nas seções eleitorais que não tiveram votos para o Lula: 58.

- UF PA (PARÁ), Qtd Votos Bolsonaro: 58.
#### Quantidade de votos para o Lula nas seções eleitorais que não tiveram votos para o Bolsonaro: 17.531.

- UF AM (AMAZONAS), Qtd Votos Lula: 4.794.
- UF BA (BAHIA), Qtd Votos Lula: 2.146.
- UF CE (CEARÁ), Qtd Votos Lula: 1.031.
- UF MA (MARANHÃO), Qtd Votos Lula: 5.354.
- UF MG (MINAS GERAIS), Qtd Votos Lula: 589.
- UF MS (MATO GROSSO DO SUL), Qtd Votos Lula: 212.
- UF MT (MATO GROSSO), Qtd Votos Lula: 1.157.
- UF PA (PARÁ), Qtd Votos Lula: 705.
- UF PI (PIAUÍ), Qtd Votos Lula: 726.
- UF RR (RORAIMA), Qtd Votos Lula: 337.
- UF SC (SANTA CATARINA), Qtd Votos Lula: 27.
- UF SE (SERGIPE), Qtd Votos Lula: 14.
- UF SP (SÃO PAULO), Qtd Votos Lula: 103.
- UF TO (TOCANTINS), Qtd Votos Lula: 332.
- UF ZZ (EXTERIOR), Qtd Votos Lula: 4.

### Segundo Turno

#### Quantidade de votos para o Bolsonaro nas seções eleitorais que não tiveram votos para o Lula: 124.

- UF PA (PARÁ), Qtd Votos Bolsonaro: 39.
- UF RS (RIO GRANDE DO SUL), Qtd Votos Bolsonaro: 79.
- UF ZZ (EXTERIOR), Qtd Votos Bolsonaro: 6.

#### Quantidade de votos para o Lula nas seções eleitorais que não tiveram votos para o Bolsonaro: 16.455.

- UF AC (ACRE), Qtd Votos Lula: 254.
- UF AM (AMAZONAS), Qtd Votos Lula: 4.221.
- UF BA (BAHIA), Qtd Votos Lula: 1.553.
- UF CE (CEARÁ), Qtd Votos Lula: 718.
- UF MA (MARANHÃO), Qtd Votos Lula: 5.164.
- UF MG (MINAS GERAIS), Qtd Votos Lula: 840.
- UF MS (MATO GROSSO DO SUL), Qtd Votos Lula: 206.
- UF MT (MATO GROSSO), Qtd Votos Lula: 1.067.
- UF PA (PARÁ), Qtd Votos Lula: 754.
- UF PI (PIAUÍ), Qtd Votos Lula: 561.
- UF PR (PARANÁ), Qtd Votos Lula: 223.
- UF RR (RORAIMA), Qtd Votos Lula: 231.
- UF RS (RIO GRANDE DO SUL), Qtd Votos Lula: 339.
- UF SC (SANTA CATARINA), Qtd Votos Lula: 28.
- UF SE (SERGIPE), Qtd Votos Lula: 17.
- UF SP (SÃO PAULO), Qtd Votos Lula: 145.
- UF TO (TOCANTINS), Qtd Votos Lula: 97.
- UF ZZ (EXTERIOR), Qtd Votos Lula: 37.

Abaixo a lista completa das Seções que tiveram 0 votos para um dos dois candidatos

### Primeiro Turno

#### Quantidade de Seções eleitorais que não tiveram votos para o Lula: 1.

- UF PA (PARÁ), Município 04383 (NOVO PROGRESSO), Zona 0091, Seção 0073, Qtd Votos Bolsonaro: 58 - 100,00% do total.

#### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: 161.

- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0025, Qtd Votos Lula: 142 - 96,60% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0028, Qtd Votos Lula: 126 - 99,21% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0029, Qtd Votos Lula: 133 - 91,10% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0030, Qtd Votos Lula: 129 - 98,47% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0037, Qtd Votos Lula: 103 - 99,04% do total.
- UF AM (AMAZONAS), Município 02119 (BARREIRINHA), Zona 0026, Seção 0062, Qtd Votos Lula: 308 - 96,55% do total.
- UF AM (AMAZONAS), Município 02216 (CARAUARI), Zona 0021, Seção 0096, Qtd Votos Lula: 89 - 100,00% do total.
- UF AM (AMAZONAS), Município 02275 (CODAJÁS), Zona 0007, Seção 0046, Qtd Votos Lula: 43 - 95,56% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0022, Qtd Votos Lula: 165 - 99,40% do total.
- UF AM (AMAZONAS), Município 02410 (ITACOATIARA), Zona 0003, Seção 0158, Qtd Votos Lula: 39 - 100,00% do total.
- UF AM (AMAZONAS), Município 02453 (JAPURÁ), Zona 0048, Seção 0012, Qtd Votos Lula: 171 - 99,42% do total.
- UF AM (AMAZONAS), Município 02690 (PARINTINS), Zona 0004, Seção 0130, Qtd Votos Lula: 227 - 99,56% do total.
- UF AM (AMAZONAS), Município 02690 (PARINTINS), Zona 0004, Seção 0232, Qtd Votos Lula: 119 - 97,54% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0009, Qtd Votos Lula: 331 - 98,81% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0045, Qtd Votos Lula: 191 - 96,46% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0057, Qtd Votos Lula: 197 - 96,57% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0080, Qtd Votos Lula: 326 - 99,69% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0086, Qtd Votos Lula: 357 - 99,17% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0013, Qtd Votos Lula: 241 - 99,59% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0048, Qtd Votos Lula: 167 - 99,40% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0059, Qtd Votos Lula: 136 - 99,27% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0065, Qtd Votos Lula: 184 - 98,92% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0087, Qtd Votos Lula: 139 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0092, Qtd Votos Lula: 189 - 99,47% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0061, Qtd Votos Lula: 256 - 94,12% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0098, Qtd Votos Lula: 142 - 95,95% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0100, Qtd Votos Lula: 144 - 96,00% do total.
- UF BA (BAHIA), Município 30759 (BARRO ALTO), Zona 0174, Seção 0050, Qtd Votos Lula: 65 - 100,00% do total.
- UF BA (BAHIA), Município 33090 (ÉRICO CARDOSO), Zona 0111, Seção 0250, Qtd Votos Lula: 47 - 97,92% do total.
- UF BA (BAHIA), Município 33260 (ANDORINHA), Zona 0045, Seção 0250, Qtd Votos Lula: 213 - 96,82% do total.
- UF BA (BAHIA), Município 33596 (BARRA DO MENDES), Zona 0176, Seção 0151, Qtd Votos Lula: 69 - 100,00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0254, Qtd Votos Lula: 183 - 97,34% do total.
- UF BA (BAHIA), Município 33898 (BROTAS DE MACAÚBAS), Zona 0094, Seção 0028, Qtd Votos Lula: 28 - 82,35% do total.
- UF BA (BAHIA), Município 34975 (CURAÇÁ), Zona 0085, Seção 0079, Qtd Votos Lula: 108 - 95,58% do total.
- UF BA (BAHIA), Município 35157 (FEIRA DE SANTANA), Zona 0155, Seção 0800, Qtd Votos Lula: 21 - 100,00% do total.
- UF BA (BAHIA), Município 35378 (IAÇU), Zona 0193, Seção 0116, Qtd Votos Lula: 42 - 100,00% do total.
- UF BA (BAHIA), Município 35637 (IBITITÁ), Zona 0104, Seção 0241, Qtd Votos Lula: 48 - 96,00% do total.
- UF BA (BAHIA), Município 36595 (JANDAÍRA), Zona 0049, Seção 0155, Qtd Votos Lula: 83 - 97,65% do total.
- UF BA (BAHIA), Município 36692 (JUAZEIRO), Zona 0047, Seção 0276, Qtd Votos Lula: 47 - 95,92% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0053, Qtd Votos Lula: 107 - 98,17% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0057, Qtd Votos Lula: 70 - 100,00% do total.
- UF BA (BAHIA), Município 37591 (NOVA SOURE), Zona 0079, Seção 0152, Qtd Votos Lula: 123 - 96,85% do total.
- UF BA (BAHIA), Município 37737 (PARAMIRIM), Zona 0111, Seção 0208, Qtd Votos Lula: 57 - 98,28% do total.
- UF BA (BAHIA), Município 37770 (PARIPIRANGA), Zona 0052, Seção 0182, Qtd Votos Lula: 51 - 100,00% do total.
- UF BA (BAHIA), Município 37877 (PIATÃ), Zona 0105, Seção 0062, Qtd Votos Lula: 31 - 86,11% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0068, Qtd Votos Lula: 91 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0076, Qtd Votos Lula: 64 - 96,97% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0100, Qtd Votos Lula: 91 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0111, Qtd Votos Lula: 101 - 96,19% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0132, Qtd Votos Lula: 172 - 100,00% do total.
- UF BA (BAHIA), Município 38130 (PRESIDENTE DUTRA), Zona 0095, Seção 0516, Qtd Votos Lula: 69 - 98,57% do total.
- UF BA (BAHIA), Município 39292 (TEOFILÂNDIA), Zona 0123, Seção 0046, Qtd Votos Lula: 47 - 100,00% do total.
- UF BA (BAHIA), Município 39896 (SÃO GABRIEL), Zona 0095, Seção 0321, Qtd Votos Lula: 49 - 94,23% do total.
- UF BA (BAHIA), Município 39942 (NOVO HORIZONTE), Zona 0088, Seção 0304, Qtd Votos Lula: 69 - 100,00% do total.
- UF CE (CEARÁ), Município 13102 (QUITERIANÓPOLIS), Zona 0099, Seção 0141, Qtd Votos Lula: 113 - 97,41% do total.
- UF CE (CEARÁ), Município 13277 (ARATUBA), Zona 0105, Seção 0175, Qtd Votos Lula: 89 - 96,74% do total.
- UF CE (CEARÁ), Município 13536 (CAMPOS SALES), Zona 0038, Seção 0146, Qtd Votos Lula: 48 - 90,57% do total.
- UF CE (CEARÁ), Município 13552 (CANINDÉ), Zona 0033, Seção 0413, Qtd Votos Lula: 117 - 97,50% do total.
- UF CE (CEARÁ), Município 13951 (GRANJA), Zona 0025, Seção 0208, Qtd Votos Lula: 71 - 98,61% do total.
- UF CE (CEARÁ), Município 14290 (ITAPIPOCA), Zona 0017, Seção 0437, Qtd Votos Lula: 155 - 99,36% do total.
- UF CE (CEARÁ), Município 14630 (MAURITI), Zona 0076, Seção 0146, Qtd Votos Lula: 56 - 86,15% do total.
- UF CE (CEARÁ), Município 15210 (PORANGA), Zona 0040, Seção 0163, Qtd Votos Lula: 67 - 100,00% do total.
- UF CE (CEARÁ), Município 15431 (SANTANA DO CARIRI), Zona 0053, Seção 0050, Qtd Votos Lula: 151 - 98,05% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0101, Qtd Votos Lula: 80 - 95,24% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0102, Qtd Votos Lula: 84 - 97,67% do total.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0174, Qtd Votos Lula: 71 - 98,61% do total.
- UF MA (MARANHÃO), Município 07110 (AMARANTE DO MARANHÃO), Zona 0099, Seção 0127, Qtd Votos Lula: 77 - 100,00% do total.
- UF MA (MARANHÃO), Município 07250 (BACURI), Zona 0107, Seção 0038, Qtd Votos Lula: 109 - 99,09% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0175, Qtd Votos Lula: 91 - 100,00% do total.
- UF MA (MARANHÃO), Município 07390 (BREJO), Zona 0024, Seção 0253, Qtd Votos Lula: 57 - 93,44% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0185, Qtd Votos Lula: 274 - 100,00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0186, Qtd Votos Lula: 278 - 100,00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0205, Qtd Votos Lula: 300 - 100,00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0149, Qtd Votos Lula: 31 - 100,00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0175, Qtd Votos Lula: 55 - 100,00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0193, Qtd Votos Lula: 20 - 100,00% do total.
- UF MA (MARANHÃO), Município 07773 (ESPERANTINÓPOLIS), Zona 0061, Seção 0140, Qtd Votos Lula: 71 - 98,61% do total.
- UF MA (MARANHÃO), Município 07927 (MARANHÃOZINHO), Zona 0101, Seção 0078, Qtd Votos Lula: 303 - 99,67% do total.
- UF MA (MARANHÃO), Município 07951 (GUIMARÃES), Zona 0030, Seção 0061, Qtd Votos Lula: 87 - 100,00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0051, Qtd Votos Lula: 134 - 98,53% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0208, Qtd Votos Lula: 50 - 98,04% do total.
- UF MA (MARANHÃO), Município 08044 (PAULINO NEVES), Zona 0040, Seção 0262, Qtd Votos Lula: 108 - 100,00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0258, Qtd Votos Lula: 88 - 100,00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0337, Qtd Votos Lula: 90 - 98,90% do total.
- UF MA (MARANHÃO), Município 08141 (PRESIDENTE SARNEY), Zona 0106, Seção 0244, Qtd Votos Lula: 75 - 100,00% do total.
- UF MA (MARANHÃO), Município 08150 (LAGO DO JUNCO), Zona 0074, Seção 0295, Qtd Votos Lula: 66 - 98,51% do total.
- UF MA (MARANHÃO), Município 08222 (SANTANA DO MARANHÃO), Zona 0051, Seção 0175, Qtd Votos Lula: 43 - 100,00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0133, Qtd Votos Lula: 85 - 100,00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0170, Qtd Votos Lula: 105 - 96,33% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0170, Qtd Votos Lula: 43 - 97,73% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0190, Qtd Votos Lula: 68 - 95,77% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0198, Qtd Votos Lula: 110 - 99,10% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0028, Qtd Votos Lula: 261 - 100,00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0080, Qtd Votos Lula: 268 - 99,63% do total.
- UF MA (MARANHÃO), Município 08419 (MORROS), Zona 0110, Seção 0221, Qtd Votos Lula: 142 - 97,93% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0089, Qtd Votos Lula: 71 - 100,00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0156, Qtd Votos Lula: 58 - 95,08% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0148, Qtd Votos Lula: 73 - 100,00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0159, Qtd Votos Lula: 88 - 100,00% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0063, Qtd Votos Lula: 118 - 98,33% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0070, Qtd Votos Lula: 61 - 95,31% do total.
- UF MA (MARANHÃO), Município 08753 (POÇÃO DE PEDRAS), Zona 0061, Seção 0114, Qtd Votos Lula: 65 - 95,59% do total.
- UF MA (MARANHÃO), Município 08834 (PRESIDENTE VARGAS), Zona 0050, Seção 0151, Qtd Votos Lula: 120 - 99,17% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0089, Qtd Votos Lula: 91 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0090, Qtd Votos Lula: 36 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0193, Qtd Votos Lula: 52 - 92,86% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0202, Qtd Votos Lula: 57 - 93,44% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0186, Qtd Votos Lula: 58 - 100,00% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0352, Qtd Votos Lula: 83 - 100,00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0098, Qtd Votos Lula: 113 - 100,00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0163, Qtd Votos Lula: 85 - 100,00% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0116, Qtd Votos Lula: 160 - 98,77% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0167, Qtd Votos Lula: 89 - 98,89% do total.
- UF MA (MARANHÃO), Município 09415 (TURIAÇU), Zona 0039, Seção 0206, Qtd Votos Lula: 81 - 98,78% do total.
- UF MA (MARANHÃO), Município 09555 (BOM JARDIM), Zona 0078, Seção 0251, Qtd Votos Lula: 65 - 97,01% do total.
- UF MA (MARANHÃO), Município 09555 (BOM JARDIM), Zona 0078, Seção 0283, Qtd Votos Lula: 170 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 40320 (JUATUBA), Zona 0172, Seção 0183, Qtd Votos Lula: 22 - 95,65% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0178, Qtd Votos Lula: 152 - 99,35% do total.
- UF MG (MINAS GERAIS), Município 42757 (CARMÉSIA), Zona 0113, Seção 0042, Qtd Votos Lula: 80 - 96,39% do total.
- UF MG (MINAS GERAIS), Município 46795 (ITINGA), Zona 0015, Seção 0213, Qtd Votos Lula: 45 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0291, Qtd Votos Lula: 70 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0380, Qtd Votos Lula: 46 - 93,88% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0171, Qtd Votos Lula: 69 - 98,57% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173, Qtd Votos Lula: 16 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 50431 (PORTEIRINHA), Zona 0226, Seção 0219, Qtd Votos Lula: 27 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 52477 (SÃO JOÃO DA PONTE), Zona 0255, Seção 0146, Qtd Votos Lula: 62 - 96,88% do total.
- UF MS (MATO GROSSO DO SUL), Município 91804 (DOIS IRMÃOS DO BURITI), Zona 0049, Seção 0068, Qtd Votos Lula: 212 - 96,80% do total.
- UF MT (MATO GROSSO), Município 90280 (CONFRESA), Zona 0028, Seção 0158, Qtd Votos Lula: 375 - 100,00% do total.
- UF MT (MATO GROSSO), Município 90824 (GAÚCHA DO NORTE), Zona 0057, Seção 0078, Qtd Votos Lula: 122 - 100,00% do total.
- UF MT (MATO GROSSO), Município 91979 (SANTA TEREZINHA), Zona 0016, Seção 0086, Qtd Votos Lula: 246 - 99,60% do total.
- UF MT (MATO GROSSO), Município 98655 (CAMPINÁPOLIS), Zona 0026, Seção 0189, Qtd Votos Lula: 251 - 100,00% do total.
- UF MT (MATO GROSSO), Município 98817 (PEIXOTO DE AZEVEDO), Zona 0033, Seção 0255, Qtd Votos Lula: 112 - 100,00% do total.
- UF MT (MATO GROSSO), Município 98850 (PORTO ALEGRE DO NORTE), Zona 0028, Seção 0191, Qtd Votos Lula: 51 - 100,00% do total.
- UF PA (PARÁ), Município 04073 (ALENQUER), Zona 0021, Seção 0234, Qtd Votos Lula: 71 - 98,61% do total.
- UF PA (PARÁ), Município 04146 (SANTA LUZIA DO PARÁ), Zona 0041, Seção 0198, Qtd Votos Lula: 76 - 96,20% do total.
- UF PA (PARÁ), Município 04359 (BREVES), Zona 0015, Seção 0289, Qtd Votos Lula: 29 - 100,00% do total.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0092, Qtd Votos Lula: 27 - 90,00% do total.
- UF PA (PARÁ), Município 05037 (OEIRAS DO PARÁ), Zona 0045, Seção 0044, Qtd Votos Lula: 137 - 95,14% do total.
- UF PA (PARÁ), Município 05037 (OEIRAS DO PARÁ), Zona 0045, Seção 0084, Qtd Votos Lula: 71 - 100,00% do total.
- UF PA (PARÁ), Município 05053 (ORIXIMINÁ), Zona 0038, Seção 0260, Qtd Votos Lula: 122 - 100,00% do total.
- UF PA (PARÁ), Município 05452 (SÃO FÉLIX DO XINGU), Zona 0053, Seção 0217, Qtd Votos Lula: 123 - 100,00% do total.
- UF PA (PARÁ), Município 05657 (VISEU), Zona 0014, Seção 0221, Qtd Votos Lula: 49 - 90,74% do total.
- UF PI (PIAUÍ), Município 10243 (CORONEL JOSÉ DIAS), Zona 0013, Seção 0244, Qtd Votos Lula: 92 - 97,87% do total.
- UF PI (PIAUÍ), Município 10502 (SÃO BRAZ DO PIAUÍ), Zona 0095, Seção 0050, Qtd Votos Lula: 116 - 97,48% do total.
- UF PI (PIAUÍ), Município 10693 (ELESBÃO VELOSO), Zona 0048, Seção 0097, Qtd Votos Lula: 170 - 97,70% do total.
- UF PI (PIAUÍ), Município 10715 (ELISEU MARTINS), Zona 0067, Seção 0037, Qtd Votos Lula: 83 - 100,00% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0063, Qtd Votos Lula: 89 - 96,74% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0071, Qtd Votos Lula: 43 - 97,73% do total.
- UF PI (PIAUÍ), Município 11398 (OEIRAS), Zona 0005, Seção 0356, Qtd Votos Lula: 38 - 100,00% do total.
- UF PI (PIAUÍ), Município 12033 (SÃO JOSÉ DO PIAUÍ), Zona 0064, Seção 0073, Qtd Votos Lula: 95 - 100,00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0061, Qtd Votos Lula: 239 - 100,00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0104, Qtd Votos Lula: 98 - 98,00% do total.
- UF SC (SANTA CATARINA), Município 80942 (SÃO CRISTÓVÃO DO SUL), Zona 0011, Seção 0166, Qtd Votos Lula: 27 - 100,00% do total.
- UF SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0304, Qtd Votos Lula: 14 - 87,50% do total.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0341, Qtd Votos Lula: 16 - 94,12% do total.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0176, Seção 0471, Qtd Votos Lula: 38 - 97,44% do total.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0217, Seção 0353, Qtd Votos Lula: 33 - 97,06% do total.
- UF SP (SÃO PAULO), Município 69698 (RIBEIRÃO PRETO), Zona 0305, Seção 0333, Qtd Votos Lula: 16 - 94,12% do total.
- UF TO (TOCANTINS), Município 95257 (PEDRO AFONSO), Zona 0023, Seção 0104, Qtd Votos Lula: 66 - 100,00% do total.
- UF TO (TOCANTINS), Município 95338 (GOIATINS), Zona 0032, Seção 0070, Qtd Votos Lula: 266 - 100,00% do total.
- UF ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Qtd Votos Lula: 4 - 100,00% do total.

### Segundo Turno

#### Quantidade de Seções eleitorais que não tiveram votos para o Lula: 4.

- UF PA (PARÁ), Município 04499 (CHAVES), Zona 0017, Seção 0041, Qtd Votos Bolsonaro: 39 - 100,00% do total.
- UF RS (RIO GRANDE DO SUL), Município 86568 (CHARRUA), Zona 0100, Seção 0015, Qtd Votos Bolsonaro: 79 - 100,00% do total.
- UF ZZ (EXTERIOR), Município 29505 (CARACAS), Zona 0001, Seção 0078, Qtd Votos Bolsonaro: 5 - 100,00% do total.
- UF ZZ (EXTERIOR), Município 29505 (CARACAS), Zona 0001, Seção 0079, Qtd Votos Bolsonaro: 1 - 100,00% do total.

#### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: 143.

- UF AC (ACRE), Município 01040 (MARECHAL THAUMATURGO), Zona 0004, Seção 0326, Qtd Votos Lula: 153 - 100,00% do total.
- UF AC (ACRE), Município 01040 (MARECHAL THAUMATURGO), Zona 0004, Seção 0384, Qtd Votos Lula: 101 - 100,00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0025, Qtd Votos Lula: 138 - 100,00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0028, Qtd Votos Lula: 127 - 100,00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0029, Qtd Votos Lula: 175 - 100,00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0037, Qtd Votos Lula: 107 - 100,00% do total.
- UF AM (AMAZONAS), Município 02119 (BARREIRINHA), Zona 0026, Seção 0063, Qtd Votos Lula: 237 - 100,00% do total.
- UF AM (AMAZONAS), Município 02216 (CARAUARI), Zona 0021, Seção 0096, Qtd Votos Lula: 91 - 100,00% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0022, Qtd Votos Lula: 196 - 100,00% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0026, Qtd Votos Lula: 156 - 100,00% do total.
- UF AM (AMAZONAS), Município 02399 (IPIXUNA), Zona 0045, Seção 0052, Qtd Votos Lula: 94 - 100,00% do total.
- UF AM (AMAZONAS), Município 02410 (ITACOATIARA), Zona 0003, Seção 0158, Qtd Votos Lula: 37 - 100,00% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0010, Qtd Votos Lula: 303 - 100,00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0044, Qtd Votos Lula: 277 - 100,00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0080, Qtd Votos Lula: 347 - 100,00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0086, Qtd Votos Lula: 350 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0013, Qtd Votos Lula: 239 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0025, Qtd Votos Lula: 207 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0048, Qtd Votos Lula: 166 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0059, Qtd Votos Lula: 135 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0065, Qtd Votos Lula: 204 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0087, Qtd Votos Lula: 162 - 100,00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0092, Qtd Votos Lula: 195 - 100,00% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0098, Qtd Votos Lula: 146 - 100,00% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0100, Qtd Votos Lula: 132 - 100,00% do total.
- UF BA (BAHIA), Município 33413 (ARACI), Zona 0123, Seção 0137, Qtd Votos Lula: 87 - 100,00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0247, Qtd Votos Lula: 117 - 100,00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0255, Qtd Votos Lula: 60 - 100,00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0344, Qtd Votos Lula: 99 - 100,00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0345, Qtd Votos Lula: 52 - 100,00% do total.
- UF BA (BAHIA), Município 33898 (BROTAS DE MACAÚBAS), Zona 0094, Seção 0028, Qtd Votos Lula: 35 - 100,00% do total.
- UF BA (BAHIA), Município 34231 (CANARANA), Zona 0174, Seção 0111, Qtd Votos Lula: 141 - 100,00% do total.
- UF BA (BAHIA), Município 34436 (CASA NOVA), Zona 0066, Seção 0134, Qtd Votos Lula: 104 - 100,00% do total.
- UF BA (BAHIA), Município 35157 (FEIRA DE SANTANA), Zona 0155, Seção 0800, Qtd Votos Lula: 19 - 100,00% do total.
- UF BA (BAHIA), Município 35637 (IBITITÁ), Zona 0104, Seção 0241, Qtd Votos Lula: 55 - 100,00% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0057, Qtd Votos Lula: 73 - 100,00% do total.
- UF BA (BAHIA), Município 37737 (PARAMIRIM), Zona 0111, Seção 0208, Qtd Votos Lula: 60 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0037, Qtd Votos Lula: 33 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0068, Qtd Votos Lula: 87 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0076, Qtd Votos Lula: 65 - 100,00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0132, Qtd Votos Lula: 173 - 100,00% do total.
- UF BA (BAHIA), Município 38490 (SALVADOR), Zona 0005, Seção 0800, Qtd Votos Lula: 80 - 100,00% do total.
- UF BA (BAHIA), Município 38970 (SEABRA), Zona 0088, Seção 0181, Qtd Votos Lula: 141 - 100,00% do total.
- UF BA (BAHIA), Município 39942 (NOVO HORIZONTE), Zona 0088, Seção 0304, Qtd Votos Lula: 72 - 100,00% do total.
- UF CE (CEARÁ), Município 13102 (QUITERIANÓPOLIS), Zona 0099, Seção 0141, Qtd Votos Lula: 116 - 100,00% do total.
- UF CE (CEARÁ), Município 14478 (JUAZEIRO DO NORTE), Zona 0119, Seção 0267, Qtd Votos Lula: 22 - 100,00% do total.
- UF CE (CEARÁ), Município 15113 (PEDRA BRANCA), Zona 0059, Seção 0112, Qtd Votos Lula: 95 - 100,00% do total.
- UF CE (CEARÁ), Município 15113 (PEDRA BRANCA), Zona 0059, Seção 0114, Qtd Votos Lula: 73 - 100,00% do total.
- UF CE (CEARÁ), Município 15210 (PORANGA), Zona 0040, Seção 0163, Qtd Votos Lula: 69 - 100,00% do total.
- UF CE (CEARÁ), Município 15296 (QUIXERAMOBIM), Zona 0011, Seção 0186, Qtd Votos Lula: 103 - 100,00% do total.
- UF CE (CEARÁ), Município 15431 (SANTANA DO CARIRI), Zona 0053, Seção 0050, Qtd Votos Lula: 159 - 100,00% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0102, Qtd Votos Lula: 81 - 100,00% do total.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0174, Qtd Votos Lula: 73 - 100,00% do total.
- UF MA (MARANHÃO), Município 07110 (AMARANTE DO MARANHÃO), Zona 0099, Seção 0127, Qtd Votos Lula: 73 - 100,00% do total.
- UF MA (MARANHÃO), Município 07250 (BACURI), Zona 0107, Seção 0038, Qtd Votos Lula: 106 - 100,00% do total.
- UF MA (MARANHÃO), Município 07315 (BARRA DO CORDA), Zona 0023, Seção 0019, Qtd Votos Lula: 74 - 100,00% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0175, Qtd Votos Lula: 87 - 100,00% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0239, Qtd Votos Lula: 51 - 100,00% do total.
- UF MA (MARANHÃO), Município 07390 (BREJO), Zona 0024, Seção 0253, Qtd Votos Lula: 61 - 100,00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0185, Qtd Votos Lula: 283 - 100,00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0205, Qtd Votos Lula: 310 - 100,00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0207, Qtd Votos Lula: 69 - 100,00% do total.
- UF MA (MARANHÃO), Município 07552 (CARUTAPERA), Zona 0055, Seção 0069, Qtd Votos Lula: 53 - 100,00% do total.
- UF MA (MARANHÃO), Município 07706 (ITAIPAVA DO GRAJAÚ), Zona 0015, Seção 0314, Qtd Votos Lula: 277 - 100,00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0175, Qtd Votos Lula: 56 - 100,00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0193, Qtd Votos Lula: 19 - 100,00% do total.
- UF MA (MARANHÃO), Município 07927 (MARANHÃOZINHO), Zona 0101, Seção 0078, Qtd Votos Lula: 307 - 100,00% do total.
- UF MA (MARANHÃO), Município 07951 (GUIMARÃES), Zona 0030, Seção 0074, Qtd Votos Lula: 101 - 100,00% do total.
- UF MA (MARANHÃO), Município 07978 (HUMBERTO DE CAMPOS), Zona 0032, Seção 0055, Qtd Votos Lula: 39 - 100,00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0168, Qtd Votos Lula: 91 - 100,00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0197, Qtd Votos Lula: 44 - 100,00% do total.
- UF MA (MARANHÃO), Município 08060 (PEDRO DO ROSÁRIO), Zona 0106, Seção 0054, Qtd Votos Lula: 204 - 100,00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0337, Qtd Votos Lula: 89 - 100,00% do total.
- UF MA (MARANHÃO), Município 08141 (PRESIDENTE SARNEY), Zona 0106, Seção 0224, Qtd Votos Lula: 130 - 100,00% do total.
- UF MA (MARANHÃO), Município 08150 (LAGO DO JUNCO), Zona 0074, Seção 0295, Qtd Votos Lula: 66 - 100,00% do total.
- UF MA (MARANHÃO), Município 08222 (SANTANA DO MARANHÃO), Zona 0051, Seção 0175, Qtd Votos Lula: 40 - 100,00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0133, Qtd Votos Lula: 83 - 100,00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0138, Qtd Votos Lula: 98 - 100,00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0170, Qtd Votos Lula: 103 - 100,00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0028, Qtd Votos Lula: 260 - 100,00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0080, Qtd Votos Lula: 264 - 100,00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0087, Qtd Votos Lula: 60 - 100,00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0089, Qtd Votos Lula: 68 - 100,00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0156, Qtd Votos Lula: 61 - 100,00% do total.
- UF MA (MARANHÃO), Município 08559 (PARNARAMA), Zona 0036, Seção 0069, Qtd Votos Lula: 80 - 100,00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0148, Qtd Votos Lula: 70 - 100,00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0159, Qtd Votos Lula: 90 - 100,00% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0063, Qtd Votos Lula: 119 - 100,00% do total.
- UF MA (MARANHÃO), Município 08834 (PRESIDENTE VARGAS), Zona 0050, Seção 0151, Qtd Votos Lula: 121 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0072, Qtd Votos Lula: 93 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0089, Qtd Votos Lula: 92 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0090, Qtd Votos Lula: 42 - 100,00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0202, Qtd Votos Lula: 60 - 100,00% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0186, Qtd Votos Lula: 56 - 100,00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0067, Qtd Votos Lula: 213 - 100,00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0098, Qtd Votos Lula: 107 - 100,00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0163, Qtd Votos Lula: 86 - 100,00% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0167, Qtd Votos Lula: 87 - 100,00% do total.
- UF MA (MARANHÃO), Município 09415 (TURIAÇU), Zona 0039, Seção 0058, Qtd Votos Lula: 148 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 40320 (JUATUBA), Zona 0172, Seção 0183, Qtd Votos Lula: 21 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0152, Qtd Votos Lula: 207 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0178, Qtd Votos Lula: 165 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 42757 (CARMÉSIA), Zona 0113, Seção 0042, Qtd Votos Lula: 82 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0276, Qtd Votos Lula: 115 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0291, Qtd Votos Lula: 75 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0171, Qtd Votos Lula: 68 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 50431 (PORTEIRINHA), Zona 0226, Seção 0219, Qtd Votos Lula: 28 - 100,00% do total.
- UF MG (MINAS GERAIS), Município 52213 (SÃO FRANCISCO), Zona 0252, Seção 0211, Qtd Votos Lula: 79 - 100,00% do total.
- UF MS (MATO GROSSO DO SUL), Município 91804 (DOIS IRMÃOS DO BURITI), Zona 0049, Seção 0068, Qtd Votos Lula: 206 - 100,00% do total.
- UF MT (MATO GROSSO), Município 90280 (CONFRESA), Zona 0028, Seção 0158, Qtd Votos Lula: 383 - 100,00% do total.
- UF MT (MATO GROSSO), Município 91979 (SANTA TEREZINHA), Zona 0016, Seção 0086, Qtd Votos Lula: 248 - 100,00% do total.
- UF MT (MATO GROSSO), Município 98655 (CAMPINÁPOLIS), Zona 0026, Seção 0189, Qtd Votos Lula: 265 - 100,00% do total.
- UF MT (MATO GROSSO), Município 98817 (PEIXOTO DE AZEVEDO), Zona 0033, Seção 0255, Qtd Votos Lula: 116 - 100,00% do total.
- UF MT (MATO GROSSO), Município 98850 (PORTO ALEGRE DO NORTE), Zona 0028, Seção 0191, Qtd Votos Lula: 55 - 100,00% do total.
- UF PA (PARÁ), Município 04146 (SANTA LUZIA DO PARÁ), Zona 0041, Seção 0198, Qtd Votos Lula: 82 - 100,00% do total.
- UF PA (PARÁ), Município 04359 (BREVES), Zona 0015, Seção 0289, Qtd Votos Lula: 29 - 100,00% do total.
- UF PA (PARÁ), Município 04723 (ANAPU), Zona 0080, Seção 0254, Qtd Votos Lula: 157 - 100,00% do total.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0092, Qtd Votos Lula: 29 - 100,00% do total.
- UF PA (PARÁ), Município 04936 (MOJU), Zona 0037, Seção 0120, Qtd Votos Lula: 49 - 100,00% do total.
- UF PA (PARÁ), Município 05053 (ORIXIMINÁ), Zona 0038, Seção 0260, Qtd Votos Lula: 111 - 100,00% do total.
- UF PA (PARÁ), Município 05436 (SÃO DOMINGOS DO CAPIM), Zona 0050, Seção 0181, Qtd Votos Lula: 103 - 100,00% do total.
- UF PA (PARÁ), Município 05452 (SÃO FÉLIX DO XINGU), Zona 0053, Seção 0218, Qtd Votos Lula: 194 - 100,00% do total.
- UF PI (PIAUÍ), Município 10243 (CORONEL JOSÉ DIAS), Zona 0013, Seção 0242, Qtd Votos Lula: 119 - 100,00% do total.
- UF PI (PIAUÍ), Município 10502 (SÃO BRAZ DO PIAUÍ), Zona 0095, Seção 0050, Qtd Votos Lula: 117 - 100,00% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0063, Qtd Votos Lula: 87 - 100,00% do total.
- UF PI (PIAUÍ), Município 11398 (OEIRAS), Zona 0005, Seção 0356, Qtd Votos Lula: 34 - 100,00% do total.
- UF PI (PIAUÍ), Município 11673 (PIRIPIRI), Zona 0011, Seção 0184, Qtd Votos Lula: 85 - 100,00% do total.
- UF PI (PIAUÍ), Município 12033 (SÃO JOSÉ DO PIAUÍ), Zona 0064, Seção 0073, Qtd Votos Lula: 98 - 100,00% do total.
- UF PI (PIAUÍ), Município 12190 (TERESINA), Zona 0097, Seção 0150, Qtd Votos Lula: 21 - 100,00% do total.
- UF PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0096, Qtd Votos Lula: 223 - 100,00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0063, Qtd Votos Lula: 176 - 100,00% do total.
- UF RR (RORAIMA), Município 03115 (NORMANDIA), Zona 0005, Seção 0627, Qtd Votos Lula: 55 - 100,00% do total.
- UF RS (RIO GRANDE DO SUL), Município 86568 (CHARRUA), Zona 0100, Seção 0126, Qtd Votos Lula: 302 - 100,00% do total.
- UF RS (RIO GRANDE DO SUL), Município 87858 (PASSO FUNDO), Zona 0128, Seção 0492, Qtd Votos Lula: 37 - 100,00% do total.
- UF SC (SANTA CATARINA), Município 80942 (SÃO CRISTÓVÃO DO SUL), Zona 0011, Seção 0166, Qtd Votos Lula: 28 - 100,00% do total.
- UF SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0304, Qtd Votos Lula: 17 - 100,00% do total.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0341, Qtd Votos Lula: 14 - 100,00% do total.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0185, Seção 0372, Qtd Votos Lula: 10 - 100,00% do total.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0217, Seção 0353, Qtd Votos Lula: 32 - 100,00% do total.
- UF SP (SÃO PAULO), Município 67890 (OSASCO), Zona 0315, Seção 0336, Qtd Votos Lula: 89 - 100,00% do total.
- UF TO (TOCANTINS), Município 93653 (FORMOSO DO ARAGUAIA), Zona 0015, Seção 0072, Qtd Votos Lula: 97 - 100,00% do total.
- UF ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127, Qtd Votos Lula: 32 - 100,00% do total.
- UF ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Qtd Votos Lula: 5 - 100,00% do total.

## Mudança de direção nos votos

Bolsonaro conseguiu recuperar no segundo turno uma parte considerável dos votos que foram para outros candidatos no primeiro turno.
A conta que foi feita para obter o número de votos virados foi a seguinte: (Votos de Lula - Votos de Bolsonaro no primeiro turno) - (Votos de Lula - Votos de Bolsonaro no segundo turno).
Se o resultado for negativo, é porque Lula virou mais votos. Se for positivo, é porque Bolsonaro virou mais fotos.

### Municípios que viraram votos para o Lula do primeiro para o segundo turno: 771.

#### Lista dos municípios que viraram (a partir de 10 mil votos)

Não há nenhum município em que Lula tenha virado mais do que 10 mil votos. 
O mais próximo disso foi Sobral (CE), com 8.712 votos virados.
O segundo município que mais virou votos para Lula foi Camocim, também no Ceará, porém com apenas 1.292 votos virados.

### Municípios que viraram votos para o Bolsonaro do primeiro para o segundo turno: 4.923.

#### Lista dos municípios que viraram (a partir de 10 mil votos)

- UF SP (SÃO PAULO), Municipio 71072 (SÃO PAULO), Votos ganhos por Bolsonaro: 171.482.
- UF MG (MINAS GERAIS), Municipio 41238 (BELO HORIZONTE), Votos ganhos por Bolsonaro: 69.690.
- UF RJ (RIO DE JANEIRO), Municipio 60011 (RIO DE JANEIRO), Votos ganhos por Bolsonaro: 67.758.
- UF PR (PARANÁ), Municipio 75353 (CURITIBA), Votos ganhos por Bolsonaro: 64.504.
- UF AM (AMAZONAS), Municipio 02550 (MANAUS), Votos ganhos por Bolsonaro: 62.605.
- UF DF (DISTRITO FEDERAL), Municipio 97012 (BRASÍLIA), Votos ganhos por Bolsonaro: 51.173.
- UF GO (GOIÁS), Municipio 93734 (GOIÂNIA), Votos ganhos por Bolsonaro: 40.976.
- UF SP (SÃO PAULO), Municipio 64777 (GUARULHOS), Votos ganhos por Bolsonaro: 34.123.
- UF RS (RIO GRANDE DO SUL), Municipio 88013 (PORTO ALEGRE), Votos ganhos por Bolsonaro: 31.397.
- UF MS (MATO GROSSO DO SUL), Municipio 90514 (CAMPO GRANDE), Votos ganhos por Bolsonaro: 24.731.
- UF AL (ALAGOAS), Municipio 27855 (MACEIÓ), Votos ganhos por Bolsonaro: 24.323.
- UF CE (CEARÁ), Municipio 13897 (FORTALEZA), Votos ganhos por Bolsonaro: 24.297.
- UF PE (PERNAMBUCO), Municipio 25313 (RECIFE), Votos ganhos por Bolsonaro: 22.285.
- UF SC (SANTA CATARINA), Municipio 81795 (JOINVILLE), Votos ganhos por Bolsonaro: 21.370.
- UF RJ (RIO DE JANEIRO), Municipio 58696 (NOVA IGUAÇU), Votos ganhos por Bolsonaro: 21.265.
- UF SP (SÃO PAULO), Municipio 62910 (CAMPINAS), Votos ganhos por Bolsonaro: 20.761.
- UF MG (MINAS GERAIS), Municipio 43710 (CONTAGEM), Votos ganhos por Bolsonaro: 19.958.
- UF RS (RIO GRANDE DO SUL), Municipio 85995 (CAXIAS DO SUL), Votos ganhos por Bolsonaro: 19.896.
- UF PR (PARANÁ), Municipio 76678 (LONDRINA), Votos ganhos por Bolsonaro: 19.872.
- UF RJ (RIO DE JANEIRO), Municipio 58335 (DUQUE DE CAXIAS), Votos ganhos por Bolsonaro: 19.371.
- UF MG (MINAS GERAIS), Municipio 54038 (UBERLÂNDIA), Votos ganhos por Bolsonaro: 18.691.
- UF SP (SÃO PAULO), Municipio 70998 (SÃO JOSÉ DOS CAMPOS), Votos ganhos por Bolsonaro: 17.668.
- UF SP (SÃO PAULO), Municipio 70750 (SÃO BERNARDO DO CAMPO), Votos ganhos por Bolsonaro: 17.220.
- UF PA (PARÁ), Municipio 04278 (BELÉM), Votos ganhos por Bolsonaro: 16.836.
- UF BA (BAHIA), Municipio 38490 (SALVADOR), Votos ganhos por Bolsonaro: 16.508.
- UF RJ (RIO DE JANEIRO), Municipio 58971 (SÃO GONÇALO), Votos ganhos por Bolsonaro: 16.301.
- UF SP (SÃO PAULO), Municipio 67890 (OSASCO), Votos ganhos por Bolsonaro: 16.281.
- UF RO (RONDÔNIA), Municipio 00035 (PORTO VELHO), Votos ganhos por Bolsonaro: 15.822.
- UF SP (SÃO PAULO), Municipio 71455 (SOROCABA), Votos ganhos por Bolsonaro: 15.662.
- UF SC (SANTA CATARINA), Municipio 80470 (BLUMENAU), Votos ganhos por Bolsonaro: 15.232.
- UF SP (SÃO PAULO), Municipio 70572 (SANTO ANDRÉ), Votos ganhos por Bolsonaro: 15.133.
- UF MT (MATO GROSSO), Municipio 90670 (CUIABÁ), Votos ganhos por Bolsonaro: 14.902.
- UF PR (PARANÁ), Municipio 77771 (PONTA GROSSA), Votos ganhos por Bolsonaro: 14.508.
- UF GO (GOIÁS), Municipio 92215 (ANÁPOLIS), Votos ganhos por Bolsonaro: 14.460.
- UF RJ (RIO DE JANEIRO), Municipio 59013 (SÃO JOÃO DE MERITI), Votos ganhos por Bolsonaro: 14.108.
- UF GO (GOIÁS), Municipio 92274 (APARECIDA DE GOIÂNIA), Votos ganhos por Bolsonaro: 13.248.
- UF PR (PARANÁ), Municipio 76910 (MARINGÁ), Votos ganhos por Bolsonaro: 12.979.
- UF RJ (RIO DE JANEIRO), Municipio 58041 (BELFORD ROXO), Votos ganhos por Bolsonaro: 12.606.
- UF PB (PARAÍBA), Municipio 20516 (JOÃO PESSOA), Votos ganhos por Bolsonaro: 12.347.
- UF MG (MINAS GERAIS), Municipio 41335 (BETIM), Votos ganhos por Bolsonaro: 12.265.
- UF PR (PARANÁ), Municipio 74934 (CASCAVEL), Votos ganhos por Bolsonaro: 11.969.
- UF SP (SÃO PAULO), Municipio 69698 (RIBEIRÃO PRETO), Votos ganhos por Bolsonaro: 11.908.
- UF PR (PARANÁ), Municipio 78859 (SÃO JOSÉ DOS PINHAIS), Votos ganhos por Bolsonaro: 11.876.
- UF SP (SÃO PAULO), Municipio 70971 (SÃO JOSÉ DO RIO PRETO), Votos ganhos por Bolsonaro: 11.805.
- UF RS (RIO GRANDE DO SUL), Municipio 85898 (CANOAS), Votos ganhos por Bolsonaro: 11.632.
- UF SP (SÃO PAULO), Municipio 67130 (MOGI DAS CRUZES), Votos ganhos por Bolsonaro: 11.525.
- UF AC (ACRE), Municipio 01392 (RIO BRANCO), Votos ganhos por Bolsonaro: 11.384.
- UF SC (SANTA CATARINA), Municipio 81051 (FLORIANÓPOLIS), Votos ganhos por Bolsonaro: 10.774.
- UF SP (SÃO PAULO), Municipio 66192 (JUNDIAÍ), Votos ganhos por Bolsonaro: 10.528.
- UF ES (ESPÍRITO SANTO), Municipio 56995 (SERRA), Votos ganhos por Bolsonaro: 10.215.
- UF SP (SÃO PAULO), Municipio 63134 (CARAPICUÍBA), Votos ganhos por Bolsonaro: 10.197.
- UF MA (MARANHÃO), Municipio 09210 (SÃO LUÍS), Votos ganhos por Bolsonaro: 10.137.
- UF SP (SÃO PAULO), Municipio 68756 (PIRACICABA), Votos ganhos por Bolsonaro: 10.029.

## Seções com Logs inconsistentes ou apuradas pelo sistema de apuração (SA)

As Seções Eleitorais listadas abaixo apresentam um dos dois tipos de problema a seguir:

- Log de Urna inconsistente
- Sistema de apuração

### Log de Urna Inconsistente

Toda urna eletrônica produz um arquivo de "log", que é um arquivo onde tudo o que acontece com a urna é registrado. Cada voto computado, cada tecla indevida pressionada, cada título de eleitor digitado, cada impressão digital conferida... TUDO é listado neste arquivo.

Através deste arquivo é possível inferir, por exemplo, quantos votos para presidente foram computados, pois cada voto para presidente que é computado gera uma linha neste arquivo. Basta contar as linhas e a quantidade de votos estará disponível. Desta forma, é possível comparar se o número de votos para presidente reportado no boletim de urna é o mesmo que foi contado no arquivo de log.

### Sistema de apuração

A urna eletrônica roda um sistema chamado "VOTA". Quando a votação ocorre de maneira normal, apenas a urna eletrônica é usada para receber votos. Caso ocorra alguma pane com o equipamento, sempre há disponível equipamentos reserva para a substituição durante o período de votação. Porém, problemas mais graves podem acontecer e impossibilitar a votação através da urna eletrônica. 

Neste caso, como último recurso, os eleitores tem acesso a cédulas de papel e uma urna convencional. A urna então é contabilizada manualmente ao final da votação, e os dados de votação são inseridos manualmente através do **Sistema de Apuração**, também conhecido pela abreviação **SA**.

Quando o SA é usado, o log da urna não vai produzir os detalhes de votação como os que o VOTA produz, tornando impossível portanto comparar o boletim de urna com o log.

### Primeiro turno

#### Quantidade de seções com Log Inconsistente ou SA: **215**.

- Votos para o Lula: 22.348.
- Votos para o Bolsonaro: 19.746.
- Diferença: 2.602 votos.

##### Agrupado por UF:

- AL (ALAGOAS), Votos Lula: 164, Votos Bolsonaro: 54.
- AM (AMAZONAS), Votos Lula: 401, Votos Bolsonaro: 320.
- BA (BAHIA), Votos Lula: 1.764, Votos Bolsonaro: 780.
- CE (CEARÁ), Votos Lula: 810, Votos Bolsonaro: 209.
- DF (DISTRITO FEDERAL), Votos Lula: 93, Votos Bolsonaro: 133.
- ES (ESPÍRITO SANTO), Votos Lula: 458, Votos Bolsonaro: 465.
- GO (GOIÁS), Votos Lula: 124, Votos Bolsonaro: 54.
- MA (MARANHÃO), Votos Lula: 945, Votos Bolsonaro: 404.
- MG (MINAS GERAIS), Votos Lula: 473, Votos Bolsonaro: 465.
- MS (MATO GROSSO DO SUL), Votos Lula: 93, Votos Bolsonaro: 231.
- MT (MATO GROSSO), Votos Lula: 81, Votos Bolsonaro: 161.
- PA (PARÁ), Votos Lula: 2.254, Votos Bolsonaro: 1.261.
- PE (PERNAMBUCO), Votos Lula: 363, Votos Bolsonaro: 71.
- PI (PIAUÍ), Votos Lula: 194, Votos Bolsonaro: 14.
- PR (PARANÁ), Votos Lula: 1.020, Votos Bolsonaro: 927.
- RJ (RIO DE JANEIRO), Votos Lula: 560, Votos Bolsonaro: 844.
- RN (RIO GRANDE DO NORTE), Votos Lula: 586, Votos Bolsonaro: 185.
- RS (RIO GRANDE DO SUL), Votos Lula: 733, Votos Bolsonaro: 603.
- SC (SANTA CATARINA), Votos Lula: 246, Votos Bolsonaro: 401.
- SE (SERGIPE), Votos Lula: 334, Votos Bolsonaro: 122.
- SP (SÃO PAULO), Votos Lula: 2.758, Votos Bolsonaro: 2.789.
- ZZ (EXTERIOR), Votos Lula: 7.894, Votos Bolsonaro: 9.253.

##### Seções:

- AL (ALAGOAS), Município 27219 (BRANQUINHA), Zona 0009, Seção 0005, Lula: 164, Bolsonaro: 54, Motivo: Log de Urna inconsistente.
- AM (AMAZONAS), Município 02011 (NOVO AIRÃO), Zona 0034, Seção 0027, Lula: 48, Bolsonaro: 61, Motivo: Sistema de apuração.
- AM (AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0116, Lula: 99, Bolsonaro: 111, Motivo: Sistema de apuração.
- AM (AMAZONAS), Município 02550 (MANAUS), Zona 0040, Seção 0454, Lula: 104, Bolsonaro: 134, Motivo: Sistema de apuração.
- AM (AMAZONAS), Município 02690 (PARINTINS), Zona 0004, Seção 0129, Lula: 150, Bolsonaro: 14, Motivo: Sistema de apuração.
- BA (BAHIA), Município 30007 (LUÍS EDUARDO MAGALHÃES), Zona 0205, Seção 0210, Lula: 26, Bolsonaro: 151, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 30872 (DIAS D'ÁVILA), Zona 0186, Seção 0094, Lula: 206, Bolsonaro: 64, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 34134 (CAMAÇARI), Zona 0171, Seção 0232, Lula: 206, Bolsonaro: 103, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 34495 (CATU), Zona 0129, Seção 0143, Lula: 112, Bolsonaro: 25, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 35696 (IGAPORÃ), Zona 0168, Seção 0142, Lula: 265, Bolsonaro: 8, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 36196 (ITANAGRA), Zona 0185, Seção 0122, Lula: 226, Bolsonaro: 41, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 37478 (MURITIBA), Zona 0131, Seção 0222, Lula: 106, Bolsonaro: 35, Motivo: Sistema de apuração.
- BA (BAHIA), Município 38075 (PORTO SEGURO), Zona 0121, Seção 0151, Lula: 119, Bolsonaro: 130, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 38075 (PORTO SEGURO), Zona 0121, Seção 0191, Lula: 102, Bolsonaro: 91, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 38490 (SALVADOR), Zona 0002, Seção 0534, Lula: 243, Bolsonaro: 52, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 39594 (VALENTE), Zona 0120, Seção 0024, Lula: 153, Bolsonaro: 80, Motivo: Log de Urna inconsistente.
- CE (CEARÁ), Município 14117 (IGUATU), Zona 0013, Seção 0042, Lula: 168, Bolsonaro: 47, Motivo: Log de Urna inconsistente.
- CE (CEARÁ), Município 14192 (IPUEIRAS), Zona 0040, Seção 0050, Lula: 100, Bolsonaro: 42, Motivo: Log de Urna inconsistente.
- CE (CEARÁ), Município 15270 (QUIXADÁ), Zona 0006, Seção 0206, Lula: 174, Bolsonaro: 40, Motivo: Sistema de apuração.
- CE (CEARÁ), Município 15377 (RUSSAS), Zona 0009, Seção 0301, Lula: 172, Bolsonaro: 54, Motivo: Log de Urna inconsistente.
- CE (CEARÁ), Município 15873 (AMONTADA), Zona 0089, Seção 0181, Lula: 196, Bolsonaro: 26, Motivo: Sistema de apuração.
- DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0017, Seção 0090, Lula: 93, Bolsonaro: 133, Motivo: Log de Urna inconsistente.
- ES (ESPÍRITO SANTO), Município 56189 (SOORETAMA), Zona 0041, Seção 0226, Lula: 102, Bolsonaro: 102, Motivo: Log de Urna inconsistente.
- ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0126, Lula: 101, Bolsonaro: 114, Motivo: Log de Urna inconsistente.
- ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0511, Lula: 122, Bolsonaro: 134, Motivo: Log de Urna inconsistente.
- ES (ESPÍRITO SANTO), Município 56995 (SERRA), Zona 0053, Seção 0375, Lula: 133, Bolsonaro: 115, Motivo: Log de Urna inconsistente.
- GO (GOIÁS), Município 92916 (CAMPESTRE DE GOIÁS), Zona 0020, Seção 0107, Lula: 124, Bolsonaro: 54, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07064 (APICUM-AÇU), Zona 0107, Seção 0157, Lula: 175, Bolsonaro: 84, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0166, Lula: 159, Bolsonaro: 11, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07617 (CHAPADINHA), Zona 0042, Seção 0098, Lula: 168, Bolsonaro: 103, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0021, Lula: 188, Bolsonaro: 20, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 09210 (SÃO LUÍS), Zona 0076, Seção 0048, Lula: 135, Bolsonaro: 120, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 09598 (PAULO RAMOS), Zona 0102, Seção 0012, Lula: 120, Bolsonaro: 66, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 44830 (ESPERA FELIZ), Zona 0303, Seção 0070, Lula: 64, Bolsonaro: 47, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 45136 (FELIXLÂNDIA), Zona 0100, Seção 0231, Lula: 139, Bolsonaro: 105, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0041, Lula: 100, Bolsonaro: 158, Motivo: Sistema de apuração.
- MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173, Lula: 16, Bolsonaro: , Motivo: Sistema de apuração.
- MG (MINAS GERAIS), Município 49735 (PEDRA AZUL), Zona 0213, Seção 0036, Lula: 30, Bolsonaro: 18, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 50350 (POÇOS DE CALDAS), Zona 0350, Seção 0147, Lula: 61, Bolsonaro: 92, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 51071 (RIO NOVO), Zona 0235, Seção 0052, Lula: 63, Bolsonaro: 45, Motivo: Log de Urna inconsistente.
- MS (MATO GROSSO DO SUL), Município 90735 (DOURADOS), Zona 0043, Seção 0027, Lula: 31, Bolsonaro: 116, Motivo: Sistema de apuração.
- MS (MATO GROSSO DO SUL), Município 98035 (COSTA RICA), Zona 0038, Seção 0008, Lula: 62, Bolsonaro: 115, Motivo: Log de Urna inconsistente.
- MT (MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0126, Lula: 81, Bolsonaro: 161, Motivo: Sistema de apuração.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0011, Lula: 74, Bolsonaro: 111, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0014, Lula: 106, Bolsonaro: 115, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0025, Lula: 83, Bolsonaro: 113, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0043, Lula: 192, Bolsonaro: 35, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0044, Lula: 106, Bolsonaro: 43, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0045, Lula: 196, Bolsonaro: 61, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0176, Lula: 126, Bolsonaro: 72, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0218, Lula: 128, Bolsonaro: 104, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04103 (MÃE DO RIO), Zona 0049, Seção 0224, Lula: 159, Bolsonaro: 81, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04154 (ANANINDEUA), Zona 0072, Seção 0012, Lula: 130, Bolsonaro: 146, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0064, Lula: 165, Bolsonaro: 33, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0080, Lula: 119, Bolsonaro: 60, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0099, Lula: 132, Bolsonaro: 57, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04340 (AURORA DO PARÁ), Zona 0049, Seção 0157, Lula: 165, Bolsonaro: 66, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0024, Lula: 238, Bolsonaro: 63, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 05177 (PORTO DE MOZ), Zona 0082, Seção 0062, Lula: 135, Bolsonaro: 101, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 23434 (BOM JARDIM), Zona 0033, Seção 0036, Lula: 164, Bolsonaro: 44, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 23493 (BREJINHO), Zona 0099, Seção 0039, Lula: 199, Bolsonaro: 27, Motivo: Sistema de apuração.
- PI (PIAUÍ), Município 10316 (BENEDITINOS), Zona 0047, Seção 0017, Lula: 194, Bolsonaro: 14, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 74039 (ADRIANÓPOLIS), Zona 0048, Seção 0013, Lula: 157, Bolsonaro: 77, Motivo: Sistema de apuração.
- PR (PARANÁ), Município 74071 (ALMIRANTE TAMANDARÉ), Zona 0171, Seção 0041, Lula: 146, Bolsonaro: 119, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 75370 (CURIÚVA), Zona 0119, Seção 0016, Lula: 141, Bolsonaro: 85, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 75833 (GUARAPUAVA), Zona 0043, Seção 0074, Lula: 127, Bolsonaro: 139, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 76678 (LONDRINA), Zona 0041, Seção 0318, Lula: 58, Bolsonaro: 152, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0100, Lula: 145, Bolsonaro: 152, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 78336 (SALGADO FILHO), Zona 0131, Seção 0045, Lula: 124, Bolsonaro: 94, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 84611 (SARANDI), Zona 0206, Seção 0069, Lula: 122, Bolsonaro: 109, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58122 (QUEIMADOS), Zona 0138, Seção 0146, Lula: 80, Bolsonaro: 120, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58394 (ITAGUAÍ), Zona 0105, Seção 0040, Lula: 71, Bolsonaro: 121, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0065, Seção 0041, Lula: 87, Bolsonaro: 140, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58971 (SÃO GONÇALO), Zona 0036, Seção 0007, Lula: 102, Bolsonaro: 137, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 59013 (SÃO JOÃO DE MERITI), Zona 0186, Seção 0147, Lula: 82, Bolsonaro: 111, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0167, Seção 0006, Lula: 138, Bolsonaro: 215, Motivo: Log de Urna inconsistente.
- RN (RIO GRANDE DO NORTE), Município 16977 (JAÇANÃ), Zona 0068, Seção 0066, Lula: 195, Bolsonaro: 55, Motivo: Log de Urna inconsistente.
- RN (RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0077, Lula: 45, Bolsonaro: 7, Motivo: Sistema de apuração.
- RN (RIO GRANDE DO NORTE), Município 18139 (RIACHO DE SANTANA), Zona 0065, Seção 0032, Lula: 203, Bolsonaro: 30, Motivo: Log de Urna inconsistente.
- RN (RIO GRANDE DO NORTE), Município 18953 (VERA CRUZ), Zona 0007, Seção 0123, Lula: 143, Bolsonaro: 93, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 87378 (MARAU), Zona 0062, Seção 0035, Lula: 11, Bolsonaro: 86, Motivo: Sistema de apuração.
- RS (RIO GRANDE DO SUL), Município 87912 (PELOTAS), Zona 0060, Seção 0322, Lula: 186, Bolsonaro: 69, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0002, Seção 0199, Lula: 129, Bolsonaro: 111, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 88013 (PORTO ALEGRE), Zona 0114, Seção 0336, Lula: 113, Bolsonaro: 99, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 89052 (SEBERI), Zona 0132, Seção 0063, Lula: 171, Bolsonaro: 124, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 89192 (SOLEDADE), Zona 0054, Seção 0009, Lula: 123, Bolsonaro: 114, Motivo: Log de Urna inconsistente.
- SC (SANTA CATARINA), Município 81434 (IMBITUBA), Zona 0073, Seção 0134, Lula: 91, Bolsonaro: 119, Motivo: Log de Urna inconsistente.
- SC (SANTA CATARINA), Município 83054 (SANTA CECÍLIA), Zona 0051, Seção 0059, Lula: 98, Bolsonaro: 142, Motivo: Log de Urna inconsistente.
- SC (SANTA CATARINA), Município 83275 (SÃO JOSÉ), Zona 0084, Seção 0022, Lula: 57, Bolsonaro: 140, Motivo: Log de Urna inconsistente.
- SE (SERGIPE), Município 31690 (LAGARTO), Zona 0012, Seção 0077, Lula: 153, Bolsonaro: 34, Motivo: Log de Urna inconsistente.
- SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0035, Lula: 181, Bolsonaro: 88, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 61557 (ARAÇATUBA), Zona 0299, Seção 0242, Lula: 87, Bolsonaro: 89, Motivo: Sistema de apuração.
- SP (SÃO PAULO), Município 61654 (ARARAS), Zona 0014, Seção 0206, Lula: 74, Bolsonaro: 199, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 63070 (CAPELA DO ALTO), Zona 0140, Seção 0002, Lula: 78, Bolsonaro: 156, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64270 (FRANCISCO MORATO), Zona 0367, Seção 0018, Lula: 116, Bolsonaro: 105, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64734 (GUARIBA), Zona 0197, Seção 0122, Lula: 71, Bolsonaro: 95, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0394, Seção 0152, Lula: 148, Bolsonaro: 139, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64939 (IBITINGA), Zona 0049, Seção 0212, Lula: 121, Bolsonaro: 127, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66230 (JUQUIÁ), Zona 0223, Seção 0023, Lula: 67, Bolsonaro: 121, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66397 (LIMEIRA), Zona 0066, Seção 0430, Lula: 40, Bolsonaro: 167, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66850 (MARTINÓPOLIS), Zona 0071, Seção 0023, Lula: 88, Bolsonaro: 90, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0365, Seção 0166, Lula: 126, Bolsonaro: 105, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70335 (SANTA FÉ DO SUL), Zona 0187, Seção 0058, Lula: 67, Bolsonaro: 190, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70718 (SANTOS), Zona 0118, Seção 0314, Lula: 121, Bolsonaro: 69, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0412, Seção 0195, Lula: 81, Bolsonaro: 175, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0020, Seção 0036, Lula: 142, Bolsonaro: 68, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0350, Seção 0033, Lula: 127, Bolsonaro: 101, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0351, Seção 0271, Lula: 109, Bolsonaro: 64, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0351, Seção 0545, Lula: 128, Bolsonaro: 75, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0353, Seção 0775, Lula: 197, Bolsonaro: 90, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0381, Seção 0441, Lula: 198, Bolsonaro: 84, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0529, Lula: 176, Bolsonaro: 81, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0417, Seção 0095, Lula: 115, Bolsonaro: 88, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0418, Seção 0395, Lula: 151, Bolsonaro: 70, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71811 (TATUÍ), Zona 0140, Seção 0216, Lula: 69, Bolsonaro: 124, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71838 (TAUBATÉ), Zona 0407, Seção 0205, Lula: 61, Bolsonaro: 117, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494, Lula: 1, Bolsonaro: 14, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001, Lula: 21, Bolsonaro: 18, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003, Lula: 12, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495, Lula: 19, Bolsonaro: 7, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735, Lula: 30, Bolsonaro: 8, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29386 (BERLIM), Zona 0001, Seção 3057, Lula: 314, Bolsonaro: 54, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028, Lula: 23, Bolsonaro: 21, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0051, Lula: 12, Bolsonaro: 53, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0053, Lula: 50, Bolsonaro: 180, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 1041, Lula: 39, Bolsonaro: 185, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0071, Lula: 111, Bolsonaro: 148, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0072, Lula: 129, Bolsonaro: 165, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096, Lula: 2, Bolsonaro: 35, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380, Lula: 18, Bolsonaro: 37, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1909, Lula: 137, Bolsonaro: 178, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1912, Lula: 131, Bolsonaro: 112, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1740, Lula: 52, Bolsonaro: 240, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1750, Lula: 32, Bolsonaro: 262, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703, Lula: 13, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127, Lula: 28, Bolsonaro: 1, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29807 (HOUSTON), Zona 0001, Seção 0940, Lula: 45, Bolsonaro: 118, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146, Lula: 2, Bolsonaro: 14, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390, Lula: 9, Bolsonaro: 27, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150, Lula: 16, Bolsonaro: 7, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29947 (LIMA), Zona 0001, Seção 0154, Lula: 115, Bolsonaro: 180, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0540, Lula: 195, Bolsonaro: 129, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0541, Lula: 217, Bolsonaro: 106, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0591, Lula: 344, Bolsonaro: 91, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1355, Lula: 142, Bolsonaro: 63, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1632, Lula: 271, Bolsonaro: 106, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1637, Lula: 133, Bolsonaro: 81, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1640, Lula: 95, Bolsonaro: 107, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1671, Lula: 249, Bolsonaro: 137, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29998 (LUANDA), Zona 0001, Seção 0199, Lula: 124, Bolsonaro: 93, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0024, Lula: 145, Bolsonaro: 102, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0026, Lula: 52, Bolsonaro: 34, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 1029, Lula: 7, Bolsonaro: 8, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30082 (MANILA), Zona 0001, Seção 0990, Lula: 20, Bolsonaro: 35, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0230, Lula: 49, Bolsonaro: 233, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0504, Lula: 34, Bolsonaro: 172, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1125, Lula: 45, Bolsonaro: 164, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1132, Lula: 29, Bolsonaro: 129, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1164, Lula: 42, Bolsonaro: 205, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 3040, Lula: 33, Bolsonaro: 154, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0394, Lula: 135, Bolsonaro: 103, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0399, Lula: 98, Bolsonaro: 83, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0426, Lula: 42, Bolsonaro: 33, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0432, Lula: 122, Bolsonaro: 106, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0442, Lula: 109, Bolsonaro: 89, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647, Lula: 45, Bolsonaro: 28, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340, Lula: 9, Bolsonaro: 2, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0164, Lula: 36, Bolsonaro: 198, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0165, Lula: 40, Bolsonaro: 193, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0168, Lula: 47, Bolsonaro: 290, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0247, Lula: 37, Bolsonaro: 170, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0248, Lula: 39, Bolsonaro: 218, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30228 (NOVA YORK), Zona 0001, Seção 0297, Lula: 79, Bolsonaro: 69, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30252 (OTTAWA), Zona 0001, Seção 0767, Lula: 144, Bolsonaro: 83, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0758, Lula: 190, Bolsonaro: 63, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0768, Lula: 197, Bolsonaro: 37, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0796, Lula: 342, Bolsonaro: 34, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353, Lula: 2, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1858, Lula: 110, Bolsonaro: 110, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1886, Lula: 232, Bolsonaro: 104, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384, Lula: 10, Bolsonaro: 7, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764, Lula: 22, Bolsonaro: 6, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30562 (SYDNEY), Zona 0001, Seção 1375, Lula: 54, Bolsonaro: 14, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182, Lula: 7, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30619 (TEL AVIV), Zona 0001, Seção 0682, Lula: 40, Bolsonaro: 62, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1765, Lula: 54, Bolsonaro: 132, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1787, Lula: 71, Bolsonaro: 154, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1818, Lula: 34, Bolsonaro: 224, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1820, Lula: 39, Bolsonaro: 215, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1821, Lula: 36, Bolsonaro: 221, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1031, Lula: 159, Bolsonaro: 163, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1488, Lula: 139, Bolsonaro: 156, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444, Lula: 8, Bolsonaro: 6, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1964, Lula: 179, Bolsonaro: 85, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1966, Lula: 195, Bolsonaro: 70, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1967, Lula: 239, Bolsonaro: 74, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30783 (WASHINGTON), Zona 0001, Seção 0458, Lula: 107, Bolsonaro: 110, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1690, Lula: 117, Bolsonaro: 22, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524, Lula: 4, Bolsonaro: 33, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558, Lula: 6, Bolsonaro: 2, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004, Lula: 25, Bolsonaro: 14, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3268, Lula: 247, Bolsonaro: 118, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0545, Lula: 48, Bolsonaro: 123, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0551, Lula: 51, Bolsonaro: 99, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0590, Lula: 106, Bolsonaro: 190, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0643, Lula: 66, Bolsonaro: 180, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0662, Lula: 49, Bolsonaro: 188, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0719, Lula: 51, Bolsonaro: 172, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712, Lula: 11, Bolsonaro: 25, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490, Lula: 13, Bolsonaro: 7, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123, Lula: 32, Bolsonaro: 90, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Lula: 4, Bolsonaro: , Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228, Lula: 8, Bolsonaro: 15, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259, Lula: 11, Bolsonaro: 2, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327, Lula: 10, Bolsonaro: 22, Motivo: Sistema de apuração.

### Segundo turno

#### Quantidade de seções com Log Inconsistente ou SA: **94**.

- Votos para o Lula: 8.563.
- Votos para o Bolsonaro: 8.524.
- Diferença: 39 votos.

##### Agrupado por UF:

- AM (AMAZONAS), Votos Lula: 110, Votos Bolsonaro: 113.
- BA (BAHIA), Votos Lula: 597, Votos Bolsonaro: 281.
- CE (CEARÁ), Votos Lula: 205, Votos Bolsonaro: 31.
- DF (DISTRITO FEDERAL), Votos Lula: 266, Votos Bolsonaro: 327.
- ES (ESPÍRITO SANTO), Votos Lula: 239, Votos Bolsonaro: 333.
- GO (GOIÁS), Votos Lula: 102, Votos Bolsonaro: 199.
- MA (MARANHÃO), Votos Lula: 610, Votos Bolsonaro: 250.
- MG (MINAS GERAIS), Votos Lula: 264, Votos Bolsonaro: 270.
- PA (PARÁ), Votos Lula: 472, Votos Bolsonaro: 505.
- PE (PERNAMBUCO), Votos Lula: 801, Votos Bolsonaro: 273.
- PR (PARANÁ), Votos Lula: 382, Votos Bolsonaro: 741.
- RJ (RIO DE JANEIRO), Votos Lula: 588, Votos Bolsonaro: 820.
- RS (RIO GRANDE DO SUL), Votos Lula: 417, Votos Bolsonaro: 343.
- SC (SANTA CATARINA), Votos Lula: 138, Votos Bolsonaro: 446.
- SE (SERGIPE), Votos Lula: 203, Votos Bolsonaro: 31.
- SP (SÃO PAULO), Votos Lula: 2.077, Votos Bolsonaro: 2.473.
- TO (TOCANTINS), Votos Lula: 78, Votos Bolsonaro: 184.
- ZZ (EXTERIOR), Votos Lula: 1.014, Votos Bolsonaro: 904.

##### Seções:

- AM (AMAZONAS), Município 02550 (MANAUS), Zona 0068, Seção 0512, Lula: 110, Bolsonaro: 113, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 34134 (CAMAÇARI), Zona 0171, Seção 0232, Lula: 216, Bolsonaro: 119, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 37877 (PIATÃ), Zona 0105, Seção 0083, Lula: 78, Bolsonaro: 15, Motivo: Sistema de apuração.
- BA (BAHIA), Município 38652 (SANTANA), Zona 0099, Seção 0046, Lula: 154, Bolsonaro: 58, Motivo: Log de Urna inconsistente.
- BA (BAHIA), Município 39535 (URUÇUCA), Zona 0198, Seção 0043, Lula: 149, Bolsonaro: 89, Motivo: Log de Urna inconsistente.
- CE (CEARÁ), Município 13404 (BARROQUINHA), Zona 0108, Seção 0042, Lula: 205, Bolsonaro: 31, Motivo: Log de Urna inconsistente.
- DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0006, Seção 0044, Lula: 113, Bolsonaro: 141, Motivo: Log de Urna inconsistente.
- DF (DISTRITO FEDERAL), Município 97012 (BRASÍLIA), Zona 0009, Seção 0011, Lula: 153, Bolsonaro: 186, Motivo: Sistema de apuração.
- ES (ESPÍRITO SANTO), Município 56251 (CARIACICA), Zona 0054, Seção 0618, Lula: 125, Bolsonaro: 146, Motivo: Log de Urna inconsistente.
- ES (ESPÍRITO SANTO), Município 56634 (LINHARES), Zona 0025, Seção 0089, Lula: 114, Bolsonaro: 187, Motivo: Log de Urna inconsistente.
- GO (GOIÁS), Município 92886 (COCALZINHO DE GOIÁS), Zona 0026, Seção 0131, Lula: 102, Bolsonaro: 199, Motivo: Sistema de apuração.
- MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0149, Lula: 92, Bolsonaro: 31, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07277 (BALSAS), Zona 0022, Seção 0122, Lula: 203, Bolsonaro: 88, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07650 (COELHO NETO), Zona 0028, Seção 0030, Lula: 201, Bolsonaro: 59, Motivo: Log de Urna inconsistente.
- MA (MARANHÃO), Município 07668 (GOVERNADOR NUNES FREIRE), Zona 0101, Seção 0098, Lula: 114, Bolsonaro: 72, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 40720 (SÃO JOÃO DO MANHUAÇU), Zona 0167, Seção 0179, Lula: 151, Bolsonaro: 182, Motivo: Log de Urna inconsistente.
- MG (MINAS GERAIS), Município 51152 (RIO POMBA), Zona 0239, Seção 0022, Lula: 113, Bolsonaro: 88, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04154 (ANANINDEUA), Zona 0043, Seção 0914, Lula: 165, Bolsonaro: 135, Motivo: Log de Urna inconsistente.
- PA (PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0017, Lula: 76, Bolsonaro: 96, Motivo: Sistema de apuração.
- PA (PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0018, Lula: 75, Bolsonaro: 103, Motivo: Sistema de apuração.
- PA (PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0019, Lula: 68, Bolsonaro: 100, Motivo: Sistema de apuração.
- PA (PARÁ), Município 04898 (MELGAÇO), Zona 0099, Seção 0055, Lula: 88, Bolsonaro: 71, Motivo: Sistema de apuração.
- PE (PERNAMBUCO), Município 23639 (CAETÉS), Zona 0130, Seção 0006, Lula: 197, Bolsonaro: 18, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 23973 (CUMARU), Zona 0091, Seção 0099, Lula: 81, Bolsonaro: 33, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 24350 (IGARASSU), Zona 0085, Seção 0044, Lula: 198, Bolsonaro: 80, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 25216 (PETROLINA), Zona 0083, Seção 0416, Lula: 161, Bolsonaro: 107, Motivo: Log de Urna inconsistente.
- PE (PERNAMBUCO), Município 26212 (VERDEJANTE), Zona 0075, Seção 0135, Lula: 164, Bolsonaro: 35, Motivo: Sistema de apuração.
- PR (PARANÁ), Município 76678 (LONDRINA), Zona 0146, Seção 0273, Lula: 90, Bolsonaro: 175, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 78050 (REALEZA), Zona 0130, Seção 0041, Lula: 126, Bolsonaro: 147, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 78859 (SÃO JOSÉ DOS PINHAIS), Zona 0199, Seção 0186, Lula: 103, Bolsonaro: 229, Motivo: Log de Urna inconsistente.
- PR (PARANÁ), Município 84670 (SANTA TEREZINHA DE ITAIPU), Zona 0147, Seção 0417, Lula: 63, Bolsonaro: 190, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0128, Seção 0136, Lula: 90, Bolsonaro: 224, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58335 (DUQUE DE CAXIAS), Zona 0200, Seção 0154, Lula: 127, Bolsonaro: 164, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0029, Seção 0162, Lula: 118, Bolsonaro: 149, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 58777 (PETRÓPOLIS), Zona 0065, Seção 0233, Lula: 58, Bolsonaro: 146, Motivo: Log de Urna inconsistente.
- RJ (RIO DE JANEIRO), Município 60011 (RIO DE JANEIRO), Zona 0007, Seção 0479, Lula: 195, Bolsonaro: 137, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 85871 (CANGUÇU), Zona 0014, Seção 0106, Lula: 106, Bolsonaro: 138, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 87912 (PELOTAS), Zona 0060, Seção 0322, Lula: 197, Bolsonaro: 82, Motivo: Log de Urna inconsistente.
- RS (RIO GRANDE DO SUL), Município 89192 (SOLEDADE), Zona 0054, Seção 0013, Lula: 114, Bolsonaro: 123, Motivo: Sistema de apuração.
- SC (SANTA CATARINA), Município 80470 (BLUMENAU), Zona 0003, Seção 0463, Lula: 75, Bolsonaro: 196, Motivo: Log de Urna inconsistente.
- SC (SANTA CATARINA), Município 83011 (SALETE), Zona 0046, Seção 0022, Lula: 63, Bolsonaro: 250, Motivo: Sistema de apuração.
- SE (SERGIPE), Município 31690 (LAGARTO), Zona 0012, Seção 0092, Lula: 203, Bolsonaro: 31, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0165, Lula: 95, Bolsonaro: 193, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0394, Seção 0204, Lula: 148, Bolsonaro: 131, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66192 (JUNDIAÍ), Zona 0065, Seção 0451, Lula: 85, Bolsonaro: 189, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 66192 (JUNDIAÍ), Zona 0424, Seção 0001, Lula: 76, Bolsonaro: 100, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 67938 (OSVALDO CRUZ), Zona 0163, Seção 0023, Lula: 69, Bolsonaro: 152, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 67954 (OURINHOS), Zona 0082, Seção 0084, Lula: 82, Bolsonaro: 142, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 68535 (PERUÍBE), Zona 0295, Seção 0177, Lula: 73, Bolsonaro: 114, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 69876 (ROSEIRA), Zona 0190, Seção 0100, Lula: 108, Bolsonaro: 160, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70572 (SANTO ANDRÉ), Zona 0156, Seção 0309, Lula: 102, Bolsonaro: 122, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0127, Seção 0007, Lula: 67, Bolsonaro: 98, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0127, Seção 0546, Lula: 88, Bolsonaro: 212, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 70998 (SÃO JOSÉ DOS CAMPOS), Zona 0282, Seção 0126, Lula: 108, Bolsonaro: 152, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0002, Seção 0310, Lula: 126, Bolsonaro: 125, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0258, Seção 0504, Lula: 141, Bolsonaro: 132, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0373, Seção 0734, Lula: 215, Bolsonaro: 86, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0374, Seção 0478, Lula: 173, Bolsonaro: 93, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0418, Seção 0261, Lula: 164, Bolsonaro: 149, Motivo: Log de Urna inconsistente.
- SP (SÃO PAULO), Município 71153 (SÃO SEBASTIÃO), Zona 0132, Seção 0129, Lula: 157, Bolsonaro: 123, Motivo: Log de Urna inconsistente.
- TO (TOCANTINS), Município 73440 (PALMAS), Zona 0029, Seção 0229, Lula: 78, Bolsonaro: 184, Motivo: Log de Urna inconsistente.
- ZZ (EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494, Lula: 2, Bolsonaro: 13, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001, Lula: 26, Bolsonaro: 15, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003, Lula: 14, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495, Lula: 20, Bolsonaro: 8, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735, Lula: 36, Bolsonaro: 12, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028, Lula: 20, Bolsonaro: 25, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096, Lula: 3, Bolsonaro: 32, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380, Lula: 22, Bolsonaro: 37, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703, Lula: 13, Bolsonaro: 6, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127, Lula: 32, Bolsonaro: , Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146, Lula: 1, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390, Lula: 10, Bolsonaro: 29, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150, Lula: 21, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647, Lula: 46, Bolsonaro: 23, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340, Lula: 11, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353, Lula: 2, Bolsonaro: 3, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384, Lula: 13, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764, Lula: 24, Bolsonaro: 7, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182, Lula: 8, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1231, Lula: 146, Bolsonaro: 190, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444, Lula: 8, Bolsonaro: 4, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1693, Lula: 36, Bolsonaro: 32, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524, Lula: 4, Bolsonaro: 31, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 30902 (HARTFORD), Zona 0001, Seção 3134, Lula: 135, Bolsonaro: 103, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558, Lula: 6, Bolsonaro: 2, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004, Lula: 26, Bolsonaro: 15, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3030, Lula: 262, Bolsonaro: 197, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712, Lula: 14, Bolsonaro: 32, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490, Lula: 15, Bolsonaro: 9, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Lula: 5, Bolsonaro: , Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228, Lula: 9, Bolsonaro: 14, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259, Lula: 13, Bolsonaro: 1, Motivo: Sistema de apuração.
- ZZ (EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327, Lula: 11, Bolsonaro: 25, Motivo: Sistema de apuração.

## Votos para Bolsonaro e Lula, por Modelo de Urna

Existem 6 modelos diferentes de Urnas Eletrônicas em uso no Brasil. Eles são identificados pelo ano de fabricação. São eles: 2009, 2010, 2011, 2013, 2015 e 2020.

O Log da Urna escreve linhas identificando o modelo da urna. Na maioria das seções, a mesma urna permanece em uso do início ao fim do período de votação. Porém, pode acontecer da urna precisar ser substituída, e a urna substituta ser de um modelo diferente da anterior.

É impossível, no entanto, saber o modelo exato da urna que computou cada voto para cada candidato, pois por motivos óbvios, a urna fornece meios para identificar cada voto de forma que possa ser associado a outros dados (como o horário do voto, por exemplo).

A fim obter uma contagem mais aproximada da quantidade de votos que cada candidato recebeu em casa modelo de urna, o TSE Parser grava o modelo de urna ao processar os logs. Cada voto que aparece no log é salvo em uma tabela juntamente com a versão da urna. Então se uma seção teve, digamos, 20 votos em uma UE2009, e depois mais 40 votos em uma UE2015, podemos assumir que 33% dos votos foram em uma urna modelo 2009 e os outros 66% em uma urna 2015. E assim, se o Lula teve, digamos, 30 votos nesta seção, e Bolsonaro outros 20, podemos calcular que Lula teve 10 votos na urna 2009 e 20 votos na urna 2015, enquanto Bolsonaro teve 7 votos na urna 2009 e 13 votos na urna 2015.

### Primeiro Turno

```mermaid
    pie showData;
        title Modelos de Urna Primeiro Turno;
        "2009" : 10248578;
        "2010" : 20846453;
        "2011" : 6042971;
        "2013" : 5512995;
        "2015" : 19261091;
        "2020" : 46342552;
```


| Modelo Urna | Votos Lula | Votos Bolsonaro | Soma Votos | % Lula | % Bolsonaro |
|-------------|------------|-----------------|------------|--------|-------------|
| 2009        | 5.455.272  | 4.795.353       | 10.248.578 | 53.22  | 46.79       |
| 2010        | 11.631.740 | 9.219.848       | 20.846.453 | 55.79  | 44.22       |
| 2011        | 3.366.107  | 2.678.565       | 6.042.971  | 55.70  | 44.32       |
| 2013        | 2.929.781  | 2.585.417       | 5.512.995  | 53.14  | 46.89       |
| 2015        | 10.324.227 | 8.942.460       | 19.261.091 | 53.60  | 46.42       |
| 2020        | 23.518.298 | 22.825.140      | 46.342.552 | 50.74  | 49.25       |


### Segundo Turno

```mermaid
    pie showData;
        title Modelos de Urna Segundo Turno;
        "2009" : 11607216;
        "2010" : 22471535;
        "2011" : 6395081;
        "2013" : 5869620;
        "2015" : 20877873;
        "2020" : 51296097;
```

| Modelo Urna | Votos Lula | Votos Bolsonaro | Soma Votos | % Lula    | % Bolsonaro |
|-------------|------------|-----------------|------------|-----------|-------------|
| 2009        | 5.927.237  | 5.681.919       | 11.607.216 | 51.06     | 48.95       |
| 2010        | 12.080.656 | 10.395.321      | 22.471.535 | 53.75     | 46.25       |
| 2011        | 3.442.808  | 2.953.702       | 6.395.081  | 53.83     | 46.18       |
| 2013        | 3.007.820  | 2.864.107       | 5.869.620  | 51.24     | 48.79       |
| 2015        | 10.768.537 | 10.114.880      | 20.877.873 | 51.57     | 48.44       |
| 2020        | 25.109.386 | 26.187.588      | 51.296.097 | 48.94     | 51.05       |





