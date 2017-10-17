using System;

namespace BinanceDotNet.models.requests {
    public abstract class UserStreamRequest : ApiKeyRequest {
        public override string BuildUrl() {
            return "v1/userDataStream";
        }
    }
}
