using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class MyTrade {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Commission { get; set; }
        public string CommissionAssets { get; set; }
        public long Time { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}
