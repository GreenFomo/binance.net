namespace BinanceDotNet.models.requests {
    public abstract class RequestWithSymbol : Request {
        public string Symbol { get; set; }
    }
}
