using System;

namespace IntegrationCelcoin.Models
{
    public class ResultRechargeCreateCelcoinReceipt
    {
        public string receiptData { get; set; }
        public string receiptformatted { get; set; }
    }

    public class ResultRechargeCreateCelcoinModel
    {
        public int NSUnameProvider { get; set; }
        public int authentication { get; set; }
        public object authenticationAPI { get; set; }
        public ResultRechargeCreateCelcoinReceipt receipt { get; set; }
        public DateTime settleDate { get; set; }
        public DateTime createDate { get; set; }
        public int transactionId { get; set; }
        public object Urlreceipt { get; set; }
        public string errorCode { get; set; }
        public object message { get; set; }
        public int status { get; set; }
    }
}
