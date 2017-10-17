namespace BinanceDotNet.models {
    public class Ticker24hr {
        public decimal PriceChange { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal WeightedAvgPrice { get; set; }
        public decimal PrevClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal Volume { get; set;  }
        public long OpenTime { get; set; }
        public long CloseTime { get; set; }
        public long FirstId { get; set; }
        public long LastId { get; set; }
        public long Count { get; set; }

    }
}
