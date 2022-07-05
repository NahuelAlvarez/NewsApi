using System;
using System.Collections.Generic;

namespace NewsApi.Models
{
    public class NewsResponse
    {
        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Article> Articles { get; set; }
        public int TotalResults { get; set; }
    }

    public class Article
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
    }

    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


}
