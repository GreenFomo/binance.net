using System;

namespace BinanceDotNet.exceptions {
    public class BinanceFailedRequest : Exception {
        public BinanceFailedRequest(string message) : base(message) {
        }
    }
}
