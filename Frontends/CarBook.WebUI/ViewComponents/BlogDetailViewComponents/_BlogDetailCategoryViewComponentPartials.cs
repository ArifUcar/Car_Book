using CarBook.Dto.BlogDtos;
using CarBook.Dto.CategoryDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailCategoryViewComponentPartials : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailCategoryViewComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Categories";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
    }
}
