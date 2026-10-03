using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.DashboardDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Dashboard
{
    public class DashboardEmployeePerformanceViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardEmployeePerformanceViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Dashboard/GetEmployeePerformance");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultEmployePerformanceDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultEmployePerformanceDto>());
        }
    }
}
