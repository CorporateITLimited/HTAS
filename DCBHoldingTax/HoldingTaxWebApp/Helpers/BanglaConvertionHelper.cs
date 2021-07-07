using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Helpers
{
    public class BanglaConvertionHelper
    {
        public static string ReplaceDecimalValue(decimal? param)
        {
            if (param != null)
            {
                if (param > 0)
                {
                    decimal? fractionValue = param - Math.Truncate(param ?? default(decimal));
                    if (fractionValue > 0)
                    {
                        return param.ToString().Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                                               .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                                               .Replace("8", "৮").Replace("9", "৯");
                    }
                    else
                    {
                        return param.ToString().Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩")
                                               .Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭")
                                               .Replace("8", "৮").Replace("9", "৯") + ".০০";
                    }
                }
                else
                {
                    return "০.০০";
                }
            }
            else
                return "";

        }

        public static string ReplaceIntegerValue(int? param)
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

    }
}