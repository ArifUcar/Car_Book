using CarBook.Dto.BannerDtos;
using CarBook.Dto.CarDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCoverUILayoutComponentPartials :ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultCoverUILayoutComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async  Task<IViewComponentResult> InvokeAsync() {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Banners";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint );

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List< ResultBannerDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
    }

