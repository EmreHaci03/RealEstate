using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Security.Claims;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.ViewComponents.Dashboard
{
    public class EstateAgentHighlightedProductsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EstateAgentHighlightedProductsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage=await client.GetAsync($"https://localhost:7025/api/Product/GetProductAdvertsListByUser/{userId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData) ?? new();
                ViewBag.MostExpensive = values.OrderByDescending(x=>x.Price).FirstOrDefault();
                ViewBag.Cheapest = values.OrderBy(x=>x.Price).FirstOrDefault(); 
                ViewBag.Newest = values.OrderByDescending(x=>x.ProductId).FirstOrDefault();
            }
            return View();
        }
    }
}
