using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Project.Controllers
{
    

    public class SuperAdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            string superadmin = HttpContext.Session.GetString("Name");
            ViewBag.superadmin = superadmin;
            return View();
        }
    }
}
