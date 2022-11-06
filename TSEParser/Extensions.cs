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

            if(value.Days > 0)
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
                timeString = value.Hours.ToString() + ":" + value.Minutes.LeadZeros(2) + ":" + value.Seconds.LeadZeros(2);

            return timeString;
        }

    }
}
