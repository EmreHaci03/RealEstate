using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.DashboardDtos;
using System.Net.Http;

namespace RealEstate_Dapper_UI.ViewComponents.Dashboard
{
    public class DashboardSummaryCardViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardSummaryCardViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Dashboard/DashboardSummary");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultDashboardSummaryDto>(jsonData);
                return View(values);
            }
            return View(new List<ResultDashboardSummaryDto>());
        }
    }
}
