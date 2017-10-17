using System.Net.Http;

namespace BinanceDotNet.models.requests {
    class CancelOrderRequest : GetOrderRequest {

        public CancelOrderRequest() : base() {
            Method = HttpMethod.Delete;
        }
    }
}
