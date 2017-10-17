using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class AccountInfoRequest : SignedRequest {

        public override Dictionary<string, string> BuildQueryString() {
            return new Dictionary<string, string>();
        }

        public override string BuildUrl() {
           return "v3/account";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
