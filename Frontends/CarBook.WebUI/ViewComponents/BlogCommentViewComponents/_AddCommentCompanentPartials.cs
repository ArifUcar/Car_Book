using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogCommentViewComponents
{
    public class _AddCommentCompanentPartials:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
