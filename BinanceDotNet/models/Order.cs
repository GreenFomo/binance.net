using BinanceDotNet.models.enums;
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
        public OrderStatus Status { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public OrderType Type { get; set; }
        public OrderSide Side { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public long Time { get; set; }
    }
}
