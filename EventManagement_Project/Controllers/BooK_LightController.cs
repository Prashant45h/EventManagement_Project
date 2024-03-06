using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class BooK_LightController : Controller
    {
        private readonly LightBookingRepos LightBookingRepos;
        public BooK_LightController(LightBookingRepos LightBookingRepos)
        {
            this.LightBookingRepos = LightBookingRepos;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LightBooking()
        {
            List<LightModel> LightBook = LightBookingRepos.Getlighting();
            TempData["LightBook"] = LightBook;
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LightingsBook(string LIGHTS ,string Selectedlights)
        {
            try
            {
                string[]? LIGHT = JsonConvert.DeserializeObject<string[]>(LIGHTS);
                string[]? lights = JsonConvert.DeserializeObject<string[]>(Selectedlights);

                    var resultAdd = LightBookingRepos.BookingLight(LIGHT,lights);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = " Data Save succesfully" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "An Error Occured" });
                }
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }
    }
}
