using CarBook.Dto.BlogDtos;
using CarBook.Dto.CategoryDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailAuthorAboutComponentPartials:ViewComponent
    {
       


        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailAuthorAboutComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.blogid = id;
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Blog/";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint+ "GetBlogWithAuthorByAuthorId/"+id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultBlogByAuthorId>(jsonData);
                return View(values);
            }


            return View();
        }
    }
    }

