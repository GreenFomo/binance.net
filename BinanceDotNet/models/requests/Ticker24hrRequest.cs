using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class Ticker24hrRequest : RequestWithSymbol {
        public override string BuildUrl() {
            return $"v1/ticker/24hr?symbol={Symbol}";
        }

        public override bool IsValid() {
            return ValidateRequired("Symbol");
        }
    }
}
