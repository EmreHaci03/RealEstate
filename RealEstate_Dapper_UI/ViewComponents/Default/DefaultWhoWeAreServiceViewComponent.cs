using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ServiceDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultWhoWeAreServiceViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DefaultWhoWeAreServiceViewComponent(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Service/GetActiveServices");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
