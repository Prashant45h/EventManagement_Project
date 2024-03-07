using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EventManagement_Project.Controllers
{
    public class FlowerController : Controller
    {
        private readonly FlowerRepos FlowerRepos;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public FlowerController(FlowerRepos FlowerRepos, IWebHostEnvironment hostingEnvironment)
        {
            FlowerRepos = FlowerRepos;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveFlower(IFormFile txtFlowerFilepath)
        {
            try
            {
                string[] Flowermodel = HttpContext.Request.Form["flowermodel[]"];

                var uploadDirectory = Path.Combine("wwwroot", "FlowerImages");
                Directory.CreateDirectory(uploadDirectory);

                if (txtFlowerFilepath != null && txtFlowerFilepath.Length > 0)
                {

                    var fileName = Path.GetFileName(txtFlowerFilepath.FileName);
                    var filePathInDatabase = Path.Combine("/FlowerImages", fileName);

                    if (!string.IsNullOrEmpty(Flowermodel[0]))
                    {
                        var result = FlowerRepos.GetFlowerDataForEdit(Flowermodel[0]);
                        if (result.Rows.Count > 0)
                        {
                            var resultUpdate = FlowerRepos.EditFlower(Flowermodel, txtFlowerFilepath, uploadDirectory, filePathInDatabase);
                            var filePath = Path.Combine(uploadDirectory, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await txtFlowerFilepath.CopyToAsync(fileStream);
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
                        var resultAdd = FlowerRepos.Flowerdatasave(Flowermodel, txtFlowerFilepath, uploadDirectory, filePathInDatabase);
                        var filePath = Path.Combine(uploadDirectory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await txtFlowerFilepath.CopyToAsync(fileStream);
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
        public IActionResult FlowerDetails()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }
        [HttpPost]
        public IActionResult LoadallFlowers()
        {
            try
            {
                var result = FlowerRepos.GetFlowerDetails();
                if (result.Rows.Count > 0)
                {
                    var List = new List<FlowerModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        FlowerModel Obj = new FlowerModel();
                        Obj.FlowerID = Convert.ToInt32(dr["FlowerID"]);
                        Obj.FlowerName = Convert.ToString(dr["FlowerName"]);
                        Obj.FlowerFilename = Convert.ToString(dr["FlowerFilename"]);
                        Obj.FlowerFilepath = Convert.ToString(dr[("FlowerFilepath")]);
                        Obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                        Obj.FlowerCost = Convert.ToInt32(dr["FlowerCost"]);
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
        public ActionResult Getdataforedit(string FlowerID)
        {
            try
            {
                var result = FlowerRepos.GetFlowerDataForEdit(FlowerID);
                if (result.Rows.Count > 0)
                {
                    var DataList = new List<FlowerModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        FlowerModel Obj = new FlowerModel();
                        Obj.FlowerID = Convert.ToInt32(dr[("FlowerID")]);
                        Obj.FlowerName = Convert.ToString(dr[("FlowerName")]);
                        Obj.FlowerFilename = Convert.ToString(dr[("FlowerFilename")]);
                        Obj.FlowerFilepath = Convert.ToString(dr[("FlowerFilepath")]);
                        Obj.FlowerCost = Convert.ToInt32(dr[("FlowerCost")]);
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

        //public ActionResult DeleteFlowerData(string deleteFlower)
        //{
        //    try
        //    {
        //        if (deleteFlower != "")
        //        {
        //            var result = FlowerRepos.deletedata(deleteFlower);
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
        public ActionResult DeleteFlowerData(string deleteFlower)
        {
            try
            {
                string connectionstring = "Server=DESKTOP-10T1J5F\\SQLEXPRESS;Database=DB_EventManagment;User Id=Ivory;Password=Ivory@2024";
                string selectQuery = "SELECT FlowerFilePath FROM Flower_Tbl WHERE FlowerID = @FlowerID";
                string filePath = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    selectCommand.Parameters.AddWithValue("@FlowerID", deleteFlower);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePath = reader["FlowerFilePath"].ToString().Replace("\\", "/");
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

                FlowerRepos.deletedata(deleteFlower);

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
