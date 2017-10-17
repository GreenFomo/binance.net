using System;

namespace BinanceDotNet.exceptions {
    public class BinanceBadApiRequest : Exception {
        public BinanceBadApiRequest(string message) : base(message) {
        }
    }
}
