using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.WhoWeAreDetailDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultWhoWeAreViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DefaultWhoWeAreViewComponent(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/WhoWeAreDetail");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);
                ViewBag.Title=values.Select(x=>x.Title).FirstOrDefault();
                ViewBag.SubTitle = values.Select(x => x.SubTitle).FirstOrDefault();
                ViewBag.Description1 = values.Select(x => x.Description1).FirstOrDefault();
                ViewBag.Description2 = values.Select(x => x.Description2).FirstOrDefault();
                return View(values);
            }
            return View();
        }
    }
}
