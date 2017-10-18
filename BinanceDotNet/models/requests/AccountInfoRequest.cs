using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public class AccountInfoRequest : SignedRequest {
        public long? RecvWindow { get; set; }

        public override Dictionary<string, string> BuildQueryParams() {
            var qp = new Dictionary<string, string>();

            if (RecvWindow.HasValue)
                qp.Add("recvWindow", RecvWindow.ToString());

            return qp;
        }

        public override string BuildUrl() {
            return "v3/account";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
