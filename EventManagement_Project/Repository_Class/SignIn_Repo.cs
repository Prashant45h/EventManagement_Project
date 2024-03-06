using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using EventManagement_Project.Models;

namespace EventManagement_Project.Repository_Class
{
    public class SignIn_Repo
    {
        private static string connectionString;

        public SignIn_Repo(string connectionString)
        {
            SignIn_Repo.connectionString = connectionString;
        }
        public static (bool IsValid, string Name, string Role,int ID) IsValidLogin(string[] RegistrationModel)
        {
            string query = "SELECT R.Id, R.Name, Ro.RoleName FROM Registartion_Table R INNER JOIN Role_Tbl Ro ON R.RoleId = Ro.Role_ID WHERE R.Username = @Username AND R.Password = @Password;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = RegistrationModel[0];
                sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = RegistrationModel[1];


                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string Name = reader.GetString(1);
                        string Role = reader.GetString(2);
                        return (true, Name, Role, Id);
                    }
                    else
                    {
                        return (false, "", "",0);
                    }
                }
            }
        }

        public static bool IsEmailOrUsernameExists(string email, string username)
        {
            string query = "SELECT COUNT(*) FROM Registartion_Table WHERE Email_ID = @Email_ID OR Username = @Username;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.Add("@Email_ID", SqlDbType.VarChar).Value = email;
                sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;

                int count = (int)sqlCommand.ExecuteScalar();

                return count > 0;
            }
        }
        public static bool RegisterUser(string[] RegistrationModel)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connectionString))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("Sp_Insertdataforsignup", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = RegistrationModel[1];
                        cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = RegistrationModel[2];
                        cmd.Parameters.Add("Country", SqlDbType.Int).Value = RegistrationModel[3];
                        cmd.Parameters.Add("State", SqlDbType.Int).Value = RegistrationModel[4];
                        cmd.Parameters.Add("City", SqlDbType.Int).Value = RegistrationModel[5];
                        cmd.Parameters.Add("Mobile_No", SqlDbType.VarChar).Value = RegistrationModel[6];
                        cmd.Parameters.Add("Email_ID", SqlDbType.VarChar).Value = RegistrationModel[7];
                        cmd.Parameters.Add("Username", SqlDbType.VarChar).Value = RegistrationModel[8];
                        cmd.Parameters.Add("Password", SqlDbType.VarChar).Value = RegistrationModel[9];
                        cmd.Parameters.Add("ConfirmPassword", SqlDbType.VarChar).Value = RegistrationModel[10];
                        cmd.Parameters.Add("Gender", SqlDbType.VarChar).Value = RegistrationModel[11];
                        cmd.Parameters.Add("BirthDate", SqlDbType.DateTime).Value = RegistrationModel[12];
                        cmd.Parameters.Add("RoleID", SqlDbType.Int).Value = RegistrationModel[13];
                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static List<Role_Model> GetRoles()
        {
            List<Role_Model> roles = new List<Role_Model>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "select * from Role_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Role_Model Role = new Role_Model
                                {
                                    RoleID = Convert.ToInt32(reader["Role_ID"]),
                                    Rolename = Convert.ToString(reader["RoleName"]),
                                };
                                roles.Add(Role);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetch data", ex);
            }
            return roles;
        }
        public static List<CountryModel> GetCounty()
        {
            List<CountryModel> country = new List<CountryModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "select * from Country_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CountryModel counties = new CountryModel
                                {
                                    CountryID = Convert.ToInt32(reader["Country_ID"]),
                                    CountryName = Convert.ToString(reader["Country_Name"]),

                                };
                                country.Add(counties);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetch data", ex);
            }
            return country;
        }
        public static List<StateModel> GetStatenames(int Countryid)
        {
            List<StateModel> states = new List<StateModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM State_Tbl WHERE Country_ID = @Country_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Country_ID", Countryid);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StateModel state = new StateModel
                                {
                                    StateID = Convert.ToInt32(reader["State_ID"]),
                                    StateName = Convert.ToString(reader["State_Name"]),
                                    CountryID = Convert.ToInt32(reader["Country_ID"]),
                                };
                                states.Add(state);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetch data", ex);
            }
            return states;
        }
        public static List<CityModel> GetCitynames(int stateid)
        {
            List<CityModel> cities = new List<CityModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM City_Tbl WHERE State_ID = @State_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@State_ID", stateid);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CityModel city = new CityModel
                                {
                                    CityID = Convert.ToInt32(reader["City_ID"]),
                                    CityName = Convert.ToString(reader["City_Name"]),
                                    StateID = Convert.ToInt32(reader["State_ID"]),
                                };
                                cities.Add(city);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetch data", ex);
            }
            return cities;
        }
        public static DataTable GetUserdetails(int roleid)
        {
            DataTable dt = new();
            try
            {
                string query = "SELECT RT.Name,RT.Username,RT.Mobile_No,RT.Email_ID,RT.Gender,RT.BirthDate, CT.Country_Name,ST.State_Name,CTY.City_Name,RT.Address,RT.CreatedOn FROM   Registartion_Table RT JOIN   City_Tbl CTY ON RT.City = CTY.City_ID JOIN    State_Tbl ST ON RT.State = ST.State_ID JOIN   Country_Tbl CT ON RT.Country = CT.Country_ID where ID = @ID ";
                 
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", roleid);

                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Connection = sqlConnection;
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public static bool passwordupdate(string[] username)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connectionString))
                {
                    Sqlcon.Open();
                    string query = "UPDATE Registartion_Table SET Password = @Password, ConfirmPassword = @ConfirmPassword WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, Sqlcon))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("Username", SqlDbType.VarChar).Value = username[0];
                        cmd.Parameters.Add("Password", SqlDbType.VarChar).Value = username[1];
                        cmd.Parameters.Add("ConfirmPassword", SqlDbType.VarChar).Value = username[2];
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                        if (rowsAffected == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}