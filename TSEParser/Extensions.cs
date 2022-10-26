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

        public static string LeadZeros(this int value, int qtdZeroes)
        {
            return value.ToString().PadLeft(qtdZeroes, '0');
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

    }
}
