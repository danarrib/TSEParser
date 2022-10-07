using System;
using System.Collections.Generic;
using System.Text;

namespace TSEParser.CrawlerModels
{
    public class UFConfig
    {
        public string dg { get; set; } // Data da apuração ? "02/10/2022"
        public string hg { get; set; } // Hora da apuração ? "19:27:49"
        public string f { get; set; } // Não sei ? "0"
        public string cdp { get; set; } // Não sei ? "406"
        public List<ABR> abr { get; set; } // Não sei o que significa ABR, mas a lista de municípios está dentro
    }

    public class ABR
    {
        public string cd { get; set; } // Código da UF "AP"
        public string ds { get; set; } // Descrição da UF "AMAPÁ"
        public List<Municipio> mu { get; set; } // Lista de municípios
    }

    public class Municipio
    {
        public string cd { get; set; } // Código do município "06050"
        public string nm { get; set; } // Nome do Município
        public List<ZonaEleitoral> zon { get; set; } // Zonas eleitorais do Município

    }

    public class ZonaEleitoral
    {
        public string cd { get; set; } // Código da zona eleitoral "0002"
        public List<SecaoEleitoral> sec { get; set; } // Seção eleitoral
    }

    public class SecaoEleitoral
    {
        public string ns { get; set; } // Número da Seção eleitoral "0001"
        public string nsp { get; set; } // Número da Seção eleitoral "0001" (repetido porque?)
    }

    public class BoletimUrna
    {
        public string dg { get; set; } // Data da apuração? "02/10/2022"
        public string hg { get; set; } // Hora da apuração? "23:27:25"
        public string f { get; set; } // Não sei? "0"
        public string st { get; set; } // Situação? "Totalizada"
        public string ds { get; set; } // Não sei? ""
        public List<BoletimUrnaHash> hashes { get; set; } // Lista de Hashes
    }

    public class BoletimUrnaHash
    {
        public string hash { get; set; } // Hash "534f753676357056516e4a42384e376c77544b32533257562b794a2d4968375a6e7a654f504b6746762b493d"
        public string dr { get; set; } // Data do hash? "02/10/2022"
        public string hg { get; set; } // Hora do hash? "19:20:08"
        public string st { get; set; } // Situação? "Totalizado"
        public string ds { get; set; } // Não sei? ""
        public List<string> nmarq { get; set; } // Lista dos nomes dos arquivos
    }

}
