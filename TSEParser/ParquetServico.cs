using ChoETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.IO;

namespace TSEParser
{
	public class ParquetServico
    {
        public void GerarParquetDoSQL(string connectionString, string caminhoparquet, List<string> UFs)
        {
            if (File.Exists(caminhoparquet))
                File.Delete(caminhoparquet);

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var w = new ChoParquetWriter(caminhoparquet)
                    .Configure(c => c.LiteParsing = true)
                    .Configure(c => c.RowGroupSize = 5000)
                    .NotifyAfter(100000)
                    .OnRowsWritten((o, e) => $"Linhas carregadas: {e.RowsWritten} <-- {DateTime.Now}".Print())
                    )
                {
                    foreach (var UF in UFs)
                    {
                        var cmdMunicipios = new SqlCommand($"SELECT Codigo FROM Municipio WHERE UFSigla = '{UF}' ORDER BY Codigo", conn);
                        var drMunicipios = cmdMunicipios.ExecuteReader();

                        List<int> lstMunicipios = new List<int>();
                        while (drMunicipios.Read())
                        {
                            lstMunicipios.Add(drMunicipios["Codigo"].ToInt());
                        }
                        drMunicipios.Close();
                        cmdMunicipios.Dispose();

                        int muAtual = 0;
                        foreach (var codMunicipio in lstMunicipios)
                        {
                            muAtual++;
                            decimal percentual = (muAtual.ToDecimal() / lstMunicipios.Count().ToDecimal()) * 100;
                            Console.WriteLine($"{percentual:N2}% - Carregando UF {UF}, Municipio {muAtual}/{lstMunicipios.Count()}");

                            string strSQL = @$"SELECT		UF.Sigla as UFSigla,
			UF.Nome as UFNome,
			M.Codigo as CodMunicipio,
			M.Nome as NomeMunicipio, 
			VS.CodigoZonaEleitoral, 
			VS.CodigoSecao, 
			S.CodigoLocalVotacao,
			S.EleitoresAptos,
			S.Comparecimento,
			S.EleitoresFaltosos,
			S.HabilitadosPorAnoNascimento,
			S.CodigoIdentificacaoUrnaEletronica,
			S.AberturaUrnaEletronica,
			S.FechamentoUrnaEletronica,
			S.DF_EleitoresAptos,
			S.DF_VotosNominais,
			S.DF_VotosLegenda,
			S.DF_Brancos,
			S.DF_Nulos,
			S.DF_Total,
			S.DE_EleitoresAptos,
			S.DE_VotosNominais,
			S.DE_VotosLegenda,
			S.DE_Brancos,
			S.DE_Nulos,
			S.DE_Total,
			S.SE_EleitoresAptos,
			S.SE_VotosNominais,
			S.SE_Brancos,
			S.SE_Nulos,
			S.SE_Total,
			S.GO_EleitoresAptos,
			S.GO_VotosNominais,
			S.GO_Brancos,
			S.GO_Nulos,
			S.GO_Total,
			S.PR_EleitoresAptos,
			S.PR_VotosNominais,
			S.PR_Brancos,
			S.PR_Nulos,
			S.PR_Total,			
			VS.Cargo, 
			VS.VotoLegenda, 
			VS.NumeroCandidato,
			C.Nome,
			VS.QtdVotos,
			P.Numero as NumPartido,
			P.Nome as NomePartido
FROM		VotosSecao VS with (NOLOCK) 
INNER JOIN	SecaoEleitoral S with (NOLOCK) ON S.MunicipioCodigo = VS.MunicipioCodigo AND S.CodigoZonaEleitoral = VS.CodigoZonaEleitoral AND S.CodigoSecao = VS.CodigoSecao
INNER JOIN	Municipio M with (NOLOCK) ON M.Codigo = VS.MunicipioCodigo
INNER JOIN	UnidadeFederativa UF with (NOLOCK) ON UF.Sigla = M.UFSigla
INNER JOIN	Candidato C with (NOLOCK) ON C.Cargo = VS.Cargo AND C.NumeroCandidato = VS.NumeroCandidato AND C.UFSigla = CASE WHEN C.Cargo = 5 THEN 'BR' ELSE M.UFSigla END
INNER JOIN	Partido P with (NOLOCK) ON P.Numero = CONVERT(tinyint, LEFT(CONVERT(varchar(10), VS.NumeroCandidato), 2))
WHERE		VS.MunicipioCodigo = {codMunicipio}
ORDER BY	VS.CodigoZonaEleitoral,
			VS.CodigoSecao,
			VS.Cargo,
			VS.VotoLegenda,
			VS.NumeroCandidato";

                            var cmd = new SqlCommand(strSQL, conn);

                            var dr = cmd.ExecuteReader();

                            w.Write(dr);

                            dr.Close();
                        }
                    }
                }
            }
        }
    }
}
