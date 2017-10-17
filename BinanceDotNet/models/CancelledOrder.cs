namespace BinanceDotNet.models {
    public class CancelledOrder {
        public string Symbol { get; set; }
        public string OrigClientOrderId { get; set; }
        public string OrderId { get; set; }
        public string ClientOrderId { get; set; }
    }
}
