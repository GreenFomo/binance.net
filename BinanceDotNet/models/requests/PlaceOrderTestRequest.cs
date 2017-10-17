using BinanceDotNet.models.enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using static BinanceDotNet.extensions.EnumHelper;

namespace BinanceDotNet.models.requests {

    public class PlaceOrderTestRequest : SignedRequest {
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public override Dictionary<string, string> BuildQueryString() {
            return new Dictionary<string, string>() {
                { "symbol", Symbol },
                { "side", Side.GetValue() },
                { "recvWindow", "6500" },
                { "type", Type.GetValue() },
                { "timeInForce", TimeInForce.GetValue() },
                { "quantity", Quantity.ToString() },
                { "price", Price.ToString() }
            };
        }

        public override string BuildUrl() {
            return "v3/order/test";
        }

        public override bool IsValid() {
            return ValidateRequireds(new List<string>() { "Symbol", "Side", "Type", "TimeInForce", "Quantity", "Price" });
        }

        public PlaceOrderTestRequest() {
            Method = HttpMethod.Post;
        }
    }
}
