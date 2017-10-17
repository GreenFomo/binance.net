using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class WsKline : WsBase {
        [JsonProperty("k")]
        public WsKlineInner line { get; set; }

       
    }
}

