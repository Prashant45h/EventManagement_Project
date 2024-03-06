using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;

namespace EventManagement_Project.Controllers
{

    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

    }
}
