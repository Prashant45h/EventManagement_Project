using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class AllBookingsController : Controller
    {
        private readonly AllBookingRepos AllBookingRepos;
        public AllBookingsController(AllBookingRepos AllBookingRepos)
        {
            this.AllBookingRepos = AllBookingRepos;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAllBookings()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }



        [HttpPost]
        public IActionResult LoadBookingdetails()
        {
            try
            {
                var result = AllBookingRepos.GetBookingDetails(Team.storedRoleId);
                if (result.Rows.Count > 0)
                {
                    var List = new List<BookingDetailsModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        BookingDetailsModel Obj = new BookingDetailsModel();
                        Obj.BookingID = Convert.ToInt32(dr["BookingID"]);
                        Obj.BookingNo = Convert.ToString(dr["BookingNo"]);
                        Obj.BookingDate = Convert.ToDateTime(dr["BookingDate"]);
                        Obj.BookingApprovel = Convert.ToString(dr["BookingApprovel"]);
                        Obj.BookingApprovelDate = dr["BookingApprovelDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["BookingApprovelDate"]);

                        List.Add(Obj);
                    }
                    return Json(new { IsSuccess = true, Message = "", List });
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Json(new { IsSuccess = false, Message = "Something went wrong while fetching records" });
        }

        [HttpGet]
        public ActionResult Getdataforedit(string BookingNo)
        {
            try
            {
                var venueResult = AllBookingRepos.Getvenudata(BookingNo);
                string venuname = string.Empty;
                int venuCost = 0;

                if (venueResult.Rows.Count > 0)
                {
                    venuname = Convert.ToString(venueResult.Rows[0]["VenuName"]);
                    venuCost = Convert.ToInt32(venueResult.Rows[0]["VenuCost"]);

                }

        


                var EquipmentCostResult = AllBookingRepos.GetEquipmentdata(BookingNo);
                List<string> EquipmentNames = new List<string>();
                List<int> EquipmentCosts = new List<int>();

                foreach (DataRow row in EquipmentCostResult.Rows)
                {
                    EquipmentNames.Add(Convert.ToString(row["EquipmentName"]));
                    EquipmentCosts.Add(Convert.ToInt32(row["EquipmentCost"]));
                }



                var FoodResult = AllBookingRepos.Getfooddata(BookingNo);
                List<string> FoodNames = new List<string>();
                List<int> FoodCosts = new List<int>();
                string FoodType = string.Empty;
                string MealType = string.Empty;

                foreach (DataRow row in FoodResult.Rows)
                {
                    FoodNames.Add(Convert.ToString(row["FoodName"]));
                    FoodCosts.Add(Convert.ToInt32(row["FoodCost"]));
                }

                if (FoodResult.Rows.Count > 0)
                {
                    FoodType = Convert.ToString(FoodResult.Rows[0]["FoodType"]);
                    MealType = Convert.ToString(FoodResult.Rows[0]["MealType"]);
                }


               
                var LightResult = AllBookingRepos.GetLightsdata(BookingNo);
                string LightType = string.Empty;
                List<string> LightName = new List<string>();
                List<int> LightCost = new List<int>();
                foreach (DataRow row in LightResult.Rows)
                {
                    LightName.Add(Convert.ToString(row["LightName"]));
                    LightCost.Add(Convert.ToInt32(row["LightCost"]));
                }
                if (LightResult.Rows.Count > 0)
                {
                    LightType = Convert.ToString(LightResult.Rows[0]["LightType"]);

                }

             

                var flowerResult = AllBookingRepos.Getflowerdata(BookingNo);
                List<string> FlowerName = new List<string>();
                List<int> FlowerCost = new List<int>();
                foreach (DataRow row in flowerResult.Rows)
                {
                    FlowerName.Add(Convert.ToString(row["FlowerName"]));
                    FlowerCost.Add(Convert.ToInt32(row["FlowerCost"]));
                }


                var bookingdetails = AllBookingRepos.getbookingdetails(BookingNo);
                int BookingID = 0;
                string bookingNo = string.Empty;
                DateTime bookingDate = DateTime.MinValue;

                if (bookingdetails.Rows.Count > 0)
                {
                    BookingID = Convert.ToInt32(bookingdetails.Rows[0]["BookingID"]);


                    bookingNo = Convert.ToString(bookingdetails.Rows[0]["BookingNo"]);

                    string bookingDateString = Convert.ToString(bookingdetails.Rows[0]["BookingDate"]);
                    bookingDate = DateTime.Parse(bookingDateString);
                }

                var model = new
                {
                    IsSuccess = true,
                    VenuName = venuname,
                    VenuCost = venuCost,
                    EquipmentNames = EquipmentNames,
                    EquipmentCosts = EquipmentCosts,
                    FoodType = FoodType,
                    MealType = MealType,
                    FoodNames = FoodNames,
                    FoodCosts = FoodCosts,
                    LightType = LightType,
                    LightName = LightName,
                    LightCost= LightCost,
                    FlowerName = FlowerName,
                    FlowerCost = FlowerCost,
                    BookingID = BookingID,
                    BookingNo = bookingNo,
                    BookingDate = bookingDate,
                };

                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CancelOrder(string BookingId)
        {
            try
            {
                var resultAdd = AllBookingRepos.Canclled(BookingId);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = "Cancel Order" });
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
