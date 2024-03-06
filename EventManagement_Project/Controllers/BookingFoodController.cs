
using System.Data;
using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{

    public class BookingFoodController : Controller
    {
        private readonly BookingFoodRepos BookingFoodRepos;
        public BookingFoodController(BookingFoodRepos BookingFoodRepos)
        {
            this.BookingFoodRepos = BookingFoodRepos;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookDish()
        {
            List<FoodModel> Food = BookingFoodRepos.Featchdishes();
            TempData["Food"] = Food;

            List<DishTypeModel> dishtype = BookingFoodRepos.Featchdishtype();
            TempData["dishtype"] = dishtype;
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }


        public async Task<ActionResult> FoodBookings(string foodbooking, string selectedFoods)
        {
            try
            {
                string[]? FOOD = JsonConvert.DeserializeObject<string[]>(foodbooking);

                string[]? foodlist = JsonConvert.DeserializeObject<string[]>(selectedFoods);


                var resultAdd = BookingFoodRepos.BookingFood(FOOD,foodlist);

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
