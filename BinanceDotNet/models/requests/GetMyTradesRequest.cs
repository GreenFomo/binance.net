namespace BinanceDotNet.models.requests {
    public class GetMyTradesRequest : OrdersRequest {
        public override string BuildUrl() {
            return "v3/myTrades";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
