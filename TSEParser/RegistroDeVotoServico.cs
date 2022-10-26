using org.bn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TSERDV;

namespace TSEParser
{
    public class RegistroDeVotoServico
    {

        public EntidadeResultadoRDV DecodificarRegistroVoto(string arquivoRDV)
        {
            IDecoder decoder = CoderFactory.getInstance().newDecoder("DER");
            EntidadeResultadoRDV retorno = null;

            var arquivoRDVBytes = File.ReadAllBytes(arquivoRDV);
            using (MemoryStream memoryStream = new MemoryStream(arquivoRDVBytes))
            {
                retorno = decoder.decode<EntidadeResultadoRDV>(memoryStream);
                var jsonBU = JsonSerializer.Serialize(retorno, new JsonSerializerOptions()
                {
                    MaxDepth = 0,
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true
                });
            }

            return retorno;


        }

    }
}
