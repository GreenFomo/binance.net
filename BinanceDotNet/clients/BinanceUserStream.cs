using BinanceDotNet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.clients {
    public class BinanceUserStream {
        private UserDataEndpoint activeConnection; 

        public BinanceClient HttpApi { get; set; }
        public BinanceSocketClient SocketApi { get; set; }

        public async void Start(Action<string> fn) {
            activeConnection = await HttpApi.StartUserStream();
            Console.WriteLine(activeConnection.ListenKey);

            SocketApi.StartListenUserStream(activeConnection.ListenKey, (r) => {
                Console.WriteLine(r);
                fn.Invoke(r);
            });
        }

        public async void Stop() {
            if (activeConnection == null)
                return;

            await HttpApi.DeleteUserStream(activeConnection.ListenKey);
            activeConnection = null;
        }

    }
}
