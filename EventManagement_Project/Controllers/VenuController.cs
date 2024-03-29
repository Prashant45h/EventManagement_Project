﻿using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace EventManagement_Project.Controllers
{
    public class VenuController : Controller
    {
        private readonly VenuRepos VenuRepos;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public VenuController(VenuRepos venuRepos, IWebHostEnvironment hostingEnvironment)
        {
            venuRepos = venuRepos;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Venu()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveVenu(IFormFile txtvenuFilepath, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                if (txtvenuFilepath == null)
                {
                    return Json(new { IsSuccess = false, Message = "Please Upload A File" });
                }

                string[] venumodel = HttpContext.Request.Form["venumodel[]"];

                var uploadDirectory = Path.Combine(hostingEnvironment.WebRootPath, "VenuImages");
                Directory.CreateDirectory(uploadDirectory);

                var fileName = Path.GetFileName(txtvenuFilepath.FileName);
                var filePathInDatabase = Path.Combine("/VenuImages", fileName);
   
                if (!string.IsNullOrEmpty(venumodel[0]))
                {
                    var result = VenuRepos.GetVenuDataForEdit(venumodel[0]);
                    if (result.Rows.Count > 0)
                    {
                        var resultUpdate = VenuRepos.EditVenu(venumodel, txtvenuFilepath, uploadDirectory, filePathInDatabase);
                        var filePath = Path.Combine(uploadDirectory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await txtvenuFilepath.CopyToAsync(fileStream);
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
                    var resultAdd = VenuRepos.Venudatasave(venumodel, txtvenuFilepath, uploadDirectory, filePathInDatabase);
                    var filePath = Path.Combine(uploadDirectory, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await txtvenuFilepath.CopyToAsync(fileStream);
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
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
            return Json(new { IsSuccess = false, Message = "Invalid request" });
        }


        public IActionResult VenuDetails()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }
        [HttpPost]
        public IActionResult LoadallVenus()
        {
            try
            {
                var result = VenuRepos.GetVenuDetails();
                if (result.Rows.Count > 0)
                {
                    var List = new List<VenuModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        VenuModel Obj = new VenuModel();
                        Obj.VenuID = Convert.ToInt32(dr["VenuID"]);
                        Obj.VenuName = Convert.ToString(dr["VenuName"]);
                        Obj.VenuFilename = Convert.ToString(dr["VenuFilename"]);
                        Obj.VenuFilepath = Convert.ToString(dr[("VenuFilepath")]);
                        Obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                        Obj.VenuCost = Convert.ToInt32(dr["VenuCost"]);
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
        public ActionResult Getdataforedit(string VenuID)
        {
            try
            {
                var result = VenuRepos.GetVenuDataForEdit(VenuID);
                if (result.Rows.Count > 0)
                {
                    var DataList = new List<VenuModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        VenuModel Obj = new VenuModel();
                        Obj.VenuID = Convert.ToInt32(dr[("VenuID")]);
                        Obj.VenuName = Convert.ToString(dr[("VenuName")]);
                        Obj.VenuFilename = Convert.ToString(dr[("VenuFilename")]);
                        Obj.VenuFilepath = Convert.ToString(dr[("VenuFilepath")]);
                        Obj.VenuCost = Convert.ToInt32(dr[("VenuCost")]);
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
        //public ActionResult DeleteVenuData(string deletevenu)
        //{
        //    try
        //    {
        //        if (deletevenu != "")
        //        {

        //            var result = VenuRepos.deletedata(deletevenu);
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
        public ActionResult DeleteVenuData(string deletevenu)
        {
            try
            {
                string connectionstring = "Server=DESKTOP-10T1J5F\\SQLEXPRESS;Database=DB_EventManagment;User Id=Ivory;Password=Ivory@2024";
                string selectQuery = "SELECT VenuFilepath FROM Venu_Tbl WHERE VenuID = @VenuID";
                string filePath = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    selectCommand.Parameters.AddWithValue("@VenuID", deletevenu);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePath = reader["VenuFilepath"].ToString().Replace("\\", "/");
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

                VenuRepos.deletedata(deletevenu);

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
