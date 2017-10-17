using BinanceDotNet.exceptions;
using BinanceDotNet.models;
using BinanceDotNet.models.requests;
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
            return new RawResponse();
        }

        public async Task<RawResponse> PrivateRequest(SignedRequest req) {
            if (apiKey == null || apiSecret == null) {
                throw new BinanceClientNotConfigured($"API key and API secret must be set before making private/signed requests. Provided: {apiKey}, {apiSecret}");
            }

            if (!req.IsValid()) {
                //TODO: proper exception
                throw new Exception("Signed Request not valid");
            }

            var response = await ReqAsync(req.BuildSignedUrl(apiSecret), req.Method, true);
            Console.WriteLine("URL: " + response.RequestMessage.RequestUri.ToString());
            Console.WriteLine(response.StatusCode);

            if (!response.IsSuccessStatusCode) {
                throw new BinanceFailedRequest($"Request failed with status code: {response.StatusCode}. Request URL: {response.RequestMessage.RequestUri.ToString()}");
            }
            return await RawResponse.FromHttpResponse(response);
        }

        public async Task<RawResponse> PublicRequest(Request req, Dictionary<string, string> data = null) {

            if (!req.IsValid())
                throw new Exception("WTF");
            var response = await ReqAsync(req.BuildUrl());
            return await RawResponse.FromHttpResponse(response);


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
