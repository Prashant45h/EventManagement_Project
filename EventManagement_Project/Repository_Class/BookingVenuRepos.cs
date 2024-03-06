using System.Data;
using System.Data.SqlClient;
using EventManagement_Project.Models;

namespace EventManagement_Project.Repository_Class
{
    public class BookingVenuRepos
    {
        private static string _Connection;

        public BookingVenuRepos(string _Connection)
        {
            BookingVenuRepos._Connection = _Connection;
        }
        public static List<Eventtypemoedel> GetEventType()
        {
            List<Eventtypemoedel> Events = new List<Eventtypemoedel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_Connection))
                {
                    connection.Open();

                    string query = "SELECT EventID, EventType  FROM EventType_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Eventtypemoedel Event = new Eventtypemoedel
                                {
                                    EventID = Convert.ToInt32(reader["EventID"]),
                                    EventType = Convert.ToString(reader["EventType"]),
                                };
                                Events.Add(Event);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return Events;
        }
        public static List<VenuModel> GetVenus()
        {
            List<VenuModel> Venus = new List<VenuModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_Connection))
                {
                    connection.Open();

                    string query = "SELECT VenuID, VenuName ,VenuFilepath FROM Venu_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VenuModel venu = new VenuModel
                                {
                                    VenuID = Convert.ToInt32(reader["VenuID"]),
                                    VenuName = Convert.ToString(reader["VenuName"]),
                                    VenuFilepath = Convert.ToString(reader["VenuFilepath"]),
                                };
                                Venus.Add(venu);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return Venus;
        }
        public static bool ISDateAvailableorNot(string date, string Venuid)
        {
            string query = "SELECT COUNT(*) FROM BookingVenu_Tbl WHERE Createdate = @Createdate AND VenuID = @VenuID";

            using (SqlConnection sqlConnection = new SqlConnection(_Connection))
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.Add("@Createdate", SqlDbType.VarChar).Value = date;
                sqlCommand.Parameters.Add("@VenuID", SqlDbType.VarChar).Value = Venuid;

                int count = (int)sqlCommand.ExecuteScalar();

                return count == 0;
            }
        }




        public static bool SaveBookings(string[] VenuBookmodel)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_Connection))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SaveBookingVenue", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EventTypeID", SqlDbType.Int).Value = int.Parse(VenuBookmodel[1]);
                        cmd.Parameters.Add("@VenuID", SqlDbType.Int).Value = int.Parse(VenuBookmodel[2]);
                        cmd.Parameters.Add("@GuestCount", SqlDbType.Int).Value = int.Parse(VenuBookmodel[3]);
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("@Createdate", SqlDbType.DateTime).Value = DateTime.Parse(VenuBookmodel[5]);

                        SqlParameter outputParameter = new SqlParameter();
                        outputParameter.ParameterName = "@BookingID";
                        outputParameter.SqlDbType = SqlDbType.Int;
                        outputParameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParameter);

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
    }

}