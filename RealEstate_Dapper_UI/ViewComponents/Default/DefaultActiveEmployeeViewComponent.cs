using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.EmployeeDtos;
using System.Net.Http;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultActiveEmployeeViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultActiveEmployeeViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Employee/ActiveEmployees");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultEmployeeDto>());
        }
    }
}
