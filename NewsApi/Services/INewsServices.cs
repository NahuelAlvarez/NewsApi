using NewsApi.Models;
using System;
using System.Threading.Tasks;

namespace NewsApi.Services
{
    public interface INewsServices
    {
        Task<NewsResponse> GetSearch(DateTime dateFrom, DateTime dateTo, string? keywords, int? pageSize, int? page);

        Task<NewsResponse> GetTopHeadLines(string country, int? page, int? pageSize);
    }
}
