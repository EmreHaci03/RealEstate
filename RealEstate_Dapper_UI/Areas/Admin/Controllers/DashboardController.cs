using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.DashboardDtos;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Dashboard/GetProductCountByCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductCountByCategoryDto>>(jsonData);
                var ProductCountWithCategory = values;
                ViewBag.ProductCount = JsonConvert.SerializeObject(ProductCountWithCategory.Select(x=>x.ProductCount).ToList());
                ViewBag.CategoryName = JsonConvert.SerializeObject(ProductCountWithCategory.Select(x => x.CategoryName).ToList());
            }

            var ProdutMessage = await client.GetAsync("https://localhost:7025/api/Dashboard/GetProductByCity");
            if (ProdutMessage.IsSuccessStatusCode)
            {
                var jsonData=await ProdutMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultDashboardProductByCityDto>>(jsonData);
                ViewBag.ProductCountByCity = JsonConvert.SerializeObject(values.Select(x => x.ProductCount).ToList());
                ViewBag.City = JsonConvert.SerializeObject(values.Select(x => x.City).ToList());
            }

            var dayMessage = await client.GetAsync("https://localhost:7025/api/Dashboard/GetDashboardDayMessage");
            if(dayMessage.IsSuccessStatusCode)
            {
                var jsonData=await dayMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultDashboardDayMessageDto>>(jsonData);
                ViewBag.MessageCount = JsonConvert.SerializeObject(values.Select(x => x.MessageCount).ToList());
                ViewBag.Date = JsonConvert.SerializeObject(values.Select(x => x.Day).ToList());
            }

            return View();
        }
    }
}
