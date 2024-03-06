using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class BookingFlowersController : Controller
    {
        private readonly BookingFlowerRepos BookingFlowerRepos;
        public BookingFlowersController(BookingFlowerRepos BookingFlowerRepos)
        {
            this.BookingFlowerRepos = BookingFlowerRepos;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookFlower()
        {
            List<FlowerModel> bookflower = BookingFlowerRepos.Getflowerslist();
            TempData["bookflower"] = bookflower;
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Flowersbooking(string FLOWERS,string Selectedflowers)
        {
            try
            {
                string[]? FLower = JsonConvert.DeserializeObject<string[]>(FLOWERS);
                string[]? selectedflowersid = JsonConvert.DeserializeObject<string[]>(Selectedflowers);

                var resultAdd = BookingFlowerRepos.BookingFLower(FLower, selectedflowersid);

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
