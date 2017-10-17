using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace BinanceDotNet.models.converters {
    public class CustomDepthConverter : JsonConverter {
        private string idKey;
        private string bidsKey;
        private string asksKey;


        public CustomDepthConverter(string _idKey = "lastUpdateId", string _bidsKey = "bids", string _asksKey = "asks") {
            idKey = _idKey;
            bidsKey = _bidsKey;
            asksKey = _asksKey;

        }
        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(Depth));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject obj = JObject.Load(reader);
            Depth root = new Depth();
            Console.WriteLine(obj);

            root.LastUpdateId = (long)obj[idKey];

            var rawBids = obj[bidsKey];
            
            foreach (var rawBid in rawBids) {
                root.Bids.Add(new Bid((decimal)rawBid[0], (decimal)rawBid[1]));
            }

            var rawAsks= obj[asksKey];

            foreach (var rawAsk in rawAsks) {
                root.Asks.Add(new Ask((decimal)rawAsk[0], (decimal)rawAsk[1]));
            }

            return root;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
