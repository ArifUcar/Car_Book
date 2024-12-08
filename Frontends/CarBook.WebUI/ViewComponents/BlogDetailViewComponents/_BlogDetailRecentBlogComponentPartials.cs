using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailRecentBlogComponentPartials:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailRecentBlogComponentPartials(IHttpClientFactory httpClientFactory)
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
                var values = JsonConvert.DeserializeObject<List<ResultLast3BlogWithAuthorDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}
