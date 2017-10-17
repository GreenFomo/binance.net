﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class GetAllOrdersRequest : SignedRequest {

        public override Dictionary<string, string> BuildQueryString() {
            return new Dictionary<string, string>() {
                { "symbol", Symbol }
            };
        }

        public override string BuildUrl() {
            return "v3/allOrders";
        }

        public override bool IsValid() {
            return ValidateRequireds(new List<string>() { "Symbol" });
        }
    }
}