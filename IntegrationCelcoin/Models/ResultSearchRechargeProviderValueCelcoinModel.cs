using System.Collections.Generic;

namespace IntegrationCelcoin.Models
{
    public class ResultSearchRechargeProviderValueCelcoinValue
    {
        public object properties { get; set; }
        public int code { get; set; }
        public double cost { get; set; }
        public string detail { get; set; }
        public string productName { get; set; }
        public int checkSum { get; set; }
        public int dueProduct { get; set; }
        public double valueBonus { get; set; }
        public double maxValue { get; set; }
        public double minValue { get; set; }
    }


    public class ResultSearchRechargeProviderValueCelcoinModel
    {
        public List<ResultSearchRechargeProviderValueCelcoinValue> value { get; set; }
        public object externalNsuQuery { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }
}
