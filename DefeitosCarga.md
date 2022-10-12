# Defeitos e inconsistências dos dados do TSE

Eu desenvolvi o TSE Parser e o TSE Crawler para poder baixar os dados de todas as urnas eletrônicas e poder realizar meus próprios estudos sobre estes dados. 99% dos dados estão corretos e funcionam sem problemas.

### Contagem de Seções incorreta

O site do TSE reporta que existem 472.075 seções no total. 

O TSE Crawler, no entanto, só encontrou 471.970 seções eleitorais, **105** a menos do que o reportado pelo TSE. 

Destas 105 seções faltantes, **51** são defeitos de carregamento de seções do exterior (UF "ZZ"). 

As 54 seções faltantes eu simplesmente não consegui localizar. Provavelmente são seções do Exterior (ZZ), pois todas as UFs no Brasil possuem quantidades de votos exatamente iguais as apresentadas pelo site do TSE. Porém, a contagem de votos para o Exterior não é igual.
Esta contagem deveria ser igual ao número de seções reportadas nos arquivos JSON de cada UF, porém, somando as seções reportadas, o número é de **472.021**, 54 a menos do que as 472.075 informadas no site do TSE.

Os arquivos JSON a que me refiro estão no site do TSE. São os seguintes (repare que os nomes são iguais, mudando apenas a sigla da UF):

- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ac/ac-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/al/al-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/am/am-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ap/ap-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ba/ba-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/br/br-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ce/ce-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/df/df-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/es/es-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/go/go-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ma/ma-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/mg/mg-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ms/ms-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/mt/mt-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/pa/pa-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/pb/pb-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/pe/pe-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/pi/pi-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/pr/pr-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/rj/rj-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/rn/rn-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/ro/ro-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/rr/rr-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/rs/rs-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/sc/sc-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/se/se-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/sp/sp-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/to/to-p000406-cs.json
- https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/zz/zz-p000406-cs.json

### Imagem do Boletim de Urna faltando

De todas as 471.010 seções no Brasil, apenas uma não possui o arquivo de Imagem de Boletim de Urna (arquivo com extensão `.imgbu`). É a da Unidade Federativa MA (Maranhão), Município São Matheus do Maranhão (09237), Zona Eleitoral 0084, Seção 215.

O TSE Parser usa este arquivo para importar os dados por padrão. Esta decisão foi tomada pois o arquivo de Imagem do Boletim da Urna é (ou deveria ser) exatamente o mesmo disponibilizado para os eleitores após o encerramento da votação. Portanto, trata-se de um documento oficial. Além disso, ler este arquivo é mais simples, já que se trata de texto puro e não exige um programa específico para leitura.

Para este caso específico, a solução foi importar o arquivo `.bu`, que é o Boletim de Urna em formato binário. Após importar, a soma de votos para o estado do Maranhão ficou idêntica a disponibilizada pelo TSE.

### Diferenças de votos entre os arquivos .bu e .imgbu

Existem diversas seções que apresentam diferenças entre os dados do arquivo `.bu` e do arquivo `.imgbu`. Porém, em apenas uma delas, há uma preocupante diferença de votos.

Na Unidade Federativa RS (Rio Grande do Sul), Município Porto Alegre (88013), Zona Eleitoral 0002, Seção 0199. Os arquivos `.imgbu` e `.bu` não parecem ter sido gerados pela mesma Urna Eletrônica.

No arquivo `.imgbu` o código de identificação da Urna Eletrônica é "02215062", e no arquivo `.bu`, o código é "02228407". 

Embora o número do município, zona eleitoral, seção e local de votação sejam os mesmos em ambos os arquivos, todos os demais dados são diferentes, incluindo contagem de votos e listas de candidados. 

**É impossível determinar qual dos dois arquivos é o que, de fato, representa a votação desta seção eleitoral.** 

A fim de manter o banco de dados fielmente igual ao site do TSE, a solução foi importar para esta seção o arquivo `.bu` em vez do `.imgbu`. 

Mas não deixa de ser estranho o fato de haver dois arquivos diferentes, gerados por duas urnas diferentes, com votações diferentes, atribuídos para uma mesma seção eleitoral.

Existem muitas outras seções eleitorais em que o Código de Identificação da Urna Eletrônica é diferente entre os arquivos `.bu` e `.imgbu`. Estes defeitos serão listados mais adiante.

### Erros no Exterior (Unidade Federativa "ZZ")

Estes são as 51 seções eleitorais no exterior que possuem informações no [JSON de configuração da UF](https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/config/zz/zz-p000406-cs.json), mas que não possuem arquivos para processar. Existem basicamente 3 tipos de defeito: 

