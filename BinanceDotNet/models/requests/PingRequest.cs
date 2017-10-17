namespace BinanceDotNet.models.requests {
    public class PingRequest : Request {
        public override string BuildUrl() {
            return "v1/ping";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
