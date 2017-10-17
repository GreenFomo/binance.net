using System;
using System.Collections.Generic;

namespace BinanceDotNet.models.requests {
    public class PlaceOrderRequest : PlaceOrderTestRequest {
        
        public override string BuildUrl() {
            return "v3/order";
        }

    }
}