- Sem arquivo

- Rejeitado

- Excluído

Nos 3 casos, não há arquivo `.bu` nem `.imgbu`. 

**Mesmo pelo site do TSE, estas seções não são exibidas**. 

Eu não sei como o TSE chegou nos números totais que são apresentados no site para a apuração das urnas do exterior.

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

### Códigos de identificação das Urnas Eletrônicas são diferentes entre os arquivos .bu e .imgbu

Existem 46 casos em que o código de identificação da Urna Eletrônica é diferente no arquivo `.bu` e na Imagem do Boletim da Urna. Embora o número da Urna seja diferente, os votos em ambos os arquivos são iguais (exceto um caso).

1. UF AL MUN 27650 ZN 0039 SE 0099 -  imgbu: 01076434, bu: 01769281
2. UF BA MUN 33596 ZN 0176 SE 0032 -  imgbu: 01226785, bu: 01240144
3. UF CE MUN 14575 ZN 0096 SE 0162 -  imgbu: 01007106, bu: 01003085
4. UF CE MUN 13897 ZN 0094 SE 0935 -  imgbu: 02234041, bu: 02233967
5. UF ES MUN 56138 ZN 0002 SE 0021 -  imgbu: 02006899, bu: 02040131
6. UF GO MUN 93033 ZN 0101 SE 0036 -  imgbu: 01202220, bu: 01005082
7. UF GO MUN 93734 ZN 0001 SE 0461 -  imgbu: 02199601, bu: 02201801
8. UF MA MUN 09679 ZN 0104 SE 0028 -  imgbu: 01662137, bu: 01618053
9. UF MS MUN 90514 ZN 0054 SE 0183 -  imgbu: 02146273, bu: 02023409
10. UF MG MUN 40215 ZN 0005 SE 0002 -  imgbu: 01754355, bu: 01836250
11. UF MG MUN 49590 ZN 0330 SE 0174 -  imgbu: 01077196, bu: 01193544
12. UF MG MUN 42994 ZN 0110 SE 0037 -  imgbu: 01170320, bu: 01143085
13. UF MG MUN 53872 ZN 0273 SE 0154 -  imgbu: 02091360, bu: 02076864
14. UF MG MUN 48771 ZN 0187 SE 0442 -  imgbu: 01310508, bu: 01284763
15. UF MG MUN 49611 ZN 0211 SE 0011 -  imgbu: 01255198, bu: 01334439
16. UF MG MUN 53597 ZN 0266 SE 0175 -  imgbu: 01606205, bu: 01601751
17. UF PA MUN 04030 ZN 0094 SE 0074 -  imgbu: 01274748, bu: 01629320
18. UF PA MUN 04073 ZN 0021 SE 0116 -  imgbu: 01808680, bu: 01078913
19. UF PB MUN 21113 ZN 0023 SE 0023 -  imgbu: 01294014, bu: 01244311
20. UF PR MUN 76813 ZN 0196 SE 0155 -  imgbu: 01139732, bu: 01172194
21. UF PR MUN 77275 ZN 0167 SE 0002 -  imgbu: 01262352, bu: 01251523
22. UF PR MUN 75698 ZN 0092 SE 0046 -  imgbu: 01242372, bu: 01252224
23. UF PE MUN 23035 ZN 0107 SE 0024 -  imgbu: 01319617, bu: 01320899
24. UF PE MUN 25313 ZN 0001 SE 0276 -  imgbu: 02171611, bu: 02193066
25. UF PE MUN 25313 ZN 0002 SE 0500 -  imgbu: 02043695, bu: 02044142
26. UF PI MUN 11037 ZN 0019 SE 0016 -  imgbu: 01335651, bu: 01335552
27. UF RJ MUN 60011 ZN 0119 SE 0228 -  imgbu: 02177741, bu: 02156817
28. UF RJ MUN 60011 ZN 0180 SE 0007 -  imgbu: 02179235, bu: 02178352
29. UF RJ MUN 58777 ZN 0029 SE 0017 -  imgbu: 01233956, bu: 01265036
30. UF RJ MUN 59013 ZN 0187 SE 0267 -  imgbu: 01276807, bu: 01239312
31. UF RJ MUN 59153 ZN 0038 SE 0276 -  imgbu: 01289247, bu: 01230490
32. UF RJ MUN 58335 ZN 0103 SE 0374 -  imgbu: 01143078, bu: 01079776
33. UF RN MUN 17590 ZN 0033 SE 0060 -  imgbu: 01309624, bu: 01256052
34. UF RS MUN 88013 ZN 0002 SE 0199 -  imgbu: 02215062, bu: 02228407 (Este é o caso que também apresentou diferença de votos)
35. UF RS MUN 89575 ZN 0162 SE 0328 -  imgbu: 02176398, bu: 02176680
36. UF RS MUN 85111 ZN 0074 SE 0314 -  imgbu: 01803675, bu: 01802139
37. UF SC MUN 83259 ZN 0028 SE 0095 -  imgbu: 01337227, bu: 01105360
38. UF SC MUN 81833 ZN 0104 SE 0155 -  imgbu: 02182199, bu: 02181355
39. UF SP MUN 62138 ZN 0199 SE 0273 -  imgbu: 01092722, bu: 01021346
40. UF SP MUN 70572 ZN 0263 SE 0223 -  imgbu: 01165648, bu: 01197381
41. UF SP MUN 68756 ZN 0093 SE 0481 -  imgbu: 02214722, bu: 02214488
42. UF SP MUN 64254 ZN 0291 SE 0209 -  imgbu: 01342905, bu: 01113866
43. UF SP MUN 61778 ZN 0335 SE 0021 -  imgbu: 01600858, bu: 01288837
44. UF SP MUN 62936 ZN 0344 SE 0063 -  imgbu: 01626780, bu: 01092477
45. UF SP MUN 71072 ZN 0006 SE 0399 -  imgbu: 02049718, bu: 02046902
46. UF SP MUN 71072 ZN 0375 SE 0064 -  imgbu: 01242054, bu: 01056799

