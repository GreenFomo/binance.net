namespace BinanceDotNet.models.requests {
    public class AggregateTradeRequest : RequestWithSymbol {

        public override string BuildUrl() {
            return $"v1/aggTrades?symbol={Symbol}";
        }

        public override bool IsValid() {
            return ValidateRequired("Symbol");
        }
    }
}
