namespace StocksApp.Models
{
    public class Stocks
    {
        public string? StocksSymbol { get; set; }
        public double CurrentPrice { get; set; }
        public double LowestPrice { get; set; }
        public double HighestPrice { get; set; }
        public double OpenPrice { get; set; }

    }
}
