using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.ViewComponents.Dashboard
{
    public class EstateAgentPriceRangesViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EstateAgentPriceRangesViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var products = new List<ResultProductWithDetailDto>();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7025/api/Product/GetProductAdvertsListByUser/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData) ?? new();
            }

            // 1. Her aralıkta kaç ilan var, say
            var ranges = new List<PriceRangeViewModel>
            {
                new() { Label = "5 Milyon altı",  Count = products.Count(x => x.Price < 5_000_000) },
                new() { Label = "5 - 10 Milyon",  Count = products.Count(x => x.Price >= 5_000_000 && x.Price < 10_000_000) },
                new() { Label = "10 - 20 Milyon", Count = products.Count(x => x.Price >= 10_000_000 && x.Price < 20_000_000) },
                new() { Label = "20 Milyon üstü", Count = products.Count(x => x.Price >= 20_000_000) }
            };

            // 2. Çubuk genişliği: en kalabalık aralık %100, diğerleri ona göre
            var max = ranges.Max(x => x.Count);
            foreach (var range in ranges)
                range.Percent = max > 0 ? (int)Math.Round(range.Count * 100.0 / max) : 0;

            return View(ranges);
        }
    }
}