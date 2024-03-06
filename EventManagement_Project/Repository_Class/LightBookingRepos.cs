using EventManagement_Project.Models;
using System.Data;
using System.Data.SqlClient;

namespace EventManagement_Project.Repository_Class
{
    public class LightBookingRepos
    {
        private static string _SQLCON;

        public LightBookingRepos(string _SQLCON)
        {
            LightBookingRepos._SQLCON = _SQLCON;
        }
        public static List<LightModel> Getlighting()
        {
            List<LightModel> Light = new List<LightModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_SQLCON))
                {
                    connection.Open();

                    string query = "select LightID,LightName,LightFilepath from Light_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LightModel LightBooking = new LightModel
                                {
                                    LightID = Convert.ToInt32(reader["LightID"]),
                                    LightName = Convert.ToString(reader["LightName"]),
                                    LightFilepath = Convert.ToString(reader["LightFilepath"]),

                                };
                                Light.Add(LightBooking);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return Light;
        }
        public static bool BookingLight(string[] Lightmodel, string[] lightingid )
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_SQLCON))
                {
                    Sqlcon.Open();

                    foreach (var lights in lightingid)
                    {

                        using (SqlCommand cmd = new SqlCommand("SP_Lightbooking", Sqlcon))
                        {
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@LightType", SqlDbType.NVarChar).Value = Lightmodel[1];
                            cmd.Parameters.Add("@LightIDSelected", SqlDbType.Int).Value = lights;


                            SqlParameter outputParameter = new SqlParameter();
                            outputParameter.ParameterName = "@BookingID";
                            outputParameter.SqlDbType = SqlDbType.Int;
                            outputParameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outputParameter);

                            cmd.Parameters.Add("@Createdby", SqlDbType.Int).Value = Team.storedRoleId;


                            cmd.ExecuteNonQuery();

                        }

                    }
                    Sqlcon.Close();

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
