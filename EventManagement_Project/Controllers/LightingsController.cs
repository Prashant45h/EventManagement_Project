using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EventManagement_Project.Controllers
{
    public class LightingsController : Controller
    {
        private readonly LightingsRepos LightingsRepos;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public LightingsController(LightingsRepos LightingsRepos, IWebHostEnvironment hostingEnvironment)
        {
            LightingsRepos = LightingsRepos;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Lightingsave(IFormFile txtLightFilepath)
        {
            try
            {
                string[] Lightmodel = HttpContext.Request.Form["lightingmodel[]"];

                var uploadDirectory = Path.Combine("wwwroot", "LightingImages");
                Directory.CreateDirectory(uploadDirectory);

                if (txtLightFilepath != null && txtLightFilepath.Length > 0)
                {
                    var fileName = Path.GetFileName(txtLightFilepath.FileName);
                    var filePathInDatabase = Path.Combine("/LightingImages", fileName);

                    if (!string.IsNullOrEmpty(Lightmodel[0]))
                    {
                        var result = LightingsRepos.GetLightingDataForEdit(Lightmodel[0]);
                        if (result.Rows.Count > 0)
                        {
                            var resultUpdate = LightingsRepos.EditLightings(Lightmodel, txtLightFilepath, uploadDirectory, filePathInDatabase);
                            var filePath = Path.Combine(uploadDirectory, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await txtLightFilepath.CopyToAsync(fileStream);
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
                        var resultAdd = LightingsRepos.Lightingdatasave(Lightmodel, txtLightFilepath, uploadDirectory, filePathInDatabase);
                        var filePath = Path.Combine(uploadDirectory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await txtLightFilepath.CopyToAsync(fileStream);
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

        public IActionResult LightingsDetails()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

        [HttpPost]
        public IActionResult LoadLightings()
        {
            try
            {
                var result = LightingsRepos.GetLightingDetails();
                if (result.Rows.Count > 0)
                {
                    var List = new List<LightModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        LightModel Obj = new LightModel();
                        Obj.LightID = Convert.ToInt32(dr["LightID"]);
                        Obj.LightType = Convert.ToString(dr["LightType"]);
                        Obj.LightName = Convert.ToString(dr["LightName"]);
                        Obj.LightFilename = Convert.ToString(dr["LightFilename"]);
                        Obj.LightFilepath = Convert.ToString(dr[("LightFilepath")]);
                        Obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                        Obj.LightCost = Convert.ToInt32(dr["LightCost"]);
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
        public ActionResult Getdataforedit(string Light)
        {
            try
            {
                var result = LightingsRepos.GetLightingDataForEdit(Light);
                if (result.Rows.Count > 0)
                {
                    var DataList = new List<LightModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        LightModel Obj = new LightModel();
                        Obj.LightID = Convert.ToInt32(dr["LightID"]);
                        Obj.LightType = Convert.ToString(dr["LightType"]);
                        Obj.LightName = Convert.ToString(dr["LightName"]);
                        Obj.LightFilename = Convert.ToString(dr["LightFilename"]);
                        Obj.LightFilepath = Convert.ToString(dr[("LightFilepath")]);
                        Obj.LightCost = Convert.ToInt32(dr["LightCost"]);
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
        //[HttpPost]
        //public ActionResult DeleteLightsData(string deleteid)
        //{
        //    try
        //    {
        //        if (deleteid != "")
        //        {
        //            var result = LightingsRepos.deletedata(deleteid);
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
        public ActionResult DeleteLightsData(string deleteid)
        {
            try
            {
                string connectionstring = "Server=DESKTOP-10T1J5F\\SQLEXPRESS;Database=DB_EventManagment;User Id=Ivory;Password=Ivory@2024";
                string selectQuery = "SELECT LightFilePath FROM Light_Tbl WHERE LightID = @LightID";
                string filePath = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    selectCommand.Parameters.AddWithValue("@LightID", deleteid);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePath = reader["LightFilePath"].ToString().Replace("\\", "/");
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

                LightingsRepos.deletedata(deleteid);

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
