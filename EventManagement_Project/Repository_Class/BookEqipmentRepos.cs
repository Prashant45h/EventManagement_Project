using EventManagement_Project.Models;
using System.Data;
using System.Data.SqlClient;

namespace EventManagement_Project.Repository_Class
{
    public class BookEqipmentRepos
    {
        private static string _sqlConnection;

        public BookEqipmentRepos(string _sqlConnection)
        {
            BookEqipmentRepos._sqlConnection = _sqlConnection;
        }

        public static List<EquipmentModel> FeatchEquipments()
        {
            List<EquipmentModel> Equipments = new List<EquipmentModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnection))
                {
                    connection.Open();

                    string query = "select EquipmentID,EquipmentName,EquipmentFilepath from Equipment_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EquipmentModel Equipmentslist = new EquipmentModel
                                {
                                    EquipmentId = Convert.ToInt32(reader["EquipmentID"]),
                                    EquipmentName = Convert.ToString(reader["EquipmentName"]),
                                    EquipmentFilepath = Convert.ToString(reader["EquipmentFilepath"]),

                                };
                                Equipments.Add(Equipmentslist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return Equipments;
        }

       
        public static bool BOOKEQIPMENT(List<int> EquipmentModel)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_sqlConnection))
                {
                    Sqlcon.Open();

                    foreach (int equipmentId in EquipmentModel)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_eqipmentbooking", Sqlcon))
                        {
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int).Value = equipmentId;
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;

                            SqlParameter outputParameter = new SqlParameter();
                            outputParameter.ParameterName = "@BookingID";
                            outputParameter.SqlDbType = SqlDbType.Int;
                            outputParameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outputParameter);
                            cmd.ExecuteNonQuery();

                            int newBookingID = Convert.ToInt32(outputParameter.Value);
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
