using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class BookingVenuController : Controller
    {
        private readonly BookingVenuRepos BookingVenuRepos;
        public BookingVenuController(BookingVenuRepos BookingVenuRepos)
        {
            this.BookingVenuRepos = BookingVenuRepos;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VenuBooking()
        {
            List<Eventtypemoedel> eventTypes = BookingVenuRepos.GetEventType();
            TempData["eventTypes"] = eventTypes;

            List<VenuModel> venutypes = BookingVenuRepos.GetVenus();
            TempData["venutypes"] = venutypes;

            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;

            return View();



        }
        [HttpPost]
        public async Task<ActionResult> Bookingsave(string BookingModel)
        {
            try
            {
                string[]? venubook = JsonConvert.DeserializeObject<string[]>(BookingModel);

                if (BookingVenuRepos.ISDateAvailableorNot(venubook[5], venubook[2]))
                {
                    var resultAdd = BookingVenuRepos.SaveBookings(venubook);

                    if (resultAdd)
                    {
                        return Json(new { IsSuccess = true, Message = "Venu Added successfully" });
                    }
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "An Error Occurred" });
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Sorry The Schedulled Is Busyy...." });
                }
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }
    }
}
