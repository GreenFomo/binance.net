namespace BinanceDotNet.models {
    public class SymbolPair {
        public string First { get; set; }
        public string Second { get; set; }

        public SymbolPair(string _first, string _second) {
            First = _first;
            Second = _second;
        }

        public override string ToString() {
            return $"{First}{Second}";
        }
    }
}
