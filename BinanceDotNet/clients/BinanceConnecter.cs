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

        private string apiKey;
        private string apiSecret;

        private HttpClient _client;

        public BinanceConnecter() {
            _client = new HttpClient {
                BaseAddress = new Uri(BASE)
            };
        }

        public BinanceConnecter(ClientConfig cfg) : this() {
            if (cfg != null)
                SetApiDetails(cfg.ApiKey, cfg.ApiSecret);
        }

        public void SetApiDetails(string _apiKey, string _apiSecret) {
            apiKey = _apiKey;
            apiSecret = _apiSecret;
        }


        public async Task<RawResponse> PrivateRequest(string url, Dictionary<string, string> data, HttpMethod method = null) {
            if (apiKey == null || apiSecret == null) {
                throw new BinanceClientNotConfigured($"API key and API secret must be set before making private/signed requests. Provided: {apiKey}, {apiSecret}");
            }

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
            byte[] keyByte = encoding.GetBytes(apiSecret);
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
                requestMessage.Headers.Add("X-MBX-APIKEY", apiKey);
            }

            return await _client.SendAsync(requestMessage);
        }
    }
}
