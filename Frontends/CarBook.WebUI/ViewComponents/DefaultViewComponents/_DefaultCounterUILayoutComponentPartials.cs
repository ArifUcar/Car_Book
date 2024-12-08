using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCounterUILayoutComponentPartials:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
