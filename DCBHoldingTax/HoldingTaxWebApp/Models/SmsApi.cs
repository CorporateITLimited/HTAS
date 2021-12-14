using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace HoldingTaxWebApp.Models
{
    public static class SmsApi
    {
        public static readonly string api_key = "1M6i058gFG2BLYGXLx4I57D7u2eLOE0n9HFu36w7";

        public static string SendSms(string msg, string to)
        {
            var url = "https://api.sms.net.bd/sendsms";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            var data = "api_key="+ api_key.Trim() +"& msg=" + msg.Trim() + "&to=" + to.Trim();

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //Console.WriteLine(result);
            }
            var c = httpResponse.StatusCode;
            return "";
        }

    }
}