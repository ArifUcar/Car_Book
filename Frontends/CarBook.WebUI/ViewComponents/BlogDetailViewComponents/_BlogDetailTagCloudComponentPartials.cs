using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailTagCloudComponentPartials    :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
