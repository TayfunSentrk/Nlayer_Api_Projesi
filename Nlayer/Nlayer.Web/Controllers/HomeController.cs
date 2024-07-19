using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Dtos;

namespace Nlayer.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //Error Page 'e yönlenmek için
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}
