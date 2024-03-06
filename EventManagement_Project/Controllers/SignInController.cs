using System.Data;
using EventManagement_Project.Models;
using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Project.Controllers
{
    public class SignInController : Controller
    {
        
       private readonly SignIn_Repo signInRepo;
        public SignInController(SignIn_Repo signInRepo)
        {
           this.signInRepo = signInRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
     
        public async Task<ActionResult> UserLogin(string RegistrationModel)
        {
            try
            {
                string[]? registrationModel = JsonConvert.DeserializeObject<string[]>(RegistrationModel);

                var loginResult = SignIn_Repo.IsValidLogin(registrationModel);

                if (registrationModel != null && loginResult.IsValid)
                {
                    int ID = loginResult.ID;
                    string Name = loginResult.Name;
                    string Role = loginResult.Role;

                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("Role", Role);
                    HttpContext.Session.SetInt32("Id", ID);

                    Team.Rolename = Role;
                    Team.storedRoleId = ID;
                    if (Role == "Admin")
                    {
                        return Json(new { IsSuccess = true, RedirectUrl = "/AdminDashboard/Dashboard" });
                    }
                    else if (Role == "Customer")
                    {
                        return Json(new { IsSuccess = true, RedirectUrl = "/CustomerDashboard/Dashboard" });
                    }
                    else if (Role == "SuperAdmin")
                    {
                        return Json(new { IsSuccess = true, RedirectUrl = "/SuperAdminDashboard/Dashboard" });
                    }   
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "Unknown role" });
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false });
                }
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(string RegistrationModel)
        {
            try
            {
                string[]? registrationModel = JsonConvert.DeserializeObject<string[]>(RegistrationModel);

                if (SignIn_Repo.IsEmailOrUsernameExists(registrationModel[7], registrationModel[8]))
                {
                    return Json(new { IsSuccess = false, Message = "This Email Is already Registered" });
                }

                var resultAdd = SignIn_Repo.RegisterUser(registrationModel);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = "Registration Success" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Eror Occured While Registartion" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }
        [HttpGet]
        public ActionResult Getstates(int Countryid)
        {
            try
            {
                List<StateModel> state = SignIn_Repo.GetStatenames(Countryid);
                if(state.Count > 0)
                {
                    return Json(new { isSuccess = true, stateList = state });
                }
                else
                {
                    return Json(new { isSuccess = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isSuccess = false });
        }
        [HttpGet]
        public ActionResult Getcities(int stateid)
        {
            try
            {
                List<CityModel> city = SignIn_Repo.GetCitynames(stateid);
                if (city.Count > 0)
                {
                    return Json(new { isSuccess = true, citylist = city });
                }
                else
                {
                    return Json(new { isSuccess = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isSuccess = false });
        }

        public IActionResult Users()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }
        [HttpPost]
        public IActionResult LoadUsers()
        {
            try
            {
                var result = SignIn_Repo.GetUserdetails(Team.storedRoleId);
                if (result.Rows.Count > 0)
                {
                    var List = new List<UserDetailsModel>();
                    foreach (DataRow dr in result.Rows)
                    {
                        UserDetailsModel Obj = new UserDetailsModel();
                        Obj.Name = Convert.ToString(dr["Name"]);
                        Obj.Username = Convert.ToString(dr["Username"]);
                        Obj.Mobileno = Convert.ToString(dr["Mobile_No"]);
                        Obj.EmailID = Convert.ToString(dr["Email_ID"]);
                        Obj.Gender = Convert.ToString(dr["Gender"]);
                        Obj.Birthdate = Convert.ToDateTime(dr["BirthDate"]);
                        Obj.CountryName = Convert.ToString(dr["Country_Name"]);
                        Obj.StateName = Convert.ToString(dr["State_Name"]);
                        Obj.CityName = Convert.ToString(dr["City_Name"]);
                        Obj.Address = Convert.ToString(dr["Address"]);
                        Obj.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

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

        public IActionResult Forgotpassword()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["Name"] = name;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> updatappassword(string DATA)
        {
            try
            {
                string[]? username = JsonConvert.DeserializeObject<string[]>(DATA);

                var resultAdd = SignIn_Repo.passwordupdate(username);

                if (resultAdd)
                {
                    return Json(new { IsSuccess = true, Message = " update success" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "An Username Is Not Exist" });
                }
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Message = "An unexpected error occurred" });
            }
        }
        public IActionResult SignUp()
        {
			List<CountryModel> countries = SignIn_Repo.GetCounty();
			TempData["countries"] = countries;

            List<Role_Model> Roles = SignIn_Repo.GetRoles();
            TempData["Roles"] = Roles;
            return View();
        }

        public IActionResult Login()
        {
			
			return View();
        }
    }
}
