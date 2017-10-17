using System.Runtime.Serialization;

namespace BinanceDotNet.models.enums {
    public enum OrderSide {
        [EnumMember(Value = "BUY")]
        Buy,

        [EnumMember(Value = "SELL")]
        Sell
    }
}
