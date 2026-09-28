using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Text.Json.Serialization;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultPopularProductListViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DefaultPopularProductListViewComponent(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Client = httpClientFactory.CreateClient();
            var responseMessage = await Client.GetAsync("https://localhost:7025/api/Product/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
