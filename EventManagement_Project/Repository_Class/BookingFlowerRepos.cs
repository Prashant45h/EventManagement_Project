using EventManagement_Project.Models;
using System.Data;
using System.Data.SqlClient;

namespace EventManagement_Project.Repository_Class
{
    public class BookingFlowerRepos
    {
        private static string _CONN;

        public BookingFlowerRepos(string _CONN)
        {
            BookingFlowerRepos._CONN = _CONN;
        }
        public static List<FlowerModel> Getflowerslist()
        {
            List<FlowerModel> Flowers = new List<FlowerModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_CONN))
                {
                    connection.Open();

                    string query = "select FlowerID,FlowerName,FlowerFilepath from Flower_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FlowerModel flowerslist = new FlowerModel
                                {
                                    FlowerID = Convert.ToInt32(reader["FlowerID"]),
                                    FlowerName = Convert.ToString(reader["FlowerName"]),
                                    FlowerFilepath = Convert.ToString(reader["FlowerFilepath"]),

                                };
                                Flowers.Add(flowerslist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return Flowers;
        }

        public static bool BookingFLower(string[] Model , string[] flowersselected)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_CONN))
                {
                    Sqlcon.Open();
                    foreach (var flowers in flowersselected)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_Flowerbooking", Sqlcon))
                        {
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add("@FlowerID", SqlDbType.Int).Value = flowers;

                            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;

                            SqlParameter outputParameter = new SqlParameter();
                            outputParameter.ParameterName = "@BookingID";
                            outputParameter.SqlDbType = SqlDbType.Int;
                            outputParameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outputParameter);
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
