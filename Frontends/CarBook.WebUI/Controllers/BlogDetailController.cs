using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class BlogDetailController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v1 = "Bloglar";
            ViewBag.v2 = "Blog Detayı ve Yorumlar";
            return View();
        }
    }
}
