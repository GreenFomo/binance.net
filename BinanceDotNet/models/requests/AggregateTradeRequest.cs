using System.Collections.Generic;

namespace BinanceDotNet.models.requests {
    public class AggregateTradeRequest : RequestWithSymbol {
        public long? FromId { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public int? Limit { get; set; }

        public override string BuildUrl() {
            var qp = new Dictionary<string, string> {
                ["symbol"] = Symbol
            };

            if (StartTime.HasValue)
                qp["startTime"] = StartTime.ToString();

            if (EndTime.HasValue)
                qp["endTime"] = StartTime.ToString();

            if (Limit.HasValue)
                qp["limit"] = Limit.ToString();

            var qs = BuildQueryStringFromParams(qp);

            return $"v1/aggTrades?{qs}";
        }

        public override bool IsValid() {
            return ValidateRequired("Symbol") && (Limit == null || Limit <= 500);
        }
    }
}
