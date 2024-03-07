using EventManagement_Project.Models;
using System.Data;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EventManagement_Project.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly EquipmentRepos EquipmentRepos;
        private readonly IWebHostEnvironment _hostingEnvironment;

     
        public EquipmentController(EquipmentRepos EquipmentRepos, IWebHostEnvironment hostingEnvironment)
        {
            EquipmentRepos = EquipmentRepos;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveEquipment(IFormFile txtEquipmentFilepath)
         {
            try
            {
                string[] Equipmentmodel = HttpContext.Request.Form["Equipmentmodel[]"];

                var uploadDirectory = Path.Combine("wwwroot", "EquipmentImages");
                Directory.CreateDirectory(uploadDirectory);

                if (txtEquipmentFilepath != null && txtEquipmentFilepath.Length > 0)
                {
                    var fileName = Path.GetFileName(txtEquipmentFilepath.FileName);
                    var filePathInDatabase = Path.Combine("/EquipmentImages", fileName);

                    if (!string.IsNullOrEmpty(Equipmentmodel[0]))
                    {
                        var result = EquipmentRepos.GetEquipmentDataForEdit(Equipmentmodel[0]);
                        if (result.Rows.Count > 0)
                        {
                            var resultUpdate = EquipmentRepos.EditEquipment(Equipmentmodel, txtEquipmentFilepath, uploadDirectory, filePathInDatabase);
                            var filePath = Path.Combine(uploadDirectory, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await txtEquipmentFilepath.CopyToAsync(fileStream);
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
                        var resultAdd = EquipmentRepos.Equipmentdatasave(Equipmentmodel, txtEquipmentFilepath, uploadDirectory, filePathInDatabase);
                        var filePath = Path.Combine(uploadDirectory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await txtEquipmentFilepath.CopyToAsync(fileStream);
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
        public ActionResult Getdataforedit(string EquipmentID)
        {
            try
            {
                var result = EquipmentRepos.GetEquipmentDataForEdit(EquipmentID);
                if (result.Rows.Count > 0)
                {
                    var DataList = new List<EquipmentModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        EquipmentModel Obj = new EquipmentModel();
                        Obj.EquipmentId = Convert.ToInt32(dr["EquipmentId"]);
                        Obj.EquipmentName = Convert.ToString(dr["EquipmentName"]);
                        Obj.EquipmentFilename = Convert.ToString(dr["EquipmentFilename"]);
                        Obj.EquipmentFilepath = Convert.ToString(dr[("EquipmentFilepath")]);
                        Obj.EquipmentCost = Convert.ToInt32(dr["EquipmentCost"]);
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

        public IActionResult EquipmentDetails()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }

        [HttpPost]
        public IActionResult LoadallEquipment()
        {
            try
            {
                var result = EquipmentRepos.GetEquipmentDetails();
                if (result.Rows.Count > 0)
                {
                    var List = new List<EquipmentModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        EquipmentModel Obj = new EquipmentModel();
                        Obj.EquipmentId = Convert.ToInt32(dr["EquipmentId"]);
                        Obj.EquipmentName = Convert.ToString(dr["EquipmentName"]);
                        Obj.EquipmentFilename = Convert.ToString(dr["EquipmentFilepath"]);
                        Obj.EquipmentFilepath = Convert.ToString(dr[("EquipmentFilepath")]);
                        Obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                        Obj.EquipmentCost = Convert.ToInt32(dr["EquipmentCost"]);
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
        //public ActionResult DeleteEquipmentData(string deleteEquipment)
        //{
        //    try
        //    {
        //        if (deleteEquipment != "")
        //        {
        //            var result = EquipmentRepos.deletedata(deleteEquipment);
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
        public ActionResult DeleteEquipmentData(string deleteEquipment)
        {
            try
            {
                string connectionstring = "Server=DESKTOP-10T1J5F\\SQLEXPRESS;Database=DB_EventManagment;User Id=Ivory;Password=Ivory@2024";
                string selectQuery = "SELECT EquipmentFilePath FROM Equipment_Tbl WHERE EquipmentID = @EquipmentID";
                string filePath = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    selectCommand.Parameters.AddWithValue("@EquipmentID", deleteEquipment);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePath = reader["EquipmentFilePath"].ToString().Replace("\\", "/");
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

                EquipmentRepos.deletedata(deleteEquipment);

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