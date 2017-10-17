using System.Net.Http;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class RawResponse {
        public string Content { get; set; }
        public string URL { get; set; }
        public string Method { get; set; }

        public async static Task<RawResponse> FromHttpResponse(HttpResponseMessage msg) {
            var rr = new RawResponse {
                Content = await msg.Content.ReadAsStringAsync(),
                URL = msg.RequestMessage.RequestUri.ToString(),
                Method = msg.RequestMessage.Method.ToString()
            };
            return rr;
        }

        private RawResponse() {
            

        }
    }
}
