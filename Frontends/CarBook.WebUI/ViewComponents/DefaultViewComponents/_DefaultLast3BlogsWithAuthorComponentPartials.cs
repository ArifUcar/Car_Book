using CarBook.Dto.BlogDtos;
using CarBook.Dto.CarDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLast3BlogsWithAuthorComponentPartials:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultLast3BlogsWithAuthorComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Blog";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint + "/GetLast3BlogsWithAuthorList");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBlogWithAuthorDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}
