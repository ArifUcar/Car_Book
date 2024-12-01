
using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Environments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.Controllers
{
    public class ServiceController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
          
    }
}
