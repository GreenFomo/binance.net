using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class Order {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public decimal Price { get; set; }
        public decimal OrigQty { get; set; }
        public decimal ExecutedQty { get; set; }
        public string Status { get; set; }
        public string TimeInForce { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public long Time { get; set; }
    }
}
