using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.EmployeeDtos;
using RealEstate_Dapper_UI.DTOS.ProductDetailDtos;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using RealEstate_Dapper_UI.DTOS.ProductImageDtos;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> PropertyList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithAllDetail");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData) ?? new();
                return View(values);
            }
            return View(new List<ResultProductWithDetailDto>());
        }

        [HttpGet]
        public async Task<IActionResult> PropertyDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var employeeResponse = await client.GetAsync($"https://localhost:7025/api/Employee/GetEmployeeByProduct?productId={id}");
            if (employeeResponse.IsSuccessStatusCode)
            {
                var jsonData = await employeeResponse.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<GetEmployeeByProductIdDto>(jsonData) ?? new();
                ViewBag.EmployeeId = employee.EmployeeId;
                ViewBag.EmployeeName = employee.Name;
                ViewBag.EmployeeMail = employee.Mail;
            }

            var imageResponse = await client.GetAsync($"https://localhost:7025/api/ProductImage/GetProductImageByProductId/{id}");
            if (imageResponse.IsSuccessStatusCode)
            {
                var jsonData = await imageResponse.Content.ReadAsStringAsync();
                var images = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData) ?? new();
                ViewBag.Images = images.Select(x => x.ImageUrl).ToList();
            }

            var responseMessage = await client.GetAsync($"https://localhost:7025/api/ProductDetail/GetProductDetailByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData) ?? new();
                return View(values);
            }

            return View(new GetProductDetailByIdDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyListByUser(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Product/GetProductAdvertsListByUser/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData) ?? new();
                return View(values);
            }

            return View(new List<ResultProductWithDetailDto>());
        }
    }
}
