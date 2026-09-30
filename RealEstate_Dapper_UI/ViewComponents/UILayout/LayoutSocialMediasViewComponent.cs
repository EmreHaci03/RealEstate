using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.SocialMediaDtos;

namespace RealEstate_Dapper_UI.ViewComponents.UILayout
{
    public class LayoutSocialMediasViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LayoutSocialMediasViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7025/api/SocialMedia");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(json);
                return View(values);
            }

            return View(new List<ResultSocialMediaDto>());
        }
    }
}