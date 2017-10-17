using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace BinanceDotNet.models.converters {
    public class OriginalPropertyContractResolver : DefaultContractResolver {

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
            // Let the base class create all the JsonProperties 
            // using the short names
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            // Now inspect each property and replace the 
            // short name with the real property name
            foreach (JsonProperty prop in list) {
                prop.PropertyName = prop.UnderlyingName;
            }

            return list;
        }
    }
}
