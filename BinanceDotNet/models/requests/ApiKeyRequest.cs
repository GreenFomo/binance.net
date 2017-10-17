namespace BinanceDotNet.models.requests {
    public abstract class ApiKeyRequest : Request{

        public ApiKeyRequest() {
            UseApiKey = true;
        }
    }
}
