using BinanceDotNet.exceptions;
using BinanceDotNet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.clients {
    public class BinanceConnecter {
        private const string BASE = "https://www.binance.com/api/";
        private const string BASE_ORDER = BASE + "/v3/";

        private const string KEY = "2hL5U6keDbQuEhM13LszKMMDNmaZTLgZZIoKKk2RYMOAhzVMU2DIq5roZvdpLwcE";
        private const string SECRET = "OTMvwc0YtJI1mL0IzGPvhHryFJVhThe1qgoTWAHgRBeZ95grZvZSo03JSS7Q9mHK";

        private HttpClient _client;

        public BinanceConnecter() {
            _client = new HttpClient {
                BaseAddress = new Uri(BASE)
            };
        }

        public async Task<RawResponse> PrivateRequest(string url, Dictionary<string, string> data, HttpMethod method = null) {
            
            if (data == null) {
                data = new Dictionary<string, string>();
            }
            data["timestamp"] = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            var query = data.Select(kv => String.Format("{0}={1}", kv.Key, Uri.EscapeDataString(kv.Value))).ToArray();
            var qs = String.Join("&", query);
            Console.WriteLine(qs);

            var sig = SignRequest(qs);
            Console.WriteLine(sig);
            Console.WriteLine(sig.Length);

            url += $"?{qs}&signature={sig}";
            var response = await ReqAsync(url, method, true);
            Console.WriteLine("URL: " + response.RequestMessage.RequestUri.ToString());
            Console.WriteLine(response.StatusCode);

            if (!response.IsSuccessStatusCode) {
                throw new BinanceFailedRequest($"Request failed with status code: {response.StatusCode}. Request URL: {response.RequestMessage.RequestUri.ToString()}");
            }
            return await RawResponse.FromHttpResponse(response);
        }

        public async Task<RawResponse> PublicRequest(string url, Dictionary<string, string> data = null) {
            var response = await ReqAsync(url);
            return await RawResponse.FromHttpResponse(response);
            
            
        }

        private string SignRequest(string input) {
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(SECRET);
            byte[] messageBytes = encoding.GetBytes(input);

            using (var hmacsha256 = new HMACSHA256(keyByte)) {
                byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);
                return String.Concat(hashMessage.Select(b => b.ToString("x2")));
            }
        }

        //###############

        private async Task<HttpResponseMessage> ReqAsync(string url, HttpMethod method = null, bool apiAuth = false) {
            var requestMessage = new HttpRequestMessage(method ?? HttpMethod.Get, url);
            
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("X-MBX-APIKEY", KEY);

            if (apiAuth) {
                requestMessage.Headers.Add("X-MBX-APIKEY", KEY);
            }
            
            return await _client.SendAsync(requestMessage);
        }
    }
}
