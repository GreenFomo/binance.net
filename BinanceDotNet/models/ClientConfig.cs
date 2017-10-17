namespace BinanceDotNet.models {

    public class ClientConfig {
        public string ApiKey { get; }
        public string ApiSecret { get; }

        public ClientConfig() {

        }

        public ClientConfig(string _apiKey, string _apiSecret) {
            ApiKey = _apiKey;
            ApiSecret = _apiSecret;
        }
    }
}
