using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EventManagement_Project.Controllers
{
    public class BookingEquipmentController : Controller
    {
        private readonly BookEqipmentRepos BookEqipmentRepos;
        public BookingEquipmentController(BookEqipmentRepos BookEqipmentRepos)
        {
            this.BookEqipmentRepos = BookEqipmentRepos;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult EqipmentBook()
        {

            List<EquipmentModel> Equipments = BookEqipmentRepos.FeatchEquipments();
            TempData["Equipments"] = Equipments;
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EqipmentBookings(string MODEL)
        {
            try
            {
                dynamic requestData = JsonConvert.DeserializeObject(MODEL);
                List<int> selectedEquipments = requestData.selectedEquipments.ToObject<List<int>>(); 
              
                var resultAdd = BookEqipmentRepos.BOOKEQIPMENT(selectedEquipments);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = "Data saved successfully" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "An Error Occurred" });
                }
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }

    }
}
