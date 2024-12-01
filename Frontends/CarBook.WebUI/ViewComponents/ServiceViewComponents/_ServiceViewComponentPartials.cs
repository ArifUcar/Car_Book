using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.ServiceViewComponents
{
    public class _ServiceViewComponentPartials:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ServiceViewComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Service";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}
