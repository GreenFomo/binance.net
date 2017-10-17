using BinanceDotNet.models.enums;
using System.Collections.Generic;
using static BinanceDotNet.extensions.EnumHelper;

namespace BinanceDotNet.models.requests {
    public class CandlestickRequest : RequestWithSymbol {
        public KlineInterval Interval { get; set; }
        public int Limit { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }

        public override string BuildUrl() {
            return $"v1/klines?symbol={Symbol}&interval={Interval.GetValue()}";
        }

        public override bool IsValid() {
            return ValidateRequireds(new List<string>() { "Symbol", "Interval" });
        }
    }
}