### Arquivos .bu corrompidos

Na Unidade Federativa **PE** (Pernambuco), Município **30015** (FERNANDO DE NORONHA), Zona Eleitoral **0004**, todas as 9 seções eleitorais apresentam arquivos `.bu` corrompidos. Estes arquivos não puderam ser decodificados pelo TSE Parser.

São as seções eleitorais: 146, 147, 186, 192, 215, 227, 234, 370 e 372.

Apesar deste problema, os arquivos `.imgbu` estão legíveis e os votos foram carregados normalmente. A contagem de votos no site do TSE também está correta, então o TSE de alguma forma, conseguiu recuperar estes dados.

### Votos para Deputados Estaduais e Deputados Federais trocados no arquivo .bu

No arquivo `.bu`, cada lista de votos (`TotalVotosVotavel`) está contida em uma série de outras listas que definem o tipo de cargo (majoritário ou proporcional), e o cargo constitucional dos votos daquela lista (Deputado Federal, Estadual ou Distrital, Senador, Governador ou Presidente).

Na Unidade Federativa SP (São Paulo), alguns arquivos `.bu` tiveram votos para um candidato a deputado federal com número **3043**. Ocorre que **não há, neste estado, candidato a deputado federal com este número**. Observando com cuidado, percebe-se que na verdade, o voto deveria ter sido computado para um deputado estadual com número **43043**. No TSE a contagem de votos aparece correta, assim como na imagem do boletim de urna. Então este é um defeito do arquivo `.bu`.

Este defeito ocorreu em 235 seções eleitorais do estado de São Paulo:

