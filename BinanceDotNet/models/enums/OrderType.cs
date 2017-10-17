using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public enum OrderType {
        [EnumMember(Value = "LIMIT")]
        Limit,

        [EnumMember(Value = "MARKET")]
        Market
    }
}
