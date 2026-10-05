using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Security.Claims;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.ViewComponents.Dashboard
{
    public class EstateAgentDashboardLastProductsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EstateAgentDashboardLastProductsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return View(new List<ResultProductWithDetailDto>());


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Product/ProductAdvertsListByEmployee?id={userId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData) ?? new();
                return View(values.OrderByDescending(x=>x.ProductId).Take(5).ToList());
            }

            
            return View(new List<ResultProductWithDetailDto>());
        }
    }
}
