using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Helpers
{
    public class BanglaConvertionHelper
    {
        public static string DecimalValueEnglish2Bangla(decimal? param)
        {
            if (param != null)
            {
                if (param > 0)
                {
                    //decimal? fractionValue = param - Math.Truncate(param ?? default(decimal));
                    //if (fractionValue > 0)
                    //{
                    return param.ToString().Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                                           .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                                           .Replace("8", "৮").Replace("9", "৯");
                    //}
                    //else
                    //{
                    //    return param.ToString().Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                    //                           .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                    //                           .Replace("8", "৮").Replace("9", "৯") + ".০০";
                    //}
                }
                else
                {
                    return "০.০০";
                }
            }
            else
                return "";

        }

        public static string IntegerValueEnglish2Bangla(int? param)
        {
            if (param != null)
            {
                if (param > 0)
                    return param.ToString().Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                                           .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                                           .Replace("8", "৮").Replace("9", "৯");
                else
                    return "০";
            }
            else
                return "";

        }

        public static decimal DecimalValueBangla2English(string param)
        {
            string replacedValue = param.Replace("০", "0").Replace("১", "1").Replace("২", "2").Replace("৩", "3")
                                              .Replace("৪", "4").Replace("৫", "5").Replace("৬", "6").Replace("৭", "7")
                                              .Replace("৮", "8").Replace("৯", "9");
            decimal.TryParse(replacedValue, out decimal val);
            return val;
        }

        public static int IntegerValueBangla2English(string param)
        {
            string replacedValue = param.Replace("০", "0").Replace("১", "1").Replace("২", "2").Replace("৩", "3")
                                              .Replace("৪", "4").Replace("৫", "5").Replace("৬", "6").Replace("৭", "7")
                                              .Replace("৮", "8").Replace("৯", "9");

            int.TryParse(replacedValue, out int val);
            return val;
        }


        public static bool IsValidEnglishStringValue(string str)
        {
            str = str.Replace(".", string.Empty);
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static bool IsValidBanglaStringValue(string str)
        {
            str = str.Replace(".", string.Empty);
            var newData = string.Concat(str.Select(c => (char)('0' + c - '\u09E6')));
            foreach (char c in newData)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }


        public static string StringEnglish2StringBanglaDate(string str)
        {
            return str.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                                           .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                                           .Replace("8", "৮").Replace("9", "৯");
        }
    }
}