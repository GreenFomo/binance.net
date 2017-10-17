using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.exceptions {
    class BinanceClientNotConfigured : Exception {
        public BinanceClientNotConfigured(string message) : base(message) {
        }
    }
}
