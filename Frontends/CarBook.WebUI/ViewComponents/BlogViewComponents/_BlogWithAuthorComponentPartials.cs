using CarBook.Dto.BannerDtos;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogWithAuthorComponentPartials:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogWithAuthorComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Blog";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint+ "/GetAllBlogsWithAuthorList");

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
