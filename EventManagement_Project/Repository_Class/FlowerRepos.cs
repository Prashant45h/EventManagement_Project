using EventManagement_Project.Models;
using System.Data.SqlClient;
using System.Data;

namespace EventManagement_Project.Repository_Class
{
    public class FlowerRepos
    {

        private static string _SqlConn;

        public FlowerRepos(string _SqlConn)
        {
            FlowerRepos._SqlConn = _SqlConn;
        }
        public static bool Flowerdatasave(string[] Flowermodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_SqlConn))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_InsertFlower", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("FlowerName", SqlDbType.VarChar).Value = Flowermodel[1];
                        cmd.Parameters.Add("FlowerFilename", SqlDbType.VarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("FlowerFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("FlowerCost", SqlDbType.Int).Value = Flowermodel[5];
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool EditFlower(string[] Flowermodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_SqlConn))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateFlower", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("FlowerID", SqlDbType.VarChar).Value = Flowermodel[0];
                        cmd.Parameters.Add("FlowerName", SqlDbType.VarChar).Value = Flowermodel[1];
                        cmd.Parameters.Add("FlowerFilename", SqlDbType.VarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("FlowerFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("FlowerCost", SqlDbType.Int).Value = Flowermodel[5];
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

        public static DataTable GetFlowerDataForEdit(string Floweredit)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Flower_Tbl where FlowerID = @FlowerID";
                using (SqlConnection sqlConnection = new SqlConnection(_SqlConn))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@FlowerID", SqlDbType.VarChar).Value = Convert.ToInt32(Floweredit);
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

        public static DataTable GetFlowerDetails()
        {
            DataTable dt = new();
            try
            {
                string query = "select * from Flower_Tbl";

                using (SqlConnection sqlConnection = new SqlConnection(_SqlConn))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
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

        public static bool deletedata(string deleteFlower)
        {
            try
            {
                string query = "DELETE FROM Flower_Tbl WHERE FlowerID = @FlowerID";

                using (SqlConnection sqlConnection = new SqlConnection(_SqlConn))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@FlowerID", SqlDbType.Int).Value = Convert.ToString(deleteFlower);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
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
