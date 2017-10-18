using BinanceDotNet.models.enums;
using System.Collections.Generic;
using static BinanceDotNet.extensions.EnumHelper;

namespace BinanceDotNet.models.requests {
    public class CandlestickRequest : RequestWithSymbol {
        public KlineInterval Interval { get; set; }
        public int? Limit { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }

        public override string BuildUrl() {
            var qp = new Dictionary<string, string>() {
                { "symbol", Symbol },
                { "interval", Interval.GetValue() }
            };

            if (Limit.HasValue)
                qp.Add("limit", Limit.ToString());

            if (StartTime.HasValue)
                qp.Add("startTime", StartTime.ToString());

            if (EndTime.HasValue)
                qp.Add("endTime", EndTime.ToString());

            var qs = BuildQueryStringFromParams(qp);

            return $"v1/klines?{qs}";
        }

        public override bool IsValid() {
            return ValidateRequireds(new List<string>() { "Symbol", "Interval" });
        }
    }
}
