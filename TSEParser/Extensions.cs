using System;
using System.Collections.Generic;
using System.Text;

namespace TSEParser
{
    public static class Extensions
    {
        public static short ToShort(this string value)
        {
            return short.Parse(value);
        }
        public static short ToShort(this object value)
        {
            return Convert.ToInt16(value);
        }
        public static short ToShort(this int value)
        {
            return Convert.ToInt16(value);
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }
        public static int ToInt(this object value)
        {
            return Convert.ToInt32(value);
        }
        public static int ToInt(this byte value)
        {
            return Convert.ToInt32(value);
        }

        public static byte ToByte(this int value)
        {
            return Convert.ToByte(value);
        }
        public static byte ToByte(this object value)
        {
            return Convert.ToByte(value);
        }
        public static decimal ToDecimal(this int value)
        {
            return Convert.ToDecimal(value);
        }

        public static string LeadZeros(this int value, int qtdDigits)
        {
            return value.ToString().PadLeft(qtdDigits, '0');
        }

        public static string SimOuNao(this bool value)
        {
            return value ? "Sim" : "Não";
        }
        public static DateTimeOffset ToDateTimeOffset(this DateTime value)
        {
            DateTimeOffset dto = value;
            return dto;
        }
        public static string NomeCargo(this Cargos value)
        {
            switch (value)
            {
                case Cargos.DeputadoFederal:
                    return "Dep Federal";
                    break;
                case Cargos.DeputadoEstadual:
                    return "Dep Est/Dist";
                    break;
                case Cargos.Senador:
                    return "Senador";
                    break;
                case Cargos.Governador:
                    return "Governador";
                    break;
                case Cargos.Presidente:
                    return "Presidente";
                    break;
                default:
                    return "[Cargo Inválido]";
                    break;
            }


        }

        public static string TempoPorExtenso(this TimeSpan value)
        {
            string timeString = "";

            if (value.Days > 0)
                timeString = value.Days.ToString() + " dia" + (value.Days > 1 ? "s" : "") + ", "
                    + value.Hours.ToString() + " hora" + (value.Hours > 1 ? "s" : "") + ", "
                    + value.Minutes.ToString() + " minuto" + (value.Minutes > 1 ? "s" : "") + ", "
                    + value.Seconds.ToString() + " segundo" + (value.Seconds > 1 ? "s" : "") + "";
            if (value.Hours > 0)
                timeString = value.Hours.ToString() + " hora" + (value.Hours > 1 ? "s" : "") + ", "
                    + value.Minutes.ToString() + " minuto" + (value.Minutes > 1 ? "s" : "") + ", "
                    + value.Seconds.ToString() + " segundo" + (value.Seconds > 1 ? "s" : "") + "";
            else if (value.Minutes > 0)
                timeString = value.Minutes.ToString() + " minuto" + (value.Minutes > 1 ? "s" : "") + ", "
                    + value.Seconds.ToString() + " segundo" + (value.Seconds > 1 ? "s" : "") + "";
            else
                timeString = value.Seconds.ToString() + " segundo" + (value.Seconds > 1 ? "s" : "") + "";

            return timeString;
        }

        public static string TempoResumido(this TimeSpan value)
        {
            string timeString = "";

            if (value.Days > 0)
                timeString = value.Days.ToString() + "D " + value.Hours.LeadZeros(2) + ":" + value.Minutes.LeadZeros(2) + ":" + value.Seconds.LeadZeros(2);
            else
                timeString = value.Hours.LeadZeros(2) + ":" + value.Minutes.LeadZeros(2) + ":" + value.Seconds.LeadZeros(2);

            return timeString;
        }

        public static string DataHoraPTBR(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static byte IdRegiao(this string value)
        {
            /*
            Dictionary<string, byte> UFsRegioes = new Dictionary<string, byte>();
            UFsRegioes.Add("PR", 1);
            UFsRegioes.Add("RS", 1);
            UFsRegioes.Add("SC", 1);
            UFsRegioes.Add("ES", 2);
            UFsRegioes.Add("MG", 2);
            UFsRegioes.Add("RJ", 2);
            UFsRegioes.Add("SP", 2);
            UFsRegioes.Add("DF", 3);
            UFsRegioes.Add("GO", 3);
            UFsRegioes.Add("MS", 3);
            UFsRegioes.Add("MT", 3);
            UFsRegioes.Add("AC", 4);
            UFsRegioes.Add("AM", 4);
            UFsRegioes.Add("AP", 4);
            UFsRegioes.Add("PA", 4);
            UFsRegioes.Add("RO", 4);
            UFsRegioes.Add("RR", 4);
            UFsRegioes.Add("TO", 4);
            UFsRegioes.Add("AL", 5);
            UFsRegioes.Add("BA", 5);
            UFsRegioes.Add("CE", 5);
            UFsRegioes.Add("MA", 5);
            UFsRegioes.Add("PB", 5);
            UFsRegioes.Add("PE", 5);
            UFsRegioes.Add("PI", 5);
            UFsRegioes.Add("RN", 5);
            UFsRegioes.Add("SE", 5);
            UFsRegioes.Add("ZZ", 6);
            */

            if (value.ToLower() == "pr") return 1;
            else if (value.ToLower() == "rs") return 1;
            else if (value.ToLower() == "sc") return 1;
            else if (value.ToLower() == "es") return 2;
            else if (value.ToLower() == "mg") return 2;
            else if (value.ToLower() == "rj") return 2;
            else if (value.ToLower() == "sp") return 2;
            else if (value.ToLower() == "df") return 3;
            else if (value.ToLower() == "go") return 3;
            else if (value.ToLower() == "ms") return 3;
            else if (value.ToLower() == "mt") return 3;
            else if (value.ToLower() == "ac") return 4;
            else if (value.ToLower() == "am") return 4;
            else if (value.ToLower() == "ap") return 4;
            else if (value.ToLower() == "pa") return 4;
            else if (value.ToLower() == "ro") return 4;
            else if (value.ToLower() == "rr") return 4;
            else if (value.ToLower() == "to") return 4;
            else if (value.ToLower() == "al") return 5;
            else if (value.ToLower() == "ba") return 5;
            else if (value.ToLower() == "ce") return 5;
            else if (value.ToLower() == "ma") return 5;
            else if (value.ToLower() == "pb") return 5;
            else if (value.ToLower() == "pe") return 5;
            else if (value.ToLower() == "pi") return 5;
            else if (value.ToLower() == "rn") return 5;
            else if (value.ToLower() == "se") return 5;
            else if (value.ToLower() == "zz") return 6;
            else return 0;
        }
    }
}
