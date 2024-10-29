namespace StocksApp.ServiceContract
{
    public interface IFinhubService
    {
        Task<Dictionary<String,object>?> GetStockDetails(string stockName);
    }
}
