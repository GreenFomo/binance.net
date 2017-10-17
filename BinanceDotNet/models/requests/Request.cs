using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public abstract class Request {
        public bool UseApiKey { get; set; }
        public abstract bool IsValid();
        public abstract string BuildUrl();
        public HttpMethod Method { get; set; }

        public Request() {
            Method = HttpMethod.Get;
            UseApiKey = false;
        }

        public bool ValidateRequireds(List<string> propNames) {
            bool valid = true;
            foreach (var propName in propNames) {
                var propValid = ValidateRequired(propName);
                if (propValid) {
                    //add to errors;
                }
                valid &= propValid;

            }
            return valid;
        }

        public bool ValidateRequired(string propName) {
            var value = GetType().GetProperty(propName).GetValue(this);
            if (value == null) {
                return false;
            }
            return true;
        }
    }
}
