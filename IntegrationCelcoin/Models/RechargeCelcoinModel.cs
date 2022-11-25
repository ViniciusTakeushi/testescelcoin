namespace IntegrationCelcoin.Models
{
    public class RechargeCelcoinPhone
    {
        public int stateCode { get; set; }
        public int countryCode { get; set; }
        public int number { get; set; }
    }

    public class RechargeCelcoinTopupData
    {
        public double value { get; set; }
    }

    public class RechargeCelcoinModel
    {
        public string externalTerminal { get; set; }
        public string externalNsu { get; set; }
        public RechargeCelcoinTopupData topupData { get; set; }
        public string cpfCnpj { get; set; }
        public string signerCode { get; set; }
        public int providerId { get; set; }
        public RechargeCelcoinPhone phone { get; set; }
    }
}
