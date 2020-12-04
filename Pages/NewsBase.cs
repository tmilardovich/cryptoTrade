using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace cryptoTrade.Pages
{
    public class NewsBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        public NewsData[] news;

        protected override async Task OnInitializedAsync()
        {
            news = await Http.GetFromJsonAsync<NewsData[]>("news-data/news.json");
        }

        public class NewsData
        {
            public string Title { get; set; }

            public string Summary { get; set; }

            public string Picture { get; set; }

            public string LinkToArticle { get; set; }
        }
    }
}
