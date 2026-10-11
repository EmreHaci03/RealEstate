using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.CategoryDtos;
using RealEstate_Dapper_UI.DTOS.EmployeeDtos;
using RealEstate_Dapper_UI.DTOS.ProductDetailDtos;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithAllDetail");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync(); 
                var values=JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData);
                return View(values);        
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();

            var categoryResponse = await client.GetAsync("https://localhost:7025/api/Category");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData = await categoryResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                ViewBag.Categories = values?
                    .Where(x => x.CategoryStatus)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId.ToString()
                    })
                    .ToList();
            }

            var employeeResponse = await client.GetAsync("https://localhost:7025/api/Employee");
            if (employeeResponse.IsSuccessStatusCode)
            {
                var jsonData = await employeeResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                ViewBag.Employees = values?
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.EmployeeId.ToString()
                    })
                    .ToList();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Product", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "İlan eklenemedi.";
            return RedirectToAction("CreateProduct");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Product/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Emlak başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Emlak silinemedi.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();


            var productResponse = await client.GetAsync($"https://localhost:7025/api/Product/{id}");
            if (!productResponse.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "İlan bulunamadı.";
                return RedirectToAction("Index");
            }

            var productJson = await productResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<UpdateProductDto>(productJson);

            // Kategoriler
            var categoryResponse = await client.GetAsync("https://localhost:7025/api/Category");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData = await categoryResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                ViewBag.Categories = values?
                    .Where(x => x.CategoryStatus || x.CategoryId == product?.Category)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId.ToString()
                    })
                    .ToList();
            }

            // Danışmanlar
            var employeeResponse = await client.GetAsync("https://localhost:7025/api/Employee");
            if (employeeResponse.IsSuccessStatusCode)
            {
                var jsonData = await employeeResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                ViewBag.Employees = values?
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.EmployeeId.ToString()
                    })
                    .ToList();
            }

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Product", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "İlan güncellenemedi.";
            return RedirectToAction("UpdateProduct", new { id = dto.ProductId });
        }


        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/ProductDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData);
                return View(values);
            }

            return View(new GetProductDetailByIdDto());
        }


    }
}
