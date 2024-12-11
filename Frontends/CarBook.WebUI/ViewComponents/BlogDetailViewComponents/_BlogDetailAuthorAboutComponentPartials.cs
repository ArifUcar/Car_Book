using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailAuthorAboutComponentPartials:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
