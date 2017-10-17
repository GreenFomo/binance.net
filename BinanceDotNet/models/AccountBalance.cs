using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class AccountBalance {
        public string Asset { get; set; }
        public decimal Free { get; set; }
        public decimal Locked { get; set; }
    }
}
