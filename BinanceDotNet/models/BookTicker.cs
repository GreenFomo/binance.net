namespace BinanceDotNet.models {
    public class BookTicker {
        public string Symbol { get; set; }
        public decimal BidPrice { get; set; }
        public decimal BidQty { get; set; }
        public decimal AskPrice { get; set; }
        public decimal AskQty { get; set; }
    }
}
