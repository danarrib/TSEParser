# Relatório das Eleições de 2020

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

### Seções eleitorais com Códigos de Identificação de Urna Eletrônica repetidos

Existem 6 Códigos de Identificação de Urna Eletrônica que se repetem para duas ou mais seções eleitorais. Cada urna não deveria ter seu próprio número de série único?

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

- UF AM (AMAZONAS), Qtd Votos Lula: 4794.
- UF BA (BAHIA), Qtd Votos Lula: 2146.
- UF CE (CEARÁ), Qtd Votos Lula: 1031.
- UF MA (MARANHÃO), Qtd Votos Lula: 5354.
- UF MG (MINAS GERAIS), Qtd Votos Lula: 589.
- UF MS (MATO GROSSO DO SUL), Qtd Votos Lula: 212.
- UF MT (MATO GROSSO), Qtd Votos Lula: 1157.
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
- UF AM (AMAZONAS), Qtd Votos Lula: 4221.
- UF BA (BAHIA), Qtd Votos Lula: 1553.
- UF CE (CEARÁ), Qtd Votos Lula: 718.
- UF MA (MARANHÃO), Qtd Votos Lula: 5164.
- UF MG (MINAS GERAIS), Qtd Votos Lula: 840.
- UF MS (MATO GROSSO DO SUL), Qtd Votos Lula: 206.
- UF MT (MATO GROSSO), Qtd Votos Lula: 1067.
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

- UF PA (PARÁ), Município 04383 (NOVO PROGRESSO), Zona 0091, Seção 0073, Qtd Votos Bolsonaro: 58 - 100.00% do total.
  
#### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: 161.

- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0025, Qtd Votos Lula: 142 - 96.60% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0028, Qtd Votos Lula: 126 - 99.21% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0029, Qtd Votos Lula: 133 - 91.10% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0030, Qtd Votos Lula: 129 - 98.47% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0037, Qtd Votos Lula: 103 - 99.04% do total.
- UF AM (AMAZONAS), Município 02119 (BARREIRINHA), Zona 0026, Seção 0062, Qtd Votos Lula: 308 - 96.55% do total.
- UF AM (AMAZONAS), Município 02216 (CARAUARI), Zona 0021, Seção 0096, Qtd Votos Lula: 89 - 100.00% do total.
- UF AM (AMAZONAS), Município 02275 (CODAJÁS), Zona 0007, Seção 0046, Qtd Votos Lula: 43 - 95.56% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0022, Qtd Votos Lula: 165 - 99.40% do total.
- UF AM (AMAZONAS), Município 02410 (ITACOATIARA), Zona 0003, Seção 0158, Qtd Votos Lula: 39 - 100.00% do total.
- UF AM (AMAZONAS), Município 02453 (JAPURÁ), Zona 0048, Seção 0012, Qtd Votos Lula: 171 - 99.42% do total.
- UF AM (AMAZONAS), Município 02690 (PARINTINS), Zona 0004, Seção 0130, Qtd Votos Lula: 227 - 99.56% do total.
- UF AM (AMAZONAS), Município 02690 (PARINTINS), Zona 0004, Seção 0232, Qtd Votos Lula: 119 - 97.54% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0009, Qtd Votos Lula: 331 - 98.81% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0045, Qtd Votos Lula: 191 - 96.46% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0057, Qtd Votos Lula: 197 - 96.57% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0080, Qtd Votos Lula: 326 - 99.69% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0086, Qtd Votos Lula: 357 - 99.17% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0013, Qtd Votos Lula: 241 - 99.59% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0048, Qtd Votos Lula: 167 - 99.40% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0059, Qtd Votos Lula: 136 - 99.27% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0065, Qtd Votos Lula: 184 - 98.92% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0087, Qtd Votos Lula: 139 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0092, Qtd Votos Lula: 189 - 99.47% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0061, Qtd Votos Lula: 256 - 94.12% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0098, Qtd Votos Lula: 142 - 95.95% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0100, Qtd Votos Lula: 144 - 96.00% do total.
- UF BA (BAHIA), Município 30759 (BARRO ALTO), Zona 0174, Seção 0050, Qtd Votos Lula: 65 - 100.00% do total.
- UF BA (BAHIA), Município 33090 (ÉRICO CARDOSO), Zona 0111, Seção 0250, Qtd Votos Lula: 47 - 97.92% do total.
- UF BA (BAHIA), Município 33260 (ANDORINHA), Zona 0045, Seção 0250, Qtd Votos Lula: 213 - 96.82% do total.
- UF BA (BAHIA), Município 33596 (BARRA DO MENDES), Zona 0176, Seção 0151, Qtd Votos Lula: 69 - 100.00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0254, Qtd Votos Lula: 183 - 97.34% do total.
- UF BA (BAHIA), Município 33898 (BROTAS DE MACAÚBAS), Zona 0094, Seção 0028, Qtd Votos Lula: 28 - 82.35% do total.
- UF BA (BAHIA), Município 34975 (CURAÇÁ), Zona 0085, Seção 0079, Qtd Votos Lula: 108 - 95.58% do total.
- UF BA (BAHIA), Município 35157 (FEIRA DE SANTANA), Zona 0155, Seção 0800, Qtd Votos Lula: 21 - 100.00% do total.
- UF BA (BAHIA), Município 35378 (IAÇU), Zona 0193, Seção 0116, Qtd Votos Lula: 42 - 100.00% do total.
- UF BA (BAHIA), Município 35637 (IBITITÁ), Zona 0104, Seção 0241, Qtd Votos Lula: 48 - 96.00% do total.
- UF BA (BAHIA), Município 36595 (JANDAÍRA), Zona 0049, Seção 0155, Qtd Votos Lula: 83 - 97.65% do total.
- UF BA (BAHIA), Município 36692 (JUAZEIRO), Zona 0047, Seção 0276, Qtd Votos Lula: 47 - 95.92% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0053, Qtd Votos Lula: 107 - 98.17% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0057, Qtd Votos Lula: 70 - 100.00% do total.
- UF BA (BAHIA), Município 37591 (NOVA SOURE), Zona 0079, Seção 0152, Qtd Votos Lula: 123 - 96.85% do total.
- UF BA (BAHIA), Município 37737 (PARAMIRIM), Zona 0111, Seção 0208, Qtd Votos Lula: 57 - 98.28% do total.
- UF BA (BAHIA), Município 37770 (PARIPIRANGA), Zona 0052, Seção 0182, Qtd Votos Lula: 51 - 100.00% do total.
- UF BA (BAHIA), Município 37877 (PIATÃ), Zona 0105, Seção 0062, Qtd Votos Lula: 31 - 86.11% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0068, Qtd Votos Lula: 91 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0076, Qtd Votos Lula: 64 - 96.97% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0100, Qtd Votos Lula: 91 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0111, Qtd Votos Lula: 101 - 96.19% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0132, Qtd Votos Lula: 172 - 100.00% do total.
- UF BA (BAHIA), Município 38130 (PRESIDENTE DUTRA), Zona 0095, Seção 0516, Qtd Votos Lula: 69 - 98.57% do total.
- UF BA (BAHIA), Município 39292 (TEOFILÂNDIA), Zona 0123, Seção 0046, Qtd Votos Lula: 47 - 100.00% do total.
- UF BA (BAHIA), Município 39896 (SÃO GABRIEL), Zona 0095, Seção 0321, Qtd Votos Lula: 49 - 94.23% do total.
- UF BA (BAHIA), Município 39942 (NOVO HORIZONTE), Zona 0088, Seção 0304, Qtd Votos Lula: 69 - 100.00% do total.
- UF CE (CEARÁ), Município 13102 (QUITERIANÓPOLIS), Zona 0099, Seção 0141, Qtd Votos Lula: 113 - 97.41% do total.
- UF CE (CEARÁ), Município 13277 (ARATUBA), Zona 0105, Seção 0175, Qtd Votos Lula: 89 - 96.74% do total.
- UF CE (CEARÁ), Município 13536 (CAMPOS SALES), Zona 0038, Seção 0146, Qtd Votos Lula: 48 - 90.57% do total.
- UF CE (CEARÁ), Município 13552 (CANINDÉ), Zona 0033, Seção 0413, Qtd Votos Lula: 117 - 97.50% do total.
- UF CE (CEARÁ), Município 13951 (GRANJA), Zona 0025, Seção 0208, Qtd Votos Lula: 71 - 98.61% do total.
- UF CE (CEARÁ), Município 14290 (ITAPIPOCA), Zona 0017, Seção 0437, Qtd Votos Lula: 155 - 99.36% do total.
- UF CE (CEARÁ), Município 14630 (MAURITI), Zona 0076, Seção 0146, Qtd Votos Lula: 56 - 86.15% do total.
- UF CE (CEARÁ), Município 15210 (PORANGA), Zona 0040, Seção 0163, Qtd Votos Lula: 67 - 100.00% do total.
- UF CE (CEARÁ), Município 15431 (SANTANA DO CARIRI), Zona 0053, Seção 0050, Qtd Votos Lula: 151 - 98.05% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0101, Qtd Votos Lula: 80 - 95.24% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0102, Qtd Votos Lula: 84 - 97.67% do total.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0174, Qtd Votos Lula: 71 - 98.61% do total.
- UF MA (MARANHÃO), Município 07110 (AMARANTE DO MARANHÃO), Zona 0099, Seção 0127, Qtd Votos Lula: 77 - 100.00% do total.
- UF MA (MARANHÃO), Município 07250 (BACURI), Zona 0107, Seção 0038, Qtd Votos Lula: 109 - 99.09% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0175, Qtd Votos Lula: 91 - 100.00% do total.
- UF MA (MARANHÃO), Município 07390 (BREJO), Zona 0024, Seção 0253, Qtd Votos Lula: 57 - 93.44% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0185, Qtd Votos Lula: 274 - 100.00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0186, Qtd Votos Lula: 278 - 100.00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0205, Qtd Votos Lula: 300 - 100.00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0149, Qtd Votos Lula: 31 - 100.00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0175, Qtd Votos Lula: 55 - 100.00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0193, Qtd Votos Lula: 20 - 100.00% do total.
- UF MA (MARANHÃO), Município 07773 (ESPERANTINÓPOLIS), Zona 0061, Seção 0140, Qtd Votos Lula: 71 - 98.61% do total.
- UF MA (MARANHÃO), Município 07927 (MARANHÃOZINHO), Zona 0101, Seção 0078, Qtd Votos Lula: 303 - 99.67% do total.
- UF MA (MARANHÃO), Município 07951 (GUIMARÃES), Zona 0030, Seção 0061, Qtd Votos Lula: 87 - 100.00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0051, Qtd Votos Lula: 134 - 98.53% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0208, Qtd Votos Lula: 50 - 98.04% do total.
- UF MA (MARANHÃO), Município 08044 (PAULINO NEVES), Zona 0040, Seção 0262, Qtd Votos Lula: 108 - 100.00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0258, Qtd Votos Lula: 88 - 100.00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0337, Qtd Votos Lula: 90 - 98.90% do total.
- UF MA (MARANHÃO), Município 08141 (PRESIDENTE SARNEY), Zona 0106, Seção 0244, Qtd Votos Lula: 75 - 100.00% do total.
- UF MA (MARANHÃO), Município 08150 (LAGO DO JUNCO), Zona 0074, Seção 0295, Qtd Votos Lula: 66 - 98.51% do total.
- UF MA (MARANHÃO), Município 08222 (SANTANA DO MARANHÃO), Zona 0051, Seção 0175, Qtd Votos Lula: 43 - 100.00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0133, Qtd Votos Lula: 85 - 100.00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0170, Qtd Votos Lula: 105 - 96.33% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0170, Qtd Votos Lula: 43 - 97.73% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0190, Qtd Votos Lula: 68 - 95.77% do total.
- UF MA (MARANHÃO), Município 08370 (MONÇÃO), Zona 0043, Seção 0198, Qtd Votos Lula: 110 - 99.10% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0028, Qtd Votos Lula: 261 - 100.00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0080, Qtd Votos Lula: 268 - 99.63% do total.
- UF MA (MARANHÃO), Município 08419 (MORROS), Zona 0110, Seção 0221, Qtd Votos Lula: 142 - 97.93% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0089, Qtd Votos Lula: 71 - 100.00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0156, Qtd Votos Lula: 58 - 95.08% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0148, Qtd Votos Lula: 73 - 100.00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0159, Qtd Votos Lula: 88 - 100.00% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0063, Qtd Votos Lula: 118 - 98.33% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0070, Qtd Votos Lula: 61 - 95.31% do total.
- UF MA (MARANHÃO), Município 08753 (POÇÃO DE PEDRAS), Zona 0061, Seção 0114, Qtd Votos Lula: 65 - 95.59% do total.
- UF MA (MARANHÃO), Município 08834 (PRESIDENTE VARGAS), Zona 0050, Seção 0151, Qtd Votos Lula: 120 - 99.17% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0089, Qtd Votos Lula: 91 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0090, Qtd Votos Lula: 36 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0193, Qtd Votos Lula: 52 - 92.86% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0202, Qtd Votos Lula: 57 - 93.44% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0186, Qtd Votos Lula: 58 - 100.00% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0352, Qtd Votos Lula: 83 - 100.00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0098, Qtd Votos Lula: 113 - 100.00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0163, Qtd Votos Lula: 85 - 100.00% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0116, Qtd Votos Lula: 160 - 98.77% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0167, Qtd Votos Lula: 89 - 98.89% do total.
- UF MA (MARANHÃO), Município 09415 (TURIAÇU), Zona 0039, Seção 0206, Qtd Votos Lula: 81 - 98.78% do total.
- UF MA (MARANHÃO), Município 09555 (BOM JARDIM), Zona 0078, Seção 0251, Qtd Votos Lula: 65 - 97.01% do total.
- UF MA (MARANHÃO), Município 09555 (BOM JARDIM), Zona 0078, Seção 0283, Qtd Votos Lula: 170 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 40320 (JUATUBA), Zona 0172, Seção 0183, Qtd Votos Lula: 22 - 95.65% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0178, Qtd Votos Lula: 152 - 99.35% do total.
- UF MG (MINAS GERAIS), Município 42757 (CARMÉSIA), Zona 0113, Seção 0042, Qtd Votos Lula: 80 - 96.39% do total.
- UF MG (MINAS GERAIS), Município 46795 (ITINGA), Zona 0015, Seção 0213, Qtd Votos Lula: 45 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0291, Qtd Votos Lula: 70 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0380, Qtd Votos Lula: 46 - 93.88% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0171, Qtd Votos Lula: 69 - 98.57% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173, Qtd Votos Lula: 16 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 50431 (PORTEIRINHA), Zona 0226, Seção 0219, Qtd Votos Lula: 27 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 52477 (SÃO JOÃO DA PONTE), Zona 0255, Seção 0146, Qtd Votos Lula: 62 - 96.88% do total.
- UF MS (MATO GROSSO DO SUL), Município 91804 (DOIS IRMÃOS DO BURITI), Zona 0049, Seção 0068, Qtd Votos Lula: 212 - 96.80% do total.
- UF MT (MATO GROSSO), Município 90280 (CONFRESA), Zona 0028, Seção 0158, Qtd Votos Lula: 375 - 100.00% do total.
- UF MT (MATO GROSSO), Município 90824 (GAÚCHA DO NORTE), Zona 0057, Seção 0078, Qtd Votos Lula: 122 - 100.00% do total.
- UF MT (MATO GROSSO), Município 91979 (SANTA TEREZINHA), Zona 0016, Seção 0086, Qtd Votos Lula: 246 - 99.60% do total.
- UF MT (MATO GROSSO), Município 98655 (CAMPINÁPOLIS), Zona 0026, Seção 0189, Qtd Votos Lula: 251 - 100.00% do total.
- UF MT (MATO GROSSO), Município 98817 (PEIXOTO DE AZEVEDO), Zona 0033, Seção 0255, Qtd Votos Lula: 112 - 100.00% do total.
- UF MT (MATO GROSSO), Município 98850 (PORTO ALEGRE DO NORTE), Zona 0028, Seção 0191, Qtd Votos Lula: 51 - 100.00% do total.
- UF PA (PARÁ), Município 04073 (ALENQUER), Zona 0021, Seção 0234, Qtd Votos Lula: 71 - 98.61% do total.
- UF PA (PARÁ), Município 04146 (SANTA LUZIA DO PARÁ), Zona 0041, Seção 0198, Qtd Votos Lula: 76 - 96.20% do total.
- UF PA (PARÁ), Município 04359 (BREVES), Zona 0015, Seção 0289, Qtd Votos Lula: 29 - 100.00% do total.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0092, Qtd Votos Lula: 27 - 90.00% do total.
- UF PA (PARÁ), Município 05037 (OEIRAS DO PARÁ), Zona 0045, Seção 0044, Qtd Votos Lula: 137 - 95.14% do total.
- UF PA (PARÁ), Município 05037 (OEIRAS DO PARÁ), Zona 0045, Seção 0084, Qtd Votos Lula: 71 - 100.00% do total.
- UF PA (PARÁ), Município 05053 (ORIXIMINÁ), Zona 0038, Seção 0260, Qtd Votos Lula: 122 - 100.00% do total.
- UF PA (PARÁ), Município 05452 (SÃO FÉLIX DO XINGU), Zona 0053, Seção 0217, Qtd Votos Lula: 123 - 100.00% do total.
- UF PA (PARÁ), Município 05657 (VISEU), Zona 0014, Seção 0221, Qtd Votos Lula: 49 - 90.74% do total.
- UF PI (PIAUÍ), Município 10243 (CORONEL JOSÉ DIAS), Zona 0013, Seção 0244, Qtd Votos Lula: 92 - 97.87% do total.
- UF PI (PIAUÍ), Município 10502 (SÃO BRAZ DO PIAUÍ), Zona 0095, Seção 0050, Qtd Votos Lula: 116 - 97.48% do total.
- UF PI (PIAUÍ), Município 10693 (ELESBÃO VELOSO), Zona 0048, Seção 0097, Qtd Votos Lula: 170 - 97.70% do total.
- UF PI (PIAUÍ), Município 10715 (ELISEU MARTINS), Zona 0067, Seção 0037, Qtd Votos Lula: 83 - 100.00% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0063, Qtd Votos Lula: 89 - 96.74% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0071, Qtd Votos Lula: 43 - 97.73% do total.
- UF PI (PIAUÍ), Município 11398 (OEIRAS), Zona 0005, Seção 0356, Qtd Votos Lula: 38 - 100.00% do total.
- UF PI (PIAUÍ), Município 12033 (SÃO JOSÉ DO PIAUÍ), Zona 0064, Seção 0073, Qtd Votos Lula: 95 - 100.00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0061, Qtd Votos Lula: 239 - 100.00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0104, Qtd Votos Lula: 98 - 98.00% do total.
- UF SC (SANTA CATARINA), Município 80942 (SÃO CRISTÓVÃO DO SUL), Zona 0011, Seção 0166, Qtd Votos Lula: 27 - 100.00% do total.
- UF SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0304, Qtd Votos Lula: 14 - 87.50% do total.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0341, Qtd Votos Lula: 16 - 94.12% do total.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0176, Seção 0471, Qtd Votos Lula: 38 - 97.44% do total.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0217, Seção 0353, Qtd Votos Lula: 33 - 97.06% do total.
- UF SP (SÃO PAULO), Município 69698 (RIBEIRÃO PRETO), Zona 0305, Seção 0333, Qtd Votos Lula: 16 - 94.12% do total.
- UF TO (TOCANTINS), Município 95257 (PEDRO AFONSO), Zona 0023, Seção 0104, Qtd Votos Lula: 66 - 100.00% do total.
- UF TO (TOCANTINS), Município 95338 (GOIATINS), Zona 0032, Seção 0070, Qtd Votos Lula: 266 - 100.00% do total.
- UF ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Qtd Votos Lula: 4 - 100.00% do total.

