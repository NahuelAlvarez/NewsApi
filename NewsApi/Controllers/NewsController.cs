using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Models;
using NewsApi.Services;
using System;
using System.Collections.Generic;

namespace NewsApi.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        [Route("search2/")]
        [HttpGet]
        public IActionResult Search2(DateTime? dateFrom, DateTime? dateTo, string? keywords, int? page, int? pageSize)
        {
            int pag = page ?? 1;
            int total_paginas = 0;
            int records = 5;
            int pageS = pageSize ?? 1;

            List<ArticlesPages> artPages = new List<ArticlesPages>();

            DateTime dateF = dateFrom ?? DateTime.Now;
            DateTime dateT = dateTo ?? DateTime.Now;
            var articles = new List<Article>();

            var datos = _newsServices.GetSearch(dateF, dateT, keywords, pageS, pag);
            if (datos.Result.TotalResults == 0)
            {
                return NotFound(datos);
            }

            if (pageS <= 100)
                total_paginas = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(datos.Result.TotalResults / pageS))));


            for (int i = 0; i < total_paginas; i++)
            {
                var article = new ArticlesPages()
                {
                    Page = i,
                    Articles = new List<Article>()
                };
                for (int j = 0; j < pageS; j++)
                {
                    article.Articles.Add(datos.Result.Articles[j]);
                }
                artPages.Add(article);
            }
            return Ok(artPages);
        }

        [Route("search/")]
        [HttpGet]
        public IActionResult Search(DateTime? dateFrom, DateTime? dateTo, string? keywords, int? page, int? pageSize)
        {

            DateTime dateF = dateFrom ?? DateTime.Now;
            DateTime dateT = dateTo ?? DateTime.Now;


            var datos = _newsServices.GetSearch(dateF, dateT, keywords, page, pageSize);
            if (datos.Result.TotalResults == 0)
            {
                return NotFound(datos);
            }

            return Ok(datos.Result);

        }

        [Route("top-headlines/")]
        [HttpGet]
        public IActionResult GetHeadTopLines(string country, int? page, int? pageSize)
        {


            var datos = _newsServices.GetTopHeadLines(country, page, pageSize);
            if (datos.Result.TotalResults == 0)
            {
                return NotFound(datos);
            }
            return Ok(datos.Result);
        }
    }
}
