using BinanceDotNet.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BinanceDotNet.models.converters {
    public class CustomCandlestickConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(List<Candlestick>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JArray objects = JArray.Load(reader);
            var candles = new List<Candlestick>();

            foreach (var obj in objects) {
                var candle = new Candlestick();
                candle.OpenTime = (long)obj[0];

                candle.Open = (decimal)obj[1];

                candle.High = (decimal)obj[2];

                candle.Low = (decimal)obj[3];

                candle.Close = (decimal)obj[4];

                candle.Volume = (decimal)obj[5];

                candle.CloseTime = (long)obj[6];

                candle.QuoteAssetVolume = (decimal)obj[7];

                candle.NumberOfTrades = (long)obj[8];

                candle.TakerBuyBaseAssetVolume = (decimal)obj[9];

                candle.TakerBuyQuoteAssetVolume = (decimal)obj[10];

                candles.Add(candle);
            }
            return candles;
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

