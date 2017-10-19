using BinanceDotNet.models.enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceDotNet.models {
    public class Order {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public decimal Price { get; set; }
        public decimal OrigQty { get; set; }
        public decimal ExecutedQty { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public long Time { get; set; }
        public long TransactTime { get; set; }
    }
}
