using System.Collections.Generic;

namespace BinanceDotNet.models.requests {
    public class GetOpenOrdersRequest : SignedRequest {

        public override Dictionary<string, string> BuildQueryParams() {
            return new Dictionary<string, string>() {
                { "symbol", Symbol }
            };
        }

        public override string BuildUrl() {
            return "v3/openOrders";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
