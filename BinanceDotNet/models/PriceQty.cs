namespace BinanceDotNet.models {
    public class PriceQty {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        public PriceQty(decimal _price, decimal _qty) {
            Price = _price;
            Quantity = _qty;
        }
    }
}
