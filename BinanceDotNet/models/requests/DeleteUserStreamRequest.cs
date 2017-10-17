using System.Net.Http;

namespace BinanceDotNet.models.requests {
    public class DeleteUserStreamRequest : UserStreamRequest {
        public string ListenKey { get; set; }

        public DeleteUserStreamRequest() : base() {
            Method = HttpMethod.Delete;
        }

        public override bool IsValid() {
            return ValidateRequired("ListenKey");
        }
    }
}
