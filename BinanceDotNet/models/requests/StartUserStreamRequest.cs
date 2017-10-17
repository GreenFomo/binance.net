using System.Net.Http;

namespace BinanceDotNet.models.requests {
    public class StartUserStreamRequest : UserStreamRequest {

        public StartUserStreamRequest() : base() {
            Method = HttpMethod.Post;
        }

        public override bool IsValid() {
            return true;
        }

    }
}
