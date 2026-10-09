using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AboutItemDtos;

namespace RealEstate_Dapper_UI.ViewComponents.About
{
    public class AboutItemsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutItemsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int AboutId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/AboutItem/GetAboutItemsByAbout/{AboutId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultAboutItemDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultAboutItemDto>());
        }
    }
}
