using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDtos createContactDtos)
        {
            try
            {
          
                string apiUrl = EnvironmentDevelopment.ApiUrl;
                string endpoint = "Contact";

        
                var client = _httpClientFactory.CreateClient();


                createContactDtos.SendDate = DateTime.Now;

                var jsonData = JsonConvert.SerializeObject(createContactDtos);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync($"{apiUrl}{endpoint}", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Default");
                }

                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"API hatası: {errorContent}");

                return View(createContactDtos);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Beklenmeyen bir hata oluştu: {ex.Message}");
                return View(createContactDtos);
            }
        }

    }
}
