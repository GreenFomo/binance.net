using System.Collections.Generic;

namespace BinanceDotNet.models.requests {
    public abstract class OrdersRequest : SignedRequest {
        public long? OrderId { get; set; }
        public int? Limit { get; set; }
        public long? RecvWindow { get; set; }

        public override Dictionary<string, string> BuildQueryParams() {
            var qp = new Dictionary<string, string>() {
                { "symbol", Symbol }
            };
            if (OrderId.HasValue)
                qp.Add("orderId", OrderId.ToString());

            if (Limit.HasValue)
                qp.Add("limit", Limit.ToString());

            if (RecvWindow.HasValue)
                qp.Add("recvWindow", RecvWindow.ToString());

            return qp;
        }
    }
}
