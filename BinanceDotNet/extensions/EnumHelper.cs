using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.extensions {
    public static class EnumHelper {
        
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetValue(this Enum enumVal) {
            return GetAttributeOfType<EnumMemberAttribute>(enumVal).Value;
        }
        
    }
}
