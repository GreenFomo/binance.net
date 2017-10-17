using System.Runtime.Serialization;

namespace BinanceDotNet.models.enums {
    public enum TimeInForce {
        [EnumMember(Value = "GTC")]
        GoodTillCancelled,

        [EnumMember(Value = "IOC")]
        ImmediateOrCancel
    }
}


