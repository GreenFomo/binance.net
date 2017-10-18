using BinanceDotNet.models;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BinanceDotNet.clients {
    public class BinanceUserStream {
        public UserDataEndpoint ActiveConnection { get; set; }

        private DispatcherTimer _timer;

        public BinanceClient HttpApi { get; set; }
        public BinanceSocketClient SocketApi { get; set; }

        public async Task<string> Start(Action<string> fn) {
            ActiveConnection = await HttpApi.StartUserStream();

            Console.WriteLine(ActiveConnection.ListenKey);

            SocketApi.StartListenUserStream(ActiveConnection.ListenKey, (r) => {
                Console.WriteLine(r);
                fn.Invoke(r);
            });

            StartPing();

            return SocketApi.LastUrl;
        }

        public async void Stop() {
            if (ActiveConnection == null)
                return;

            StopPing();

            await HttpApi.DeleteUserStream(ActiveConnection.ListenKey);
            ActiveConnection = null;
        }

        public void StartPing() {
            _timer = new DispatcherTimer();
            _timer.Tick += async (obj, e) => {
                var resp = await HttpApi.PingUserStream(ActiveConnection.ListenKey);
                Console.WriteLine(resp.Content);
            };
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Start();
        }

        private void StopPing() {
            if (_timer == null)
                return;

            _timer.Stop();
            _timer = null;
        }

    }
}