### Segundo Turno

#### Quantidade de Seções eleitorais que não tiveram votos para o Lula: 4.

- UF PA (PARÁ), Município 04499 (CHAVES), Zona 0017, Seção 0041, Qtd Votos Bolsonaro: 39 - 100.00% do total.
- UF RS (RIO GRANDE DO SUL), Município 86568 (CHARRUA), Zona 0100, Seção 0015, Qtd Votos Bolsonaro: 79 - 100.00% do total.
- UF ZZ (EXTERIOR), Município 29505 (CARACAS), Zona 0001, Seção 0078, Qtd Votos Bolsonaro: 5 - 100.00% do total.
- UF ZZ (EXTERIOR), Município 29505 (CARACAS), Zona 0001, Seção 0079, Qtd Votos Bolsonaro: 1 - 100.00% do total.
  
#### Quantidade de Seções eleitorais que não tiveram votos para o Bolsonaro: 143.

- UF AC (ACRE), Município 01040 (MARECHAL THAUMATURGO), Zona 0004, Seção 0326, Qtd Votos Lula: 153 - 100.00% do total.
- UF AC (ACRE), Município 01040 (MARECHAL THAUMATURGO), Zona 0004, Seção 0384, Qtd Votos Lula: 101 - 100.00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0025, Qtd Votos Lula: 138 - 100.00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0028, Qtd Votos Lula: 127 - 100.00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0029, Qtd Votos Lula: 175 - 100.00% do total.
- UF AM (AMAZONAS), Município 02054 (ATALAIA DO NORTE), Zona 0042, Seção 0037, Qtd Votos Lula: 107 - 100.00% do total.
- UF AM (AMAZONAS), Município 02119 (BARREIRINHA), Zona 0026, Seção 0063, Qtd Votos Lula: 237 - 100.00% do total.
- UF AM (AMAZONAS), Município 02216 (CARAUARI), Zona 0021, Seção 0096, Qtd Votos Lula: 91 - 100.00% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0022, Qtd Votos Lula: 196 - 100.00% do total.
- UF AM (AMAZONAS), Município 02372 (SANTA ISABEL DO RIO NEGRO), Zona 0030, Seção 0026, Qtd Votos Lula: 156 - 100.00% do total.
- UF AM (AMAZONAS), Município 02399 (IPIXUNA), Zona 0045, Seção 0052, Qtd Votos Lula: 94 - 100.00% do total.
- UF AM (AMAZONAS), Município 02410 (ITACOATIARA), Zona 0003, Seção 0158, Qtd Votos Lula: 37 - 100.00% do total.
- UF AM (AMAZONAS), Município 02739 (SANTO ANTÔNIO DO IÇÁ), Zona 0047, Seção 0010, Qtd Votos Lula: 303 - 100.00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0044, Qtd Votos Lula: 277 - 100.00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0080, Qtd Votos Lula: 347 - 100.00% do total.
- UF AM (AMAZONAS), Município 02755 (SÃO PAULO DE OLIVENÇA), Zona 0022, Seção 0086, Qtd Votos Lula: 350 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0013, Qtd Votos Lula: 239 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0025, Qtd Votos Lula: 207 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0048, Qtd Votos Lula: 166 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0059, Qtd Votos Lula: 135 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0065, Qtd Votos Lula: 204 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0087, Qtd Votos Lula: 162 - 100.00% do total.
- UF AM (AMAZONAS), Município 02836 (SÃO GABRIEL DA CACHOEIRA), Zona 0019, Seção 0092, Qtd Votos Lula: 195 - 100.00% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0098, Qtd Votos Lula: 146 - 100.00% do total.
- UF AM (AMAZONAS), Município 98477 (TABATINGA), Zona 0036, Seção 0100, Qtd Votos Lula: 132 - 100.00% do total.
- UF BA (BAHIA), Município 33413 (ARACI), Zona 0123, Seção 0137, Qtd Votos Lula: 87 - 100.00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0247, Qtd Votos Lula: 117 - 100.00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0255, Qtd Votos Lula: 60 - 100.00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0344, Qtd Votos Lula: 99 - 100.00% do total.
- UF BA (BAHIA), Município 33812 (BOQUIRA), Zona 0065, Seção 0345, Qtd Votos Lula: 52 - 100.00% do total.
- UF BA (BAHIA), Município 33898 (BROTAS DE MACAÚBAS), Zona 0094, Seção 0028, Qtd Votos Lula: 35 - 100.00% do total.
- UF BA (BAHIA), Município 34231 (CANARANA), Zona 0174, Seção 0111, Qtd Votos Lula: 141 - 100.00% do total.
- UF BA (BAHIA), Município 34436 (CASA NOVA), Zona 0066, Seção 0134, Qtd Votos Lula: 104 - 100.00% do total.
- UF BA (BAHIA), Município 35157 (FEIRA DE SANTANA), Zona 0155, Seção 0800, Qtd Votos Lula: 19 - 100.00% do total.
- UF BA (BAHIA), Município 35637 (IBITITÁ), Zona 0104, Seção 0241, Qtd Votos Lula: 55 - 100.00% do total.
- UF BA (BAHIA), Município 36714 (JUSSARA), Zona 0159, Seção 0057, Qtd Votos Lula: 73 - 100.00% do total.
- UF BA (BAHIA), Município 37737 (PARAMIRIM), Zona 0111, Seção 0208, Qtd Votos Lula: 60 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0037, Qtd Votos Lula: 33 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0068, Qtd Votos Lula: 87 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0076, Qtd Votos Lula: 65 - 100.00% do total.
- UF BA (BAHIA), Município 37893 (PILÃO ARCADO), Zona 0195, Seção 0132, Qtd Votos Lula: 173 - 100.00% do total.
- UF BA (BAHIA), Município 38490 (SALVADOR), Zona 0005, Seção 0800, Qtd Votos Lula: 80 - 100.00% do total.
- UF BA (BAHIA), Município 38970 (SEABRA), Zona 0088, Seção 0181, Qtd Votos Lula: 141 - 100.00% do total.
- UF BA (BAHIA), Município 39942 (NOVO HORIZONTE), Zona 0088, Seção 0304, Qtd Votos Lula: 72 - 100.00% do total.
- UF CE (CEARÁ), Município 13102 (QUITERIANÓPOLIS), Zona 0099, Seção 0141, Qtd Votos Lula: 116 - 100.00% do total.
- UF CE (CEARÁ), Município 14478 (JUAZEIRO DO NORTE), Zona 0119, Seção 0267, Qtd Votos Lula: 22 - 100.00% do total.
- UF CE (CEARÁ), Município 15113 (PEDRA BRANCA), Zona 0059, Seção 0112, Qtd Votos Lula: 95 - 100.00% do total.
- UF CE (CEARÁ), Município 15113 (PEDRA BRANCA), Zona 0059, Seção 0114, Qtd Votos Lula: 73 - 100.00% do total.
- UF CE (CEARÁ), Município 15210 (PORANGA), Zona 0040, Seção 0163, Qtd Votos Lula: 69 - 100.00% do total.
- UF CE (CEARÁ), Município 15296 (QUIXERAMOBIM), Zona 0011, Seção 0186, Qtd Votos Lula: 103 - 100.00% do total.
- UF CE (CEARÁ), Município 15431 (SANTANA DO CARIRI), Zona 0053, Seção 0050, Qtd Votos Lula: 159 - 100.00% do total.
- UF CE (CEARÁ), Município 15652 (TAMBORIL), Zona 0061, Seção 0102, Qtd Votos Lula: 81 - 100.00% do total.
- UF MA (MARANHÃO), Município 07005 (ÁGUA DOCE DO MARANHÃO), Zona 0012, Seção 0174, Qtd Votos Lula: 73 - 100.00% do total.
- UF MA (MARANHÃO), Município 07110 (AMARANTE DO MARANHÃO), Zona 0099, Seção 0127, Qtd Votos Lula: 73 - 100.00% do total.
- UF MA (MARANHÃO), Município 07250 (BACURI), Zona 0107, Seção 0038, Qtd Votos Lula: 106 - 100.00% do total.
- UF MA (MARANHÃO), Município 07315 (BARRA DO CORDA), Zona 0023, Seção 0019, Qtd Votos Lula: 74 - 100.00% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0175, Qtd Votos Lula: 87 - 100.00% do total.
- UF MA (MARANHÃO), Município 07331 (BARREIRINHAS), Zona 0056, Seção 0239, Qtd Votos Lula: 51 - 100.00% do total.
- UF MA (MARANHÃO), Município 07390 (BREJO), Zona 0024, Seção 0253, Qtd Votos Lula: 61 - 100.00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0185, Qtd Votos Lula: 283 - 100.00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0205, Qtd Votos Lula: 310 - 100.00% do total.
- UF MA (MARANHÃO), Município 07544 (FERNANDO FALCÃO), Zona 0097, Seção 0207, Qtd Votos Lula: 69 - 100.00% do total.
- UF MA (MARANHÃO), Município 07552 (CARUTAPERA), Zona 0055, Seção 0069, Qtd Votos Lula: 53 - 100.00% do total.
- UF MA (MARANHÃO), Município 07706 (ITAIPAVA DO GRAJAÚ), Zona 0015, Seção 0314, Qtd Votos Lula: 277 - 100.00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0175, Qtd Votos Lula: 56 - 100.00% do total.
- UF MA (MARANHÃO), Município 07714 (CURURUPU), Zona 0014, Seção 0193, Qtd Votos Lula: 19 - 100.00% do total.
- UF MA (MARANHÃO), Município 07927 (MARANHÃOZINHO), Zona 0101, Seção 0078, Qtd Votos Lula: 307 - 100.00% do total.
- UF MA (MARANHÃO), Município 07951 (GUIMARÃES), Zona 0030, Seção 0074, Qtd Votos Lula: 101 - 100.00% do total.
- UF MA (MARANHÃO), Município 07978 (HUMBERTO DE CAMPOS), Zona 0032, Seção 0055, Qtd Votos Lula: 39 - 100.00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0168, Qtd Votos Lula: 91 - 100.00% do total.
- UF MA (MARANHÃO), Município 07994 (ICATU), Zona 0031, Seção 0197, Qtd Votos Lula: 44 - 100.00% do total.
- UF MA (MARANHÃO), Município 08060 (PEDRO DO ROSÁRIO), Zona 0106, Seção 0054, Qtd Votos Lula: 204 - 100.00% do total.
- UF MA (MARANHÃO), Município 08079 (ITAPECURU MIRIM), Zona 0016, Seção 0337, Qtd Votos Lula: 89 - 100.00% do total.
- UF MA (MARANHÃO), Município 08141 (PRESIDENTE SARNEY), Zona 0106, Seção 0224, Qtd Votos Lula: 130 - 100.00% do total.
- UF MA (MARANHÃO), Município 08150 (LAGO DO JUNCO), Zona 0074, Seção 0295, Qtd Votos Lula: 66 - 100.00% do total.
- UF MA (MARANHÃO), Município 08222 (SANTANA DO MARANHÃO), Zona 0051, Seção 0175, Qtd Votos Lula: 40 - 100.00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0133, Qtd Votos Lula: 83 - 100.00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0138, Qtd Votos Lula: 98 - 100.00% do total.
- UF MA (MARANHÃO), Município 08249 (SANTO AMARO DO MARANHÃO), Zona 0032, Seção 0170, Qtd Votos Lula: 103 - 100.00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0028, Qtd Votos Lula: 260 - 100.00% do total.
- UF MA (MARANHÃO), Município 08397 (MONTES ALTOS), Zona 0103, Seção 0080, Qtd Votos Lula: 264 - 100.00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0087, Qtd Votos Lula: 60 - 100.00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0089, Qtd Votos Lula: 68 - 100.00% do total.
- UF MA (MARANHÃO), Município 08524 (SERRANO DO MARANHÃO), Zona 0107, Seção 0156, Qtd Votos Lula: 61 - 100.00% do total.
- UF MA (MARANHÃO), Município 08559 (PARNARAMA), Zona 0036, Seção 0069, Qtd Votos Lula: 80 - 100.00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0148, Qtd Votos Lula: 70 - 100.00% do total.
- UF MA (MARANHÃO), Município 08605 (TURILÂNDIA), Zona 0083, Seção 0159, Qtd Votos Lula: 90 - 100.00% do total.
- UF MA (MARANHÃO), Município 08630 (PENALVA), Zona 0045, Seção 0063, Qtd Votos Lula: 119 - 100.00% do total.
- UF MA (MARANHÃO), Município 08834 (PRESIDENTE VARGAS), Zona 0050, Seção 0151, Qtd Votos Lula: 121 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0072, Qtd Votos Lula: 93 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0089, Qtd Votos Lula: 92 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0090, Qtd Votos Lula: 42 - 100.00% do total.
- UF MA (MARANHÃO), Município 08850 (PRIMEIRA CRUZ), Zona 0032, Seção 0202, Qtd Votos Lula: 60 - 100.00% do total.
- UF MA (MARANHÃO), Município 08974 (SANTA LUZIA), Zona 0070, Seção 0186, Qtd Votos Lula: 56 - 100.00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0067, Qtd Votos Lula: 213 - 100.00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0098, Qtd Votos Lula: 107 - 100.00% do total.
- UF MA (MARANHÃO), Município 09059 (SÃO BENEDITO DO RIO PRETO), Zona 0073, Seção 0163, Qtd Votos Lula: 86 - 100.00% do total.
- UF MA (MARANHÃO), Município 09075 (SÃO BENTO), Zona 0038, Seção 0167, Qtd Votos Lula: 87 - 100.00% do total.
- UF MA (MARANHÃO), Município 09415 (TURIAÇU), Zona 0039, Seção 0058, Qtd Votos Lula: 148 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 40320 (JUATUBA), Zona 0172, Seção 0183, Qtd Votos Lula: 21 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0152, Qtd Votos Lula: 207 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 41319 (BERTÓPOLIS), Zona 0004, Seção 0178, Qtd Votos Lula: 165 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 42757 (CARMÉSIA), Zona 0113, Seção 0042, Qtd Votos Lula: 82 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0276, Qtd Votos Lula: 115 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 47031 (JANUÁRIA), Zona 0148, Seção 0291, Qtd Votos Lula: 75 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0171, Qtd Votos Lula: 68 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 50431 (PORTEIRINHA), Zona 0226, Seção 0219, Qtd Votos Lula: 28 - 100.00% do total.
- UF MG (MINAS GERAIS), Município 52213 (SÃO FRANCISCO), Zona 0252, Seção 0211, Qtd Votos Lula: 79 - 100.00% do total.
- UF MS (MATO GROSSO DO SUL), Município 91804 (DOIS IRMÃOS DO BURITI), Zona 0049, Seção 0068, Qtd Votos Lula: 206 - 100.00% do total.
- UF MT (MATO GROSSO), Município 90280 (CONFRESA), Zona 0028, Seção 0158, Qtd Votos Lula: 383 - 100.00% do total.
- UF MT (MATO GROSSO), Município 91979 (SANTA TEREZINHA), Zona 0016, Seção 0086, Qtd Votos Lula: 248 - 100.00% do total.
- UF MT (MATO GROSSO), Município 98655 (CAMPINÁPOLIS), Zona 0026, Seção 0189, Qtd Votos Lula: 265 - 100.00% do total.
- UF MT (MATO GROSSO), Município 98817 (PEIXOTO DE AZEVEDO), Zona 0033, Seção 0255, Qtd Votos Lula: 116 - 100.00% do total.
- UF MT (MATO GROSSO), Município 98850 (PORTO ALEGRE DO NORTE), Zona 0028, Seção 0191, Qtd Votos Lula: 55 - 100.00% do total.
- UF PA (PARÁ), Município 04146 (SANTA LUZIA DO PARÁ), Zona 0041, Seção 0198, Qtd Votos Lula: 82 - 100.00% do total.
- UF PA (PARÁ), Município 04359 (BREVES), Zona 0015, Seção 0289, Qtd Votos Lula: 29 - 100.00% do total.
- UF PA (PARÁ), Município 04723 (ANAPU), Zona 0080, Seção 0254, Qtd Votos Lula: 157 - 100.00% do total.
- UF PA (PARÁ), Município 04774 (JURUTI), Zona 0105, Seção 0092, Qtd Votos Lula: 29 - 100.00% do total.
- UF PA (PARÁ), Município 04936 (MOJU), Zona 0037, Seção 0120, Qtd Votos Lula: 49 - 100.00% do total.
- UF PA (PARÁ), Município 05053 (ORIXIMINÁ), Zona 0038, Seção 0260, Qtd Votos Lula: 111 - 100.00% do total.
- UF PA (PARÁ), Município 05436 (SÃO DOMINGOS DO CAPIM), Zona 0050, Seção 0181, Qtd Votos Lula: 103 - 100.00% do total.
- UF PA (PARÁ), Município 05452 (SÃO FÉLIX DO XINGU), Zona 0053, Seção 0218, Qtd Votos Lula: 194 - 100.00% do total.
- UF PI (PIAUÍ), Município 10243 (CORONEL JOSÉ DIAS), Zona 0013, Seção 0242, Qtd Votos Lula: 119 - 100.00% do total.
- UF PI (PIAUÍ), Município 10502 (SÃO BRAZ DO PIAUÍ), Zona 0095, Seção 0050, Qtd Votos Lula: 117 - 100.00% do total.
- UF PI (PIAUÍ), Município 10871 (GILBUÉS), Zona 0035, Seção 0063, Qtd Votos Lula: 87 - 100.00% do total.
- UF PI (PIAUÍ), Município 11398 (OEIRAS), Zona 0005, Seção 0356, Qtd Votos Lula: 34 - 100.00% do total.
- UF PI (PIAUÍ), Município 11673 (PIRIPIRI), Zona 0011, Seção 0184, Qtd Votos Lula: 85 - 100.00% do total.
- UF PI (PIAUÍ), Município 12033 (SÃO JOSÉ DO PIAUÍ), Zona 0064, Seção 0073, Qtd Votos Lula: 98 - 100.00% do total.
- UF PI (PIAUÍ), Município 12190 (TERESINA), Zona 0097, Seção 0150, Qtd Votos Lula: 21 - 100.00% do total.
- UF PR (PARANÁ), Município 77275 (ORTIGUEIRA), Zona 0167, Seção 0096, Qtd Votos Lula: 223 - 100.00% do total.
- UF RR (RORAIMA), Município 03107 (UIRAMUTÃ), Zona 0007, Seção 0063, Qtd Votos Lula: 176 - 100.00% do total.
- UF RR (RORAIMA), Município 03115 (NORMANDIA), Zona 0005, Seção 0627, Qtd Votos Lula: 55 - 100.00% do total.
- UF RS (RIO GRANDE DO SUL), Município 86568 (CHARRUA), Zona 0100, Seção 0126, Qtd Votos Lula: 302 - 100.00% do total.
- UF RS (RIO GRANDE DO SUL), Município 87858 (PASSO FUNDO), Zona 0128, Seção 0492, Qtd Votos Lula: 37 - 100.00% do total.
- UF SC (SANTA CATARINA), Município 80942 (SÃO CRISTÓVÃO DO SUL), Zona 0011, Seção 0166, Qtd Votos Lula: 28 - 100.00% do total.
- UF SE (SERGIPE), Município 31950 (NOSSA SENHORA DO SOCORRO), Zona 0034, Seção 0304, Qtd Votos Lula: 17 - 100.00% do total.
- UF SP (SÃO PAULO), Município 64254 (FRANCA), Zona 0291, Seção 0341, Qtd Votos Lula: 14 - 100.00% do total.
- UF SP (SÃO PAULO), Município 64777 (GUARULHOS), Zona 0185, Seção 0372, Qtd Votos Lula: 10 - 100.00% do total.
- UF SP (SÃO PAULO), Município 66893 (MAUÁ), Zona 0217, Seção 0353, Qtd Votos Lula: 32 - 100.00% do total.
- UF SP (SÃO PAULO), Município 67890 (OSASCO), Zona 0315, Seção 0336, Qtd Votos Lula: 89 - 100.00% do total.
- UF TO (TOCANTINS), Município 93653 (FORMOSO DO ARAGUAIA), Zona 0015, Seção 0072, Qtd Votos Lula: 97 - 100.00% do total.
- UF ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127, Qtd Votos Lula: 32 - 100.00% do total.
- UF ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504, Qtd Votos Lula: 5 - 100.00% do total.

## Mudança de direção nos votos

Bolsonaro conseguiu recuperar no segundo turno uma parte considerável dos votos que foram para outros candidatos no primeiro turno.
A conta que foi feita para obter o número de votos virados foi a seguinte: (Votos de Lula - Votos de Bolsonaro no primeiro turno) - (Votos de Lula - Votos de Bolsonaro no segundo turno).
Se o resultado for negativo, é porque Lula virou mais votos. Se for positivo, é porque Bolsonaro virou mais fotos.

### Municípios que viraram votos para o Lula do primeiro para o segundo turno: 771.

#### Lista dos municípios que viraram (a partir de 10 mil votos)

Não há nenhum município em que Lula tenha virado mais do que 10 mil votos. O mais próximo disso foi Sobral (CE), com 8712 votos virados, o segundo município que mais virou votos para Lula foi Camocim, também no Ceará, porém com apenas 1292 votos virados.

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




