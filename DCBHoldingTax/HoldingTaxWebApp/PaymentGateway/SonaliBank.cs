using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace HoldingTaxWebApp.PaymentGateway
{
    public static class SonaliBank
    {
        private const string _uriString = "";
        private static readonly HttpClient _apiClient = new HttpClient();

        static SonaliBank()
        {
            _apiClient.BaseAddress = new Uri(_uriString);
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}