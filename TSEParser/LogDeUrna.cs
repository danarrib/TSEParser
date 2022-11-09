using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TSEParser
{
    public class LogDeUrna
    {
        public List<string> TextoLog { get; set; }
        public string NomeArquivoLog { get; set; }
        public string NomeArquivoJez { get; set; }
        public DateTime DataModificacao { get; set; }

        public LogDeUrna(string _nomeArquivoLog, string _nomeArquivoJez, DateTime _dataModificacao)
        {
            TextoLog = new List<string>();
            NomeArquivoLog = _nomeArquivoLog;
            NomeArquivoJez = _nomeArquivoJez;
            DataModificacao = _dataModificacao;
        }

        public DateTime DataHoraPrimeiraLinha { 
            get {
                // Varrer o arquivo do início ao fim e obter a primeira data disponível
                int limite = TextoLog.Count;
                if (limite > 10)
                    limite = 10;

                for (int i = 0; i < limite; i++)
                {
                    var strData = TextoLog[i].Substring(0, 19);
                    var deuCerto = DateTime.TryParseExact(strData, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataRetorno);
                    if (deuCerto)
                    {
                        return dataRetorno;
                    }
                }

                return DateTime.MinValue;
            }
        }
        public DateTime DataHoraUltimaLinha {
            get
            {
                // Varrer o arquivo 10 linhas antes do final até o final e obter a última data disponível
                var ultimaData = DateTime.MinValue;
                for (int i = TextoLog.Count - 10; i < TextoLog.Count; i++)
                {
                    if (TextoLog[i].Length < 19)
                        continue;

                    var strData = TextoLog[i].Substring(0, 19);
                    var deuCerto = DateTime.TryParseExact(strData, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataRetorno);
                    if (deuCerto)
                    {
                        ultimaData = dataRetorno;
                    }
                }

                return ultimaData;
            }
        }


    }
}
