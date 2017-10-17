using System.Runtime.Serialization;

namespace BinanceDotNet.models.enums {
    public enum KlineInterval {
        [EnumMember(Value="1m")]
        Minutes1,

        [EnumMember(Value = "3m")]
        Minutes3,

        [EnumMember(Value = "5m")]
        Minutes5,

        [EnumMember(Value = "15m")]
        Minutes15,

        [EnumMember(Value = "30m")]
        Minutes30,

        [EnumMember(Value = "1h")]
        Hours1,

        [EnumMember(Value = "2h")]
        Hours2,

        [EnumMember(Value = "4h")]
        Hours4,

        [EnumMember(Value = "6h")]
        Hours6,

        [EnumMember(Value = "8h")]
        Hours8,

        [EnumMember(Value = "12h")]
        Hours12,

        [EnumMember(Value = "1d")]
        Days1,

        [EnumMember(Value = "3d")]
        Days3,

        [EnumMember(Value = "1w")]
        Weeks1,

        [EnumMember(Value = "1M")]
        Months1
    }
}
