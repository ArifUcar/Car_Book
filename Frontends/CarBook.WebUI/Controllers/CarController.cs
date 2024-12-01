using CarBook.Dto.CarDtos;
using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Car";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint+"/GetCarWithBrand");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}

