using Microsoft.Extensions.Configuration;
using StocksApp.ServiceContract;
using System.Collections;
using System.Text.Json;
namespace StocksApp.Services
{
    public class FinhubService:IFinhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinhubService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<Dictionary<String,object>?> GetStockDetails(string stockSymbol)
        {
            using (HttpClient httpclient= _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri
                    ($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={
                        _configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };
                HttpResponseMessage httpResponseMessage = await httpclient.SendAsync(httpRequestMessage);
                Stream responseContent =httpResponseMessage.Content.ReadAsStream();

                StreamReader responseReader = new StreamReader(responseContent);
               string response= responseReader.ReadToEnd();

               Dictionary<string,object>? responseDictionary= JsonSerializer.Deserialize<Dictionary<string,object>>(response);
               if(responseDictionary==null)
                {
                    throw new InvalidOperationException("no response from finhub service");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.
                        ToString(responseDictionary["error"]));
                }               
                return responseDictionary;
            }
        }
    }
}
