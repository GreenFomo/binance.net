namespace BinanceDotNet.models.requests {
    public class TimeRequest : Request {
        public override string BuildUrl() {
            return "v1/time";
        }

        public override bool IsValid() {
            return true;
        }
    }
}
