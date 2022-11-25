using System.Collections.Generic;

namespace IntegrationCelcoin.Models
{
    public class ResultSearchRechargeProviderCelcoinProvider
    {
        public int category { get; set; }
        public string name { get; set; }
        public int providerId { get; set; }
        public List<object> RegionaisnameProvider { get; set; }
        public int TipoRecarganameProvider { get; set; }
        public double maxValue { get; set; }
        public double minValue { get; set; }
    }

    public class ResultSearchRechargeProviderCelcoinModel
    {
        public List<ResultSearchRechargeProviderCelcoinProvider> providers { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }
}
