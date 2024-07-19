using Microsoft.AspNetCore.Mvc;

namespace Nlayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
