using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class WsTrade : WsBase {

        [JsonProperty("a")]
        public long TradeId { get; set; }

        [JsonProperty("p")]
        public decimal Price { get; set; }

        [JsonProperty("q")]
        public decimal Quantity { get; set; }

        [JsonProperty("f")]
        public long FirstTradeId { get; set; }

        [JsonProperty("l")]
        public long LastTradeId { get; set; }

        [JsonProperty("T")]
        public long TradeTime { get; set; }

        [JsonProperty("m")]
        public bool IsMaker { get; set; }
    }
}
