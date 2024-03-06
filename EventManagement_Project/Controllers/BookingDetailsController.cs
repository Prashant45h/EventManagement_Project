using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class BookingDetailsController : Controller
    {

        private readonly BookingDetailsRepos BookingDetailsRepos;
        public BookingDetailsController(BookingDetailsRepos BookingDetailsRepos)
        {
            this.BookingDetailsRepos = BookingDetailsRepos;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookingDetails()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Approved(string DATA)
        {
            try
            {
                string[]? Status = JsonConvert.DeserializeObject<string[]>(DATA);

                var resultAdd = BookingDetailsRepos.approved(Status);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = " Approved" });
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

        [HttpPost]
        public async Task<ActionResult> Rejected(string DATA)
        {
            try
            {
                string[]? Status = JsonConvert.DeserializeObject<string[]>(DATA);

                var resultAdd = BookingDetailsRepos.REJECT(Status);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = " Rejected" });
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

        [HttpPost]
        public IActionResult LoadBookingdetail()
        {
            try
            {
                var result = BookingDetailsRepos.GetBookingDetail();
                if (result.Rows.Count > 0)
                {
                    var List = new List<BookingDetailsModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        BookingDetailsModel Obj = new BookingDetailsModel();
                        Obj.BookingID = Convert.ToInt32(dr["BookingID"]);
                        Obj.Name = Convert.ToString(dr["Name"]);
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


                var venueResult = BookingDetailsRepos.Getvenucost(BookingNo);
                int venuCost = 0;
                if (venueResult.Rows.Count > 0)
                {
                    venuCost = Convert.ToInt32(venueResult.Rows[0]["VenueCost"]);
                }

                var EquipmentCostResult = BookingDetailsRepos.GetEquipmentcost(BookingNo);
                List<int> EquipmentCost = new List<int>();
                foreach (DataRow row in EquipmentCostResult.Rows)
                {
                    EquipmentCost.Add(Convert.ToInt32(row["EquipmentCost"]));
                }


                var FoodResult = BookingDetailsRepos.Getfoodcost(BookingNo);
                List<int> Foodcost = new List<int>();
                foreach (DataRow row in FoodResult.Rows)
                {
                    Foodcost.Add(Convert.ToInt32(row["FoodCost"]));
                }

                var LightResult = BookingDetailsRepos.GetLightCost(BookingNo);
                List<int> Lighhtcost = new List<int>();
                foreach (DataRow row in LightResult.Rows)
                {
                    Lighhtcost.Add(Convert.ToInt32(row["Lightcost"]));
                }

                var flowerResult = BookingDetailsRepos.GetflowerCost(BookingNo);
                List<int> flowercost = new List<int>();
                foreach (DataRow row in flowerResult.Rows)
                {
                    flowercost.Add(Convert.ToInt32(row["FlowerCost"]));
                }

                var bookingdetails = BookingDetailsRepos.getbookingdetails(BookingNo);
                string bookingNo = string.Empty;
                DateTime bookingDate = DateTime.MinValue;

                if (bookingdetails.Rows.Count > 0)
                {
                    bookingNo = Convert.ToString(bookingdetails.Rows[0]["BookingNo"]);

                    string bookingDateString = Convert.ToString(bookingdetails.Rows[0]["BookingDate"]);
                    bookingDate = DateTime.Parse(bookingDateString);
                }

                var model = new
                {
                    IsSuccess = true,
                    FoodCost = Foodcost,
                    VenueCost = venuCost,
                    EquipmentCost = EquipmentCost,
                    LightCost = Lighhtcost,
                    FlowerCost = flowercost,
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
    }
}
