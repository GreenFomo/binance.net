using System.Collections.Generic;

namespace BinanceDotNet.models {
    public class Depth : WsBase {
        
        public long LastUpdateId { get; set; }

        public List<Bid> Bids { get; set; }

        public List<Ask> Asks { get; set; }

        public Depth() {
            Bids = new List<Bid>();
            Asks = new List<Ask>();
        }
    }
}
