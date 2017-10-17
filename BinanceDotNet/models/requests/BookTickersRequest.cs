using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class BookTickersRequest : Request {
        public override string BuildUrl() {
            return "v1/ticker/allBookTickers";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
