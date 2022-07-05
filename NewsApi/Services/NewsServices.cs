using Microsoft.Extensions.Configuration;
using NewsApi.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsApi.Services
{
    public class NewsServices : INewsServices
    {
        private string _baseUrl;
        private static string _api_key;
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient;


        public NewsServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("baseUrl");
            _api_key = _configuration.GetValue<string>("apikey");
            _httpClient = new HttpClient()
            {
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
            };
            _httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _api_key);
        }
        public async Task<NewsResponse> GetSearch(DateTime dateFrom, DateTime dateTo, string? keywords,int? pageSize, int? page)
        {

            var newsResponse = new NewsResponse();       

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "everything" + $"?q=" +
                $"{keywords}&from={dateFrom.ToString("yyyy-mm-dd")}&to={dateTo.ToString("yyyy-mm-dd")}&sortBy=popularity" +
                $"&page ={page} & pageSize ={pageSize}");
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            var json = await httpResponse.Content?.ReadAsStringAsync();

            try
            {
                newsResponse = JsonConvert.DeserializeObject<NewsResponse>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }


            return newsResponse;
        }

        public async Task<NewsResponse> GetTopHeadLines(string country, int? page, int? pageSize)
        {

            var newsResponse = new NewsResponse();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "top-headlines" + $"?country=" +
                $"{country}&page ={page} & pageSize ={pageSize}");
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            var json = await httpResponse.Content?.ReadAsStringAsync();

            try
            {
                newsResponse = JsonConvert.DeserializeObject<NewsResponse>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }


            return newsResponse;
        }


    }
}
