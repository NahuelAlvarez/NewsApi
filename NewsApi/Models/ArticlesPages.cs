using System.Collections.Generic;

namespace NewsApi.Models
{
    public class ArticlesPages 
    { 
        public int Page { get; set; }
        public List<Article> Articles { get; set; }
    }
}
