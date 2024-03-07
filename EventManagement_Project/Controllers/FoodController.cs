using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace EventManagement_Project.Controllers
{
    public class FoodController : Controller
    {

        private readonly FoodRepos FoodRepos;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public FoodController(FoodRepos FoodRepos, IWebHostEnvironment hostingEnvironment)
        {
            FoodRepos = FoodRepos;
            _hostingEnvironment = hostingEnvironment;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveFood(IFormFile txtfoodFilepath)
        {
            try
            {
                string[] Foodmodel = HttpContext.Request.Form["foodmodel[]"];

                var uploadDirectory = Path.Combine("wwwroot", "FoodImages");
                Directory.CreateDirectory(uploadDirectory);

                if (txtfoodFilepath != null && txtfoodFilepath.Length > 0)
                {

                    var fileName = Path.GetFileName(txtfoodFilepath.FileName);
                    var filePathInDatabase = Path.Combine("/FoodImages", fileName);

                    if (!string.IsNullOrEmpty(Foodmodel[0]))
                    {
                        var result = FoodRepos.GetFoodDataForEdit(Foodmodel[0]);
                        if (result.Rows.Count > 0)
                        {
                            var resultUpdate = FoodRepos.EditFood(Foodmodel, txtfoodFilepath, uploadDirectory, filePathInDatabase);
                            var filePath = Path.Combine(uploadDirectory, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await txtfoodFilepath.CopyToAsync(fileStream);
                            }
                            if (resultUpdate)
                            {
                                return Json(new { IsSuccess = true, Message = "Data update success" });
                            }
                            else
                            {
                                return Json(new { IsSuccess = false, Message = "Something went wrong while updating details" });
                            }
                        }
                    }
                    else
                    {
                        var resultAdd = FoodRepos.Fooddatasave(Foodmodel, txtfoodFilepath, uploadDirectory, filePathInDatabase);
                        var filePath = Path.Combine(uploadDirectory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await txtfoodFilepath.CopyToAsync(fileStream);
                        }
                        if (resultAdd)
                        {
                            return Json(new { IsSuccess = true, Message = "Success" });
                        }
                        else
                        {
                            return Json(new { IsSuccess = false, Message = "Error Occurred" });
                        }
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Plese Upload A File" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
            return Json(new { IsSuccess = false, Message = "Invalid request" });
        }

        [HttpGet]
        public ActionResult Getdataforedit(string FoodID)
        {
            try
            {
                var result = FoodRepos.GetFoodDataForEdit(FoodID);
                if (result.Rows.Count > 0)
                {
                    var DataList = new List<FoodModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        FoodModel Obj = new FoodModel();
                        Obj.FoodID = Convert.ToInt32(dr[("FoodID")]);
                        Obj.FoodType = Convert.ToString(dr["FoodType"]);
                        Obj.MealType = Convert.ToString(dr["MealType"]);
                        Obj.DishType = Convert.ToString(dr["DishType"]); 
                        Obj.FoodName = Convert.ToString(dr[("FoodName")]);
                        Obj.FoodFileName = Convert.ToString(dr[("FoodFileName")]);
                        Obj.FoodFilepath = Convert.ToString(dr[("FoodFilepath")]);
                        Obj.FoodCost = Convert.ToInt32(dr[("FoodCost")]);
                        DataList.Add(Obj);
                    }
                    return Json(new { IsSuccess = true, dataList = DataList.FirstOrDefault() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
            return Json(new { IsSuccess = false, Message = "No data found for editing" });
        }

        public IActionResult FoodDetails()
        {
            List<DishTypeModel> dishtype = FoodRepos.Featchdishtype();
            TempData["dishtype"] = dishtype;
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

        [HttpPost]
        public IActionResult LoadallDishes()
        {
            try
            {
                var result = FoodRepos.GetFoodDetails();
                if (result.Rows.Count > 0)
                {
                    var List = new List<FoodModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        FoodModel Obj = new FoodModel();
                        Obj.FoodID = Convert.ToInt32(dr["FoodID"]);
                        Obj.FoodType = Convert.ToString(dr["FoodType"]);
                        Obj.MealType = Convert.ToString(dr["MealType"]);
                        Obj.DishType = Convert.ToString(dr["DishType"]);
                        Obj.FoodName = Convert.ToString(dr["FoodName"]);
                        Obj.FoodFileName = Convert.ToString(dr["FoodFileName"]);
                        Obj.FoodFilepath = Convert.ToString(dr[("FoodFilepath")]);
                        Obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                        Obj.FoodCost = Convert.ToInt32(dr["FoodCost"]);
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

        //[HttpPost]
        //public ActionResult Deletefood(string deletedish)
        //{
        //    try
        //    {
        //        if (deletedish != "")
        //        {
        //            var result = FoodRepos.deletedata(deletedish);
        //            if (result)
        //            {
        //                return Json(new { IsSuccess = true, Message = "Record is deleted!" });
        //            }
        //            else
        //            {
        //                return Json(new { IsSuccess = false, Message = "Something went wrong while deleting record!" });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Json(new { IsSuccess = false, Message = "Something went wrong, Please try again later!" });
        //}
        [HttpPost]
        public ActionResult Deletefood(string deletedish)
        {
            try
            {
                string connectionstring = "Server=DESKTOP-10T1J5F\\SQLEXPRESS;Database=DB_EventManagment;User Id=Ivory;Password=Ivory@2024";
                string selectQuery = "SELECT FoodFilePath FROM Food_Tbl WHERE FoodID = @FoodID";
                string filePath = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    selectCommand.Parameters.AddWithValue("@FoodID", deletedish);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePath = reader["FoodFilePath"].ToString().Replace("\\", "/");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(filePath))
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    filePath = Path.Combine(webRootPath, filePath.TrimStart('/'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    else
                    {
                        Console.WriteLine($"File does not exist: {filePath}");
                    }
                }
                else
                {
                    Console.WriteLine("File path is empty or null.");
                }

                FoodRepos.deletedata(deletedish);

                return Json(new { IsSuccess = true, Message = "Record is deleted!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file and record: {ex.Message}");
                return Json(new { IsSuccess = false, Message = "Something went wrong, Please try again later!" });
            }
        }

    }
}
