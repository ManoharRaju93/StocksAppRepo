using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinhubService _finhubService; 
        private readonly IOptions<TradingOptions> _tradingOptions;
        public HomeController(FinhubService finhubService, IOptions<TradingOptions> tradingOptions) {
            _finhubService = finhubService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStock == null)
            {
                _tradingOptions.Value.DefaultStock = "MSFT";
            }

            Dictionary<string, object>? response =await _finhubService.
                GetStockDetails(_tradingOptions.Value.DefaultStock);
            Stocks stock = new Stocks()
            {
                StocksSymbol = _tradingOptions.Value.DefaultStock,
                CurrentPrice = Convert.ToDouble(response["c"].ToString()),
                LowestPrice = Convert.ToDouble(response["l"].ToString()),
                HighestPrice = Convert.ToDouble(response["h"].ToString()),
                OpenPrice = Convert.ToDouble(response["o"].ToString()),
            };
            
            return View(stock);
        }
    }
}
