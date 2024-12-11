using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogCommentViewComponents
{
    public class _CommentListByBlogComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
