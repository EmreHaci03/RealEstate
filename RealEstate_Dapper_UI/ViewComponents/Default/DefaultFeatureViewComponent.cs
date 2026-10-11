using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.CategoryDtos;
using RealEstate_Dapper_UI.DTOS.ProductDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultFeatureViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultFeatureViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var categoryResponse = await client.GetAsync("https://localhost:7025/api/Category");
            var categories = new List<ResultCategoryDto>();
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJson) ?? new();
            }
            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            var productResponse = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithAllDetail");
            var products = new List<ResultProductWithDetailDto>();
            if (productResponse.IsSuccessStatusCode)
            {
                var productJson = await productResponse.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(productJson) ?? new();
            }
            ViewBag.Cities = products
                .Where(x => !string.IsNullOrWhiteSpace(x.City))
                .Select(x => x.City)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return View(products);
        }
    }
}