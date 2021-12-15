using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace HoldingTaxWebApp.Models
{
    public static class SmsApi
    {
        public static readonly string api_key = "1qGK8Rkq7VVsSENN1ejGZvlDODukl0q62eMujHjs";

        public static string SendSms(string msg, string to)
        {
            var url = "https://api.sms.net.bd/sendsms";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            //string api_key = "1qGK8Rkq7VVsSENN1ejGZvlDODukl0q62eMujHjs";
            //string msg = "Test Message . . . .";
            //string to = "8801971090707";

            //Prepare you post parameters  
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("api_key={0}", api_key);
            sbPostData.AppendFormat("&msg={0}", msg);
            sbPostData.AppendFormat("&to={0}", to);

            //Prepare and Add URL Encoded data  
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method  
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = data.Length;
            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            //Get the response  
            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            //Close the response  
            reader.Close();

            response.Close();

            return "";

            //var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpRequest.Method = "POST";

            //var data = "api_key="+ api_key.Trim() +"&msg=" + msg.Trim() + "&to=" + to.Trim();

            //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            //{
            //    streamWriter.Write(data);
            //}

            //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //    //Console.WriteLine(result);
            //}
            //var c = httpResponse.StatusCode;
            //return "";
        }

    }
}