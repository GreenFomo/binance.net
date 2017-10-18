using System.Collections.Generic;

namespace BinanceDotNet.models.requests {
    public class GetAllOrdersRequest : OrdersRequest {
        public override string BuildUrl() {
            return "v3/allOrders";
        }

        public override bool IsValid() {
            return ValidateRequireds(new List<string>() { "Symbol" }) && (Limit == null || Limit < 500);
        }
    }
}
