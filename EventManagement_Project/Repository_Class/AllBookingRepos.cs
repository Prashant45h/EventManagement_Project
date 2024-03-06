using System.Data.SqlClient;
using System.Data;

namespace EventManagement_Project.Repository_Class
{
    public class AllBookingRepos
    {
        private static string SQLCONNECTION;

        public AllBookingRepos(string SQLCONNECTION)
        {
            AllBookingRepos.SQLCONNECTION = SQLCONNECTION;
        }

        public static DataTable GetBookingDetails(int roleid)
        {
            DataTable dt = new();
            try
            {
                string query = "SELECT * FROM BookingDetails_Tbl where CreatedBy = @CreatedBy";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", roleid);


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
     
        public static DataTable Getvenudata(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (V.VenuCost),(V.VenuName)  FROM BookingVenu_Tbl BV INNER JOIN Venu_Tbl V ON BV.VenuID = V.VenuID WHERE BV.BookingID = (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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
        public static DataTable GetEquipmentdata(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT (E.EquipmentCost), (E.EquipmentName) FROM BookingEquipment_Tbl BE INNER JOIN Equipment_Tbl E ON BE.EquipmentID = E.EquipmentID WHERE BE.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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
        public static DataTable Getfooddata(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT F.FoodCost,F.FoodType,F.MealType,F.FoodName FROM BookingFood_Tbl BF INNER JOIN Food_Tbl F ON BF.DishName = F.FoodID WHERE BF.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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
        public static DataTable GetLightsdata(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT L.LightType,L.LightName, L.LightCost FROM BookingLights_Tbl BL INNER JOIN Light_Tbl L ON BL.LightIDSelected = L.LightID WHERE BL.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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

        public static DataTable Getflowerdata(string BookingNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT FL.FlowerName,FL.FlowerCost FROM BookingFlower_Tbl BF INNER JOIN Flower_Tbl FL ON BF.FlowerID = FL.FlowerID WHERE BF.BookingID =  (SELECT BookingID FROM BookingDetails_Tbl WHERE BookingNo = @BookingNo)";

                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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
                string query = "select BookingID,BookingNo,BookingDate From BookingDetails_Tbl Where BookingNo = @BookingNO ";
                using (SqlConnection sqlConnection = new SqlConnection(SQLCONNECTION))
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
        public static bool Canclled(string details)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(SQLCONNECTION))
                {
                    Sqlcon.Open();

                    string query = "UPDATE BookingDetails_Tbl SET BookingApprovel = @BookingApprovel,BookingApprovelDate = GETDATE() WHERE BookingID = @BookingID;";

                    using (SqlCommand cmd = new SqlCommand(query, Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("BookingID", SqlDbType.VarChar).Value = details;
                        cmd.Parameters.Add("BookingApprovel", SqlDbType.VarChar).Value = "Cancelled";
                        cmd.Parameters.Add("BookingApprovelDate", SqlDbType.VarChar).Value = details;


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
