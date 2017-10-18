using System.Net.Http;

namespace BinanceDotNet.models.requests {
    public class PingUserStreamRequest : UserStreamRequest {
        public string ListenKey { get; set; }

        public PingUserStreamRequest() : base() {
            Method = HttpMethod.Put;
        }

        public override string BuildUrl() {
            var baseUrl = base.BuildUrl();

            return $"{baseUrl}?listenKey={ListenKey}";
        }

        public override bool IsValid() {
            return ValidateRequired("ListenKey");
        }
    }
}
