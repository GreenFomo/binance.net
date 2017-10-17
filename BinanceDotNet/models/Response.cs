using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class Response<T> {
        public T Content { get; set; }
        public string URL { get; set; }
        public string Method { get; set; }

        public Response(T content, RawResponse rawResponse) {
            Content = content;
            URL = rawResponse.URL;
            Method = rawResponse.Method;
        }
    }
}
