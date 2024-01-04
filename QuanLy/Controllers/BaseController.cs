using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLy.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentUser
        {
            get
            {
                return HttpContext.Session.GetString("User_Name");

            }
            set
            {
                HttpContext.Session.SetString("User_Name", value);
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentUser);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
