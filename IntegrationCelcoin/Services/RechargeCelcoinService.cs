using IntegrationCelcoin.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace IntegrationCelcoin.Services
{
    public class RechargeCelcoinService
    {
        private string _accessToken = "";

        public RechargeCelcoinService(string accessToken)
        {
            _accessToken = accessToken;
        }

        public Dictionary<string, List<ResultSearchRechargeProviderCelcoinProvider>> GetRechargeProviders(int type, int category)
        {
            var resultSearchProvider = new Dictionary<string, List<ResultSearchRechargeProviderCelcoinProvider>>();

            var urlRequest = $"https://sandbox.openfinance.celcoin.dev/v5/transactions/topups/providers?stateCode=89&type={type}&category={category}";

            var client = new RestClient();

            var request = new RestRequest(urlRequest);
            request.Method = Method.Get;

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResponse = JsonConvert.DeserializeObject<ResultSearchRechargeProviderCelcoinModel>(response.Content);

                if (objResponse.status == 0)
                    resultSearchProvider.Add("", objResponse.providers);
                else
                    resultSearchProvider.Add($"Status: {objResponse.status} - Erro: {objResponse.message}", null);
            }
            else
                resultSearchProvider.Add($"Erro: {response.StatusCode} - {response.StatusDescription}. Descrição: {response.Content}", null);

            return resultSearchProvider;
        }

        public Dictionary<string, List<ResultSearchRechargeProviderValueCelcoinValue>> GetRechargeProvidersValue(int idProvider)
        {
            var resultSearchProvider = new Dictionary<string, List<ResultSearchRechargeProviderValueCelcoinValue>>();

            var urlRequest = $"https://sandbox.openfinance.celcoin.dev/v5/transactions/topups/provider-values?stateCode=11&providerId={idProvider}";

            var client = new RestClient();

            var request = new RestRequest(urlRequest);
            request.Method = Method.Get;

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResponse = JsonConvert.DeserializeObject<ResultSearchRechargeProviderValueCelcoinModel>(response.Content);

                if (objResponse.status == 0)
                    resultSearchProvider.Add("", objResponse.value);
                else
                    resultSearchProvider.Add($"Status: {objResponse.status} - Erro: {objResponse.message}", null);
            }
            else
                resultSearchProvider.Add($"Erro: {response.StatusCode} - {response.StatusDescription}. Descrição: {response.Content}", null);

            return resultSearchProvider;
        }

        public Dictionary<string, ResultRechargeCreateCelcoinModel> CreateRecharge(RechargeCelcoinModel rechargeCelcoin)
        {
            var resultRecharge = new Dictionary<string, ResultRechargeCreateCelcoinModel>();

            var urlRequest = $"https://sandbox.openfinance.celcoin.dev/v5/transactions/topups";

            var client = new RestClient();

            var request = new RestRequest(urlRequest);
            request.Method = Method.Post;
            request.AddJsonBody(rechargeCelcoin);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResponse = JsonConvert.DeserializeObject<ResultRechargeCreateCelcoinModel>(response.Content);

                if (objResponse.status == 0)
                    resultRecharge.Add("", objResponse);
                else
                    resultRecharge.Add($"Status: {objResponse.status} - Erro: {objResponse.message}", null);
            }
            else
                resultRecharge.Add($"Erro: {response.StatusCode} - {response.StatusDescription}. Descrição: {response.Content}", null);

            return resultRecharge;
        }

        public Dictionary<bool, string> ConfirmRecharge(long transactionId, RechargeConfimModel rechargeConfim)
        {
            var resultConfirmRecharge = new Dictionary<bool, string>();

            var urlRequest = $"https://sandbox.openfinance.celcoin.dev/v5/transactions/topups/{transactionId}/capture";

            var client = new RestClient();

            var request = new RestRequest(urlRequest);
            request.Method = Method.Put;
            request.AddJsonBody(rechargeConfim);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResponse = JsonConvert.DeserializeObject<ResultRechargeCreateCelcoinModel>(response.Content);

                if (objResponse.status == 0)
                    resultConfirmRecharge.Add(true, "");
                else
                    resultConfirmRecharge.Add(false, $"Status: {objResponse.status} - Erro: {objResponse.message}");
            }
            else
                resultConfirmRecharge.Add(false, $"Erro: {response.StatusCode} - {response.StatusDescription}. Descrição: {response.Content}");

            return resultConfirmRecharge;
        }
    }
}
