using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.StatisticDtos;

namespace RealEstate_Dapper_UI.ViewComponents.About
{
    public class AboutStatisticViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutStatisticViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Statistic");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<ResultStatisticDto>(jsonData);
                return View(values);
            }
            return View(new List<ResultStatisticDto>());
        }
    }
}
