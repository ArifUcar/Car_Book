using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailCategoryViewComponentPartials:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
