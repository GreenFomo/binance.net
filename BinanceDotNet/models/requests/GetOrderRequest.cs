using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class GetOrderRequest : SignedRequest {
        public long? OrderId { get; set; }
        public string OrigClientOrderId { get; set; }

        public override Dictionary<string, string> BuildQueryString() {
            var dict = new Dictionary<string, string>() {
                { "symbol", Symbol }
            };

            if (OrderId.HasValue) {
                dict["orderId"] = OrderId.ToString();
            } else if (OrigClientOrderId != null) {
                dict["origClientOrderId"] = OrigClientOrderId.ToString();
            }

            return dict;
        }

        public override string BuildUrl() {
            return "v3/order";
        }

        public override bool IsValid() {
            return ValidateRequired("Symbol") && (ValidateRequired("OrderId") || ValidateRequired("OrigClientOrderId"));
        }
    }
}
