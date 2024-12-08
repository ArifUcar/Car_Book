using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailParagraphComponentPartials :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
