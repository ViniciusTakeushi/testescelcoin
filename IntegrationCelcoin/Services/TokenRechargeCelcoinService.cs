using IntegrationCelcoin.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace IntegrationCelcoin.Services
{
    public class TokenRechargeCelcoinService
    {
        private string _clientId = "";
        private string _clientSecret = "";
        private string _grantType = "client_credentials";

        public Dictionary<bool, string> GetToken()
        {
            var urlRequest = "https://sandbox.openfinance.celcoin.dev/v5/token";

            var resultToken = new Dictionary<bool, string>();

            var client = new RestClient(urlRequest);

            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddParameter("client_id", _clientId);
            request.AddParameter("grant_type", _grantType);
            request.AddParameter("client_secret", _clientSecret);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResponse = JsonConvert.DeserializeObject<ResultRechargeTokenCelcoinModel>(response.Content);

                resultToken.Add(true, objResponse.access_token);
            }
            else
                resultToken.Add(false, response.Content);

            return resultToken;
        }
    }
}
