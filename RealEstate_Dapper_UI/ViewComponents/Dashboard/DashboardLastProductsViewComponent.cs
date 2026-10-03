using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Dashboard
{
    public class DashboardLastProductsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardLastProductsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithAllDetail");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultProductWithDetailDto>());
        }
    }
}
