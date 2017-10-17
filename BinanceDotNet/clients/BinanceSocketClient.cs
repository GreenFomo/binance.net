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

        public bool running;

        public void Stop() {
            running = false;
        }

        public async void StartDepth(string pair, Action<Depth> fn) {
            await ConnectAsync($"wss://stream.binance.com:9443/ws/{pair.ToLower()}@depth", fn);
        }

        public async void StartKline(string pair, string interval, Action<WsKline> fn) {
            await ConnectAsync($"wss://stream.binance.com:9443/ws/{pair.ToLower()}@{interval}", fn);
        }

        public async void StartTrades(string pair, Action<WsTrade> fn) {
            Console.WriteLine("YO");
            await ConnectAsync($"wss://stream.binance.com:9443/ws/{pair.ToLower()}@aggTrade", fn);
        }


        private async Task ConnectAsync<T>(string uri, Action<T> fn = null) {
            Console.WriteLine(uri);

            using (var socket = new ClientWebSocket()) {
                Uri serverUri = new Uri(uri);
                running = true;

                await socket.ConnectAsync(serverUri, CancellationToken.None);

                while (running && (socket.State == WebSocketState.Open)) {
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);

                    WebSocketReceiveResult result = await socket.ReceiveAsync(bytesReceived, CancellationToken.None);
                    var line = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    Console.WriteLine(line);

                    JsonSerializerSettings settings = new JsonSerializerSettings();

                    Type listType = typeof(T);

                    if (listType == typeof(Depth)) {
                        var obj = JsonConvert.DeserializeObject<T>(line, new CustomDepthConverter("u", "b", "a"));
                        fn?.Invoke(obj);
                    } else {
                        var obj = JsonConvert.DeserializeObject<T>(line);
                        fn?.Invoke(obj);
                    }

                }
            }
        }
    }
}