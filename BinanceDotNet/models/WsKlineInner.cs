using BinanceDotNet.models.converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class WsKlineInner {
        [JsonProperty("t")]
        public long StartTime { get; set; }

        [JsonProperty("T")]
        public long EndTime { get; set; }

        [JsonProperty("i")]
        public string Interval { get; set; }

        [JsonProperty("f")]
        public string FirstTradeId { get; set; }

        [JsonProperty("L")]
        public string LastTradeId { get; set; }

        [JsonProperty("o")]
        public decimal Open { get; set; }

        [JsonProperty("c")]
        public decimal Close { get; set; }

        [JsonProperty("h")]
        public decimal High { get; set; }

        [JsonProperty("l")]
        public decimal Low { get; set; }

        [JsonProperty("v")]
        public decimal Volume { get; set; }

        [JsonProperty("n")]
        public long NumberOfTrades { get; set; }

        [JsonProperty("x")]
        public bool IsFinal { get; set; }

        [JsonProperty("q")]
        public decimal QuoteVolume { get; set; }

        [JsonProperty("V")]
        public decimal ActiveBuyVolume { get; set; }

        [JsonProperty("Q")]
        public decimal ActiveBuyQuoteVolume { get; set; }

        public override string ToString() {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ContractResolver = new OriginalPropertyContractResolver()
            };

            return JsonConvert.SerializeObject(this, settings);

        }
    }
}
