using System.Data.SqlClient;
using System.Data;
using EventManagement_Project.Models;

namespace EventManagement_Project.Repository_Class
{
    public class BookingDetailsRepos
    {
        private static string _CONNECTION;

        public BookingDetailsRepos(string _CONNECTION)
        {
            BookingDetailsRepos._CONNECTION = _CONNECTION;
        }

        public static DataTable GetBookingDetails(int roleid)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM BookingDetails_Tbl where CreatedBy = @CreatedBy";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", roleid);


                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Connection = sqlConnection;
                    da.SelectCommand = sqlCommand;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public static DataTable GetBookingDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT BookingDetails_Tbl.*, Registartion_Table.Name FROM BookingDetails_Tbl INNER JOIN Registartion_Table ON BookingDetails_Tbl.CreatedBy = Registartion_Table.ID";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Connection = sqlConnection;
                    da.SelectCommand = sqlCommand;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
        public static bool approved(string[] details)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_CONNECTION))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[approvedata]", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = details[0];
                        cmd.Parameters.Add("BookingDate", SqlDbType.DateTime).Value = details[1];
                        cmd.Parameters.Add("BookingApprovel", SqlDbType.VarChar).Value = "Approved";

                     
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

        public static bool REJECT(string[] details)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_CONNECTION))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[approvedata]", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = details[0];
                        cmd.Parameters.Add("BookingDate", SqlDbType.DateTime).Value = details[1];
                        cmd.Parameters.Add("BookingApprovel", SqlDbType.VarChar).Value = "Reject";


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

        public static DataTable Getvenucost(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (V.VenuCost) AS VenueCost FROM BookingVenu_Tbl BV INNER JOIN Venu_Tbl V ON BV.VenuID = V.VenuID WHERE BV.BookingID = (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public static DataTable GetEquipmentcost(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (E.EquipmentCost) AS EquipmentCost FROM BookingEquipment_Tbl BE INNER JOIN Equipment_Tbl E ON BE.EquipmentID = E.EquipmentID WHERE BE.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public static DataTable Getfoodcost(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (F.FoodCost) AS FoodCost FROM BookingFood_Tbl BF INNER JOIN Food_Tbl F ON BF.DishName = F.FoodID WHERE BF.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public static DataTable GetLightCost(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (L.LightCost) AS LightCost FROM BookingLights_Tbl BL INNER JOIN Light_Tbl L ON BL.LightIDSelected = L.LightID WHERE BL.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public static DataTable GetflowerCost(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (FL.FlowerCost) AS FlowerCost FROM BookingFlower_Tbl BF INNER JOIN Flower_Tbl FL ON BF.FlowerID = FL.FlowerID WHERE BF.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public static DataTable getbookingdetails(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select BookingNo,BookingDate From BookingDetails_Tbl Where BookingNo = @BookingNO ";
                using (SqlConnection sqlConnection = new SqlConnection(_CONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("BookingNo", SqlDbType.VarChar).Value = Convert.ToString(BookingNo);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.SelectCommand = sqlCommand;
                    sqlConnection.Close();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
    }
}
 