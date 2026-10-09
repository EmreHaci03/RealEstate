using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AmenityPropertyDtos;

namespace RealEstate_Dapper_UI.ViewComponents.PropertyDetail
{
    public class PropertyDetailAmenitiesViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyDetailAmenitiesViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/AmenityProperty/GetTrueAmenityPropertByProduct/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
               var jsonData=await responseMessage.Content.ReadAsStringAsync();  
               var values=JsonConvert.DeserializeObject<List<ResultAmenityPropertyDto>>(jsonData) ?? new();
                return View(values);
            }
            return View(new List<ResultAmenityPropertyDto>());
        }
    }
}