1. UF SP MUN 63134 ZN 0303 SE 0087
2. UF SP MUN 64017 ZN 0391 SE 0166
3. UF SP MUN 65897 ZN 0396 SE 0067
4. UF SP MUN 68730 ZN 0092 SE 0039
5. UF SP MUN 70718 ZN 0272 SE 0273
6. UF SP MUN 70718 ZN 0272 SE 0227
7. UF SP MUN 70718 ZN 0272 SE 0063
8. UF SP MUN 70718 ZN 0273 SE 0157
9. UF SP MUN 70718 ZN 0273 SE 0095
10. UF SP MUN 70718 ZN 0273 SE 0013
11. UF SP MUN 70971 ZN 0267 SE 0010
12. UF SP MUN 61638 ZN 0385 SE 0028
13. UF SP MUN 61816 ZN 0016 SE 0439
14. UF SP MUN 62138 ZN 0386 SE 0207
15. UF SP MUN 64270 ZN 0367 SE 0096
16. UF SP MUN 64270 ZN 0367 SE 0103
17. UF SP MUN 65439 ZN 0189 SE 0123
18. UF SP MUN 61565 ZN 0190 SE 0075
19. UF SP MUN 62910 ZN 0033 SE 0319
20. UF SP MUN 62910 ZN 0274 SE 0633
21. UF SP MUN 62910 ZN 0275 SE 0480
22. UF SP MUN 62910 ZN 0275 SE 0247
23. UF SP MUN 62910 ZN 0275 SE 0598
24. UF SP MUN 62910 ZN 0380 SE 0172
25. UF SP MUN 62910 ZN 0380 SE 0230
26. UF SP MUN 62910 ZN 0380 SE 0232
27. UF SP MUN 62910 ZN 0380 SE 0045
28. UF SP MUN 62910 ZN 0423 SE 0236
29. UF SP MUN 65110 ZN 0211 SE 0439
30. UF SP MUN 67032 ZN 0153 SE 0102
31. UF SP MUN 67032 ZN 0153 SE 0077
32. UF SP MUN 67032 ZN 0153 SE 0058
33. UF SP MUN 67032 ZN 0153 SE 0021
34. UF SP MUN 67695 ZN 0292 SE 0131
35. UF SP MUN 67695 ZN 0292 SE 0056
36. UF SP MUN 69795 ZN 0288 SE 0123
37. UF SP MUN 70939 ZN 0018 SE 0027
38. UF SP MUN 63096 ZN 0038 SE 0001
39. UF SP MUN 66710 ZN 0237 SE 0196
40. UF SP MUN 66710 ZN 0237 SE 0144
41. UF SP MUN 66710 ZN 0237 SE 0015
42. UF SP MUN 67377 ZN 0358 SE 0001
43. UF SP MUN 68616 ZN 0090 SE 0132
44. UF SP MUN 68616 ZN 0090 SE 0208
45. UF SP MUN 69019 ZN 0099 SE 0047
46. UF SP MUN 69019 ZN 0099 SE 0054
47. UF SP MUN 70572 ZN 0156 SE 0128
48. UF SP MUN 70572 ZN 0264 SE 0023
49. UF SP MUN 70572 ZN 0306 SE 0122
50. UF SP MUN 70572 ZN 0307 SE 0401
51. UF SP MUN 70572 ZN 0383 SE 0089
52. UF SP MUN 70572 ZN 0383 SE 0243
53. UF SP MUN 71498 ZN 0230 SE 0231
54. UF SP MUN 61310 ZN 0158 SE 0207
55. UF SP MUN 61310 ZN 0158 SE 0192
56. UF SP MUN 61310 ZN 0158 SE 0273
57. UF SP MUN 61310 ZN 0158 SE 0267
58. UF SP MUN 61310 ZN 0158 SE 0240
59. UF SP MUN 61310 ZN 0158 SE 0291
60. UF SP MUN 61310 ZN 0158 SE 0012
61. UF SP MUN 61310 ZN 0158 SE 0006
62. UF SP MUN 61310 ZN 0158 SE 0215
63. UF SP MUN 61310 ZN 0158 SE 0132
64. UF SP MUN 61310 ZN 0158 SE 0116
65. UF SP MUN 61310 ZN 0158 SE 0115
66. UF SP MUN 61310 ZN 0158 SE 0314
67. UF SP MUN 61310 ZN 0158 SE 0308
68. UF SP MUN 61310 ZN 0158 SE 0242
69. UF SP MUN 61310 ZN 0158 SE 0227
70. UF SP MUN 61310 ZN 0158 SE 0226
71. UF SP MUN 61310 ZN 0158 SE 0140
72. UF SP MUN 61310 ZN 0158 SE 0134
73. UF SP MUN 61310 ZN 0158 SE 0118
74. UF SP MUN 61310 ZN 0158 SE 0113
75. UF SP MUN 61310 ZN 0158 SE 0080
76. UF SP MUN 61310 ZN 0158 SE 0034
77. UF SP MUN 61310 ZN 0158 SE 0024
78. UF SP MUN 61310 ZN 0158 SE 0001
79. UF SP MUN 61310 ZN 0384 SE 0204
80. UF SP MUN 61310 ZN 0384 SE 0202
81. UF SP MUN 61310 ZN 0384 SE 0196
82. UF SP MUN 61310 ZN 0384 SE 0162
83. UF SP MUN 61310 ZN 0384 SE 0028
84. UF SP MUN 61310 ZN 0384 SE 0242
85. UF SP MUN 61310 ZN 0384 SE 0240
86. UF SP MUN 61310 ZN 0384 SE 0228
87. UF SP MUN 61310 ZN 0384 SE 0226
88. UF SP MUN 61310 ZN 0384 SE 0274
89. UF SP MUN 61310 ZN 0384 SE 0273
90. UF SP MUN 61310 ZN 0384 SE 0097
91. UF SP MUN 61310 ZN 0384 SE 0207
92. UF SP MUN 61310 ZN 0384 SE 0120
93. UF SP MUN 61310 ZN 0384 SE 0149
94. UF SP MUN 61310 ZN 0384 SE 0101
95. UF SP MUN 61310 ZN 0384 SE 0016
96. UF SP MUN 61310 ZN 0384 SE 0187
97. UF SP MUN 61310 ZN 0384 SE 0179
98. UF SP MUN 61310 ZN 0384 SE 0044
99. UF SP MUN 61310 ZN 0384 SE 0042
100. UF SP MUN 61310 ZN 0384 SE 0078
101. UF SP MUN 61310 ZN 0384 SE 0137
102. UF SP MUN 61310 ZN 0384 SE 0067
103. UF SP MUN 61310 ZN 0384 SE 0017
104. UF SP MUN 61310 ZN 0384 SE 0008
105. UF SP MUN 61310 ZN 0384 SE 0263
106. UF SP MUN 61310 ZN 0384 SE 0250
107. UF SP MUN 63770 ZN 0329 SE 0381
108. UF SP MUN 63770 ZN 0426 SE 0204
109. UF SP MUN 63770 ZN 0426 SE 0067
110. UF SP MUN 66370 ZN 0161 SE 0134
111. UF SP MUN 68756 ZN 0093 SE 0110
112. UF SP MUN 70750 ZN 0283 SE 0354
113. UF SP MUN 71218 ZN 0340 SE 0199
114. UF SP MUN 72257 ZN 0034 SE 0041
115. UF SP MUN 72257 ZN 0034 SE 0139
116. UF SP MUN 62316 ZN 0319 SE 0013
117. UF SP MUN 63070 ZN 0140 SE 0001
118. UF SP MUN 63711 ZN 0119 SE 0162
119. UF SP MUN 69698 ZN 0108 SE 0124
120. UF SP MUN 69698 ZN 0305 SE 0348
121. UF SP MUN 72338 ZN 0242 SE 0069
122. UF SP MUN 61204 ZN 0368 SE 0001
123. UF SP MUN 62952 ZN 0035 SE 0128
124. UF SP MUN 63118 ZN 0206 SE 0118
125. UF SP MUN 63614 ZN 0227 SE 0401
126. UF SP MUN 63614 ZN 0227 SE 0569
127. UF SP MUN 65099 ZN 0132 SE 0216
128. UF SP MUN 66397 ZN 0066 SE 0031
129. UF SP MUN 66397 ZN 0066 SE 0326
130. UF SP MUN 68314 ZN 0323 SE 0099
131. UF SP MUN 70777 ZN 0166 SE 0089
132. UF SP MUN 70777 ZN 0269 SE 0107
133. UF SP MUN 71137 ZN 0131 SE 0084
134. UF SP MUN 71137 ZN 0131 SE 0244
135. UF SP MUN 71579 ZN 0324 SE 0298
136. UF SP MUN 71579 ZN 0416 SE 0107
137. UF SP MUN 61778 ZN 0335 SE 0001
138. UF SP MUN 62391 ZN 0369 SE 0107
139. UF SP MUN 62391 ZN 0369 SE 0157
140. UF SP MUN 62391 ZN 0369 SE 0073
141. UF SP MUN 64297 ZN 0192 SE 0119
142. UF SP MUN 64297 ZN 0192 SE 0121
143. UF SP MUN 64777 ZN 0393 SE 0264
144. UF SP MUN 65692 ZN 0058 SE 0235
145. UF SP MUN 65870 ZN 0061 SE 0032
146. UF SP MUN 66192 ZN 0281 SE 0290
147. UF SP MUN 69531 ZN 0172 SE 0146
148. UF SP MUN 69957 ZN 0163 SE 0047
149. UF SP MUN 70173 ZN 0186 SE 0147
150. UF SP MUN 70173 ZN 0186 SE 0241
151. UF SP MUN 70173 ZN 0186 SE 0050
152. UF SP MUN 70173 ZN 0186 SE 0273
153. UF SP MUN 70173 ZN 0186 SE 0149
154. UF SP MUN 70173 ZN 0186 SE 0124
155. UF SP MUN 70173 ZN 0186 SE 0085
156. UF SP MUN 70173 ZN 0186 SE 0067
157. UF SP MUN 70173 ZN 0186 SE 0364
158. UF SP MUN 70173 ZN 0186 SE 0254
159. UF SP MUN 70173 ZN 0186 SE 0073
160. UF SP MUN 70173 ZN 0186 SE 0053
161. UF SP MUN 70998 ZN 0127 SE 0616
162. UF SP MUN 71072 ZN 0001 SE 0388
163. UF SP MUN 71072 ZN 0001 SE 0057
164. UF SP MUN 71072 ZN 0002 SE 0330
165. UF SP MUN 71072 ZN 0002 SE 0339
166. UF SP MUN 71072 ZN 0002 SE 0337
167. UF SP MUN 71072 ZN 0002 SE 0268
168. UF SP MUN 71072 ZN 0002 SE 0280
169. UF SP MUN 71072 ZN 0002 SE 0042
170. UF SP MUN 71072 ZN 0002 SE 0212
171. UF SP MUN 71072 ZN 0002 SE 0059
172. UF SP MUN 71072 ZN 0002 SE 0055
173. UF SP MUN 71072 ZN 0002 SE 0149
174. UF SP MUN 71072 ZN 0004 SE 0089
175. UF SP MUN 71072 ZN 0005 SE 0206
176. UF SP MUN 71072 ZN 0005 SE 0070
177. UF SP MUN 71072 ZN 0005 SE 0151
178. UF SP MUN 71072 ZN 0005 SE 0025
179. UF SP MUN 71072 ZN 0005 SE 0214
180. UF SP MUN 71072 ZN 0006 SE 0259
181. UF SP MUN 71072 ZN 0006 SE 0135
182. UF SP MUN 71072 ZN 0006 SE 0169
183. UF SP MUN 71072 ZN 0006 SE 0166
184. UF SP MUN 71072 ZN 0246 SE 0205
185. UF SP MUN 71072 ZN 0246 SE 0247
186. UF SP MUN 71072 ZN 0246 SE 0244
187. UF SP MUN 71072 ZN 0247 SE 0333
188. UF SP MUN 71072 ZN 0247 SE 0332
189. UF SP MUN 71072 ZN 0247 SE 0331
190. UF SP MUN 71072 ZN 0247 SE 0563
191. UF SP MUN 71072 ZN 0249 SE 0627
192. UF SP MUN 71072 ZN 0251 SE 0183
193. UF SP MUN 71072 ZN 0251 SE 0104
194. UF SP MUN 71072 ZN 0251 SE 0318
195. UF SP MUN 71072 ZN 0251 SE 0211
196. UF SP MUN 71072 ZN 0254 SE 0470
197. UF SP MUN 71072 ZN 0256 SE 0306
198. UF SP MUN 71072 ZN 0256 SE 0230
199. UF SP MUN 71072 ZN 0257 SE 0173
200. UF SP MUN 71072 ZN 0258 SE 0405
201. UF SP MUN 71072 ZN 0258 SE 0158
202. UF SP MUN 71072 ZN 0258 SE 0529
203. UF SP MUN 71072 ZN 0258 SE 0513
204. UF SP MUN 71072 ZN 0259 SE 0063
205. UF SP MUN 71072 ZN 0260 SE 0012
206. UF SP MUN 71072 ZN 0260 SE 0013
207. UF SP MUN 71072 ZN 0280 SE 0836
208. UF SP MUN 71072 ZN 0320 SE 0451
209. UF SP MUN 71072 ZN 0320 SE 0394
210. UF SP MUN 71072 ZN 0325 SE 0646
211. UF SP MUN 71072 ZN 0325 SE 0168
212. UF SP MUN 71072 ZN 0327 SE 0377
213. UF SP MUN 71072 ZN 0346 SE 0085
214. UF SP MUN 71072 ZN 0346 SE 0645
215. UF SP MUN 71072 ZN 0346 SE 0053
216. UF SP MUN 71072 ZN 0348 SE 0160
217. UF SP MUN 71072 ZN 0349 SE 0193
218. UF SP MUN 71072 ZN 0350 SE 0444
219. UF SP MUN 71072 ZN 0352 SE 0193
220. UF SP MUN 71072 ZN 0352 SE 0053
221. UF SP MUN 71072 ZN 0372 SE 0727
222. UF SP MUN 71072 ZN 0374 SE 0453
223. UF SP MUN 71072 ZN 0374 SE 0533
224. UF SP MUN 71072 ZN 0374 SE 0233
225. UF SP MUN 71072 ZN 0398 SE 0176
226. UF SP MUN 71072 ZN 0405 SE 0301
227. UF SP MUN 71072 ZN 0413 SE 0304
228. UF SP MUN 71072 ZN 0413 SE 0313
229. UF SP MUN 71072 ZN 0418 SE 0297
230. UF SP MUN 71072 ZN 0421 SE 0092
231. UF SP MUN 71072 ZN 0422 SE 0392
232. UF SP MUN 71072 ZN 0422 SE 0036
233. UF SP MUN 71072 ZN 0422 SE 0356
234. UF SP MUN 71072 ZN 0422 SE 0160
235. UF SP MUN 72370 ZN 0345 SE 0198

### Seções com Códigos de Identificação de Urna Eletrônica repetidos

Existem 15 Códigos de Identificação de Urna Eletrônica que se repetem para duas ou mais Seções. Acredito que este código deveria ser único para cada Urna Eletrônica. A maioria dos códigos repetidos ocorre em seções no exterior.

#### Cód Identificador UE 01296316 - 26 ocorrências:

- ZZ (EXTERIOR), Município 29270 (ACCRA), Zona 0001, Seção 0003
- ZZ (EXTERIOR), Município 29297 (ANCARA), Zona 0001, Seção 0495
- ZZ (EXTERIOR), Município 29386 (BERLIM), Zona 0001, Seção 3057
- ZZ (EXTERIOR), Município 29645 (DÍLI), Zona 0001, Seção 0380
- ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1740
- ZZ (EXTERIOR), Município 29750 (HANÓI), Zona 0001, Seção 1703
- ZZ (EXTERIOR), Município 29947 (LIMA), Zona 0001, Seção 0154
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0591
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 1355
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1632
- ZZ (EXTERIOR), Município 30082 (MANILA), Zona 0001, Seção 0990
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0504
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 3040
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0165
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0247
- ZZ (EXTERIOR), Município 30228 (NOVA YORK), Zona 0001, Seção 0297
- ZZ (EXTERIOR), Município 30430 (RIO BRANCO), Zona 0001, Seção 0384
- ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1488
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1964
- ZZ (EXTERIOR), Município 30821 (WINDHOEK), Zona 0001, Seção 1524
- ZZ (EXTERIOR), Município 39063 (VANCOUVER), Zona 0001, Seção 3268
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0590
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0662
- ZZ (EXTERIOR), Município 98000 (GUATEMALA), Zona 0001, Seção 0123
- ZZ (EXTERIOR), Município 99155 (PUERTO IGUAZÚ), Zona 0001, Seção 1504
- ZZ (EXTERIOR), Município 99180 (NASSAU), Zona 0001, Seção 1228

#### Cód Identificador UE 01273645 - 15 ocorrências:

- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0051
- ZZ (EXTERIOR), Município 29807 (HOUSTON), Zona 0001, Seção 0940
- ZZ (EXTERIOR), Município 29882 (KUAITE), Zona 0001, Seção 0390
- ZZ (EXTERIOR), Município 29912 (LAGOS), Zona 0001, Seção 0150
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1640
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 1029
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0768
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0796
- ZZ (EXTERIOR), Município 30333 (PORTO PRÍNCIPE), Zona 0001, Seção 0353
- ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1858
- ZZ (EXTERIOR), Município 30341 (PORTO), Zona 0001, Seção 1886
- ZZ (EXTERIOR), Município 30562 (SYDNEY), Zona 0001, Seção 1375
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1821
- ZZ (EXTERIOR), Município 30783 (WASHINGTON), Zona 0001, Seção 0458
- ZZ (EXTERIOR), Município 39020 (ZAGREB), Zona 0001, Seção 1004

#### Cód Identificador UE 01274462 - 12 ocorrências:

- ZZ (EXTERIOR), Município 29378 (BELGRADO), Zona 0001, Seção 1735
- ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0072
- ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1909
- ZZ (EXTERIOR), Município 29777 (HAVANA), Zona 0001, Seção 0127
- ZZ (EXTERIOR), Município 29998 (LUANDA), Zona 0001, Seção 0199
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0024
- ZZ (EXTERIOR), Município 30597 (TEERÃ), Zona 0001, Seção 1182
- ZZ (EXTERIOR), Município 30619 (TEL AVIV), Zona 0001, Seção 0682
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1787
- ZZ (EXTERIOR), Município 30708 (TUNIS), Zona 0001, Seção 0444
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0545
- ZZ (EXTERIOR), Município 39322 (NICOSIA), Zona 0001, Seção 0490

#### Cód Identificador UE 01246419 - 12 ocorrências:

- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 0053
- ZZ (EXTERIOR), Município 29475 (CAIENA), Zona 0001, Seção 0071
- ZZ (EXTERIOR), Município 29700 (GENEBRA), Zona 0001, Seção 1912
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1671
- ZZ (EXTERIOR), Município 30066 (MADRI), Zona 0001, Seção 0026
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0399
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0426
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0168
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1820
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1966
- ZZ (EXTERIOR), Município 99287 (LUSACA), Zona 0001, Seção 1259
- ZZ (EXTERIOR), Município 99473 (BAREIN), Zona 0001, Seção 1327

#### Cód Identificador UE 01295943 - 11 ocorrências:

- ZZ (EXTERIOR), Município 29173 (KATMANDU), Zona 0001, Seção 0494
- ZZ (EXTERIOR), Município 29742 (HAMAMATSU), Zona 0001, Seção 1750
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0541
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1125
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1132
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 1164
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0248
- ZZ (EXTERIOR), Município 30287 (PARIS), Zona 0001, Seção 0758
- ZZ (EXTERIOR), Município 30767 (VIENA), Zona 0001, Seção 1967
- ZZ (EXTERIOR), Município 38962 (DAR ES SALAAM), Zona 0001, Seção 0558
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0719

#### Cód Identificador UE 01340042 - 9 ocorrências:

- ZZ (EXTERIOR), Município 29254 (ABIDJÃ), Zona 0001, Seção 0001
- ZZ (EXTERIOR), Município 29580 (CONCEPCIÓN), Zona 0001, Seção 0096
- ZZ (EXTERIOR), Município 29874 (KINSHASA), Zona 0001, Seção 0146
- ZZ (EXTERIOR), Município 29955 (LISBOA), Zona 0001, Seção 0540
- ZZ (EXTERIOR), Município 30112 (MIAMI), Zona 0001, Seção 0230
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0442
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1818
- ZZ (EXTERIOR), Município 30635 (TORONTO), Zona 0001, Seção 1031
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0643

#### Cód Identificador UE 01252874 - 8 ocorrências:

- ZZ (EXTERIOR), Município 29394 (BISSAU), Zona 0001, Seção 0028
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0394
- ZZ (EXTERIOR), Município 30163 (MOSCOU), Zona 0001, Seção 0647
- ZZ (EXTERIOR), Município 30171 (MUMBAI), Zona 0001, Seção 1340
- ZZ (EXTERIOR), Município 30252 (OTTAWA), Zona 0001, Seção 0767
- ZZ (EXTERIOR), Município 30627 (TÓQUIO), Zona 0001, Seção 1765
- ZZ (EXTERIOR), Município 39080 (ATLANTA), Zona 0001, Seção 0551
- ZZ (EXTERIOR), Município 39102 (MASCATE), Zona 0001, Seção 0712

#### Cód Identificador UE 01229330 - 6 ocorrências:

- ZZ (EXTERIOR), Município 29416 (BOSTON), Zona 0001, Seção 1041
- ZZ (EXTERIOR), Município 29971 (LONDRES), Zona 0001, Seção 1637
- ZZ (EXTERIOR), Município 30120 (MILÃO), Zona 0001, Seção 0432
- ZZ (EXTERIOR), Município 30198 (NAGÓIA), Zona 0001, Seção 0164
- ZZ (EXTERIOR), Município 30546 (SÓFIA), Zona 0001, Seção 1764
- ZZ (EXTERIOR), Município 30805 (WELLINGTON), Zona 0001, Seção 1690

#### Cód Identificador UE 01293146 - 2 ocorrências:

- MG (MINAS GERAIS), Município 46450 (ITAIPÉ), Zona 0196, Seção 0037
- MG (MINAS GERAIS), Município 49050 (NOVO CRUZEIRO), Zona 0196, Seção 0173

#### Cód Identificador UE 01268286 - 2 ocorrências:

- AM (AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0116
- AM (AMAZONAS), Município 02259 (COARI), Zona 0008, Seção 0174

#### Cód Identificador UE 01095313 - 2 ocorrências:

- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0524
- SP (SÃO PAULO), Município 71072 (SÃO PAULO), Zona 0389, Seção 0529

#### Cód Identificador UE 01612929 - 2 ocorrências:

- RN (RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0028
- RN (RIO GRANDE DO NORTE), Município 17434 (MACAU), Zona 0030, Seção 0077

#### Cód Identificador UE 01316810 - 2 ocorrências:

- MG (MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0041
- MG (MINAS GERAIS), Município 47872 (MANHUAÇU), Zona 0167, Seção 0282

#### Cód Identificador UE 01620697 - 2 ocorrências:

- RS (RIO GRANDE DO SUL), Município 87181 (NICOLAU VERGUEIRO), Zona 0062, Seção 0076
- RS (RIO GRANDE DO SUL), Município 87378 (MARAU), Zona 0062, Seção 0035

#### Cód Identificador UE 01149151 - 2 ocorrências:

- MT (MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0126
- MT (MATO GROSSO), Município 98191 (JUARA), Zona 0027, Seção 0136
