using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models.requests {
    public abstract class SignedRequest : Request {
        public long Timestamp { get; set; }
        public string Symbol { get; set; }

        public SignedRequest() {
            Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public abstract Dictionary<string, string> BuildQueryString();

        public string BuildSignedUrl(string secret) {
            var data = BuildQueryString();
            data["timestamp"] = Timestamp.ToString();

            var query = data.Select(kv => String.Format("{0}={1}", kv.Key, Uri.EscapeDataString(kv.Value))).ToArray();
            var qs = String.Join("&", query);
            Console.WriteLine(qs);

            var sig = SignRequest(qs, secret);
            Console.WriteLine(sig);
            Console.WriteLine(sig.Length);

            var url = BuildUrl();
            url += $"?{qs}&signature={sig}";

            return url;
        }

        private string SignRequest(string input, string apiSecret) {
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytes = encoding.GetBytes(input);

            using (var hmacsha256 = new HMACSHA256(keyByte)) {
                byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);
                return String.Concat(hashMessage.Select(b => b.ToString("x2")));
            }
        }

    }
}
