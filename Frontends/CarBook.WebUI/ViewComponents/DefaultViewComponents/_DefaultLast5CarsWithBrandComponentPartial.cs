using CarBook.Dto.CarDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLast5CarsWithBrandComponentPartial :ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultLast5CarsWithBrandComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Car";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint + "/GetLast5CarsWithBrand");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLast5CarsWithBrandDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}
