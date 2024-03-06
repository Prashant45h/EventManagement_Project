using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Project.Controllers
{

    public class CustomerDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            string Username = HttpContext.Session.GetString("Name");
            ViewData["Name"] = Username;
            return View();
        }
    }
}
