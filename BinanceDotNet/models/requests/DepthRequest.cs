using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class DepthRequest : RequestWithSymbol {
        public int Limit { get; set; }

        public override string BuildUrl() {
            return $"v1/depth?symbol={Symbol}&limit={Limit}";
        }

        public override bool IsValid() {
            return ValidateRequired("Symbol");
        }

        
    }
}
