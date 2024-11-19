using CarBook.Dto.AboutDtos;
using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.TestimonialViewComponents
{
	public class _TestimonialComponentPartials:ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public _TestimonialComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            string apiUrl = EnvironmentDevelopment.ApiUrl;
            string endpoint = "Testimonial";


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl + endpoint);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDtos>>(jsonData);
                return View(values);
            }


            return View();
        }
	}
}
