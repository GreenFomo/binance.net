using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class AllPricesRequest : Request {
        public override string BuildUrl() {
            return "v1/ticker/allPrices";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
