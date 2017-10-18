using BinanceDotNet.models;
using BinanceDotNet.models.converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceDotNet.clients {
    public class BinanceSocketClient {
        public string LastUrl { get { return _lastUrl; } }
        private string _lastUrl;

        public readonly string WsEndpoint = "wss://stream.binance.com:9443/ws";
        public bool running;

        public void Stop() {
            running = false;
        }

        public async void StartDepth(string pair, Action<Depth> fn) {
            UpdateUrl($"{pair.ToLower()}@depth");

            await ConnectAsync(_lastUrl, fn);
        }

        public async void StartKline(string pair, string interval, Action<WsKline> fn) {
            UpdateUrl($"{pair.ToLower()}@{interval}");

            await ConnectAsync(_lastUrl, fn);
        }

        public async void StartTrades(string pair, Action<WsTrade> fn) {
            UpdateUrl($"{pair.ToLower()}@aggTrade");

            await ConnectAsync(_lastUrl, fn);
        }

        public async void StartListenUserStream(string listenKey, Action<string> fn) {
            UpdateUrl(listenKey);

            await ConnectAsync(_lastUrl, fn);
        }

        private void UpdateUrl(string part) {
            _lastUrl = $"{WsEndpoint}/{part}";
        }

        private async Task ConnectAsync<T>(string uri, Action<T> fn = null) {
            using (var socket = new ClientWebSocket()) {
                Uri serverUri = new Uri(LastUrl);
                running = true;

                await socket.ConnectAsync(serverUri, CancellationToken.None);

                while (running && (socket.State == WebSocketState.Open)) {
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);

                    WebSocketReceiveResult result = await socket.ReceiveAsync(bytesReceived, CancellationToken.None);
                    var line = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    Console.WriteLine("Line: " + line);

                    JsonSerializerSettings settings = new JsonSerializerSettings();

                    Type type = typeof(T);

                    if (type == typeof(Depth)) {
                        var obj = JsonConvert.DeserializeObject<T>(line, new CustomDepthConverter("u", "b", "a"));
                        fn?.Invoke(obj);
                    } else if (type == typeof(string)) {
                        Console.WriteLine("[WS-string]: " + line);
                    } else {
                        var obj = JsonConvert.DeserializeObject<T>(line);
                        fn?.Invoke(obj);
                    }

                    }
                }
            }
        }
    }