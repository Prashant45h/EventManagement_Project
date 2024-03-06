using EventManagement_Project.Models;
using System.Data.SqlClient;
using System.Data;

namespace EventManagement_Project.Repository_Class
{
    public class LightingsRepos
    {
        private static string Con;

        public LightingsRepos(string Con)
        {
            LightingsRepos.Con = Con;
        }
        public static bool Lightingdatasave(string[] Lightmodel, IFormFile file, string uploadDirectory,string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(Con))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_InsertLight", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("LightType", SqlDbType.NVarChar).Value = Lightmodel[1];
                        cmd.Parameters.Add("LightName", SqlDbType.NVarChar).Value = Lightmodel[2];
                        cmd.Parameters.Add("LightFilename", SqlDbType.NVarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("LightFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("LightCost", SqlDbType.Int).Value = Lightmodel[6];
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

        public static bool EditLightings(string[] Lightmodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(Con))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateLight", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("LightID", SqlDbType.Int).Value = Lightmodel[0];
                        cmd.Parameters.Add("LightType", SqlDbType.NVarChar).Value = Lightmodel[1];
                        cmd.Parameters.Add("LightName", SqlDbType.NVarChar).Value = Lightmodel[2];
                        cmd.Parameters.Add("LightFilename", SqlDbType.NVarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("LightFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("LightCost", SqlDbType.Int).Value = Lightmodel[6];
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

        public static DataTable GetLightingDataForEdit(string light)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Light_Tbl where LightID = @LightID";
                using (SqlConnection sqlConnection = new SqlConnection(Con))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@LightID", SqlDbType.VarChar).Value = Convert.ToInt32(light);
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

        public static DataTable GetLightingDetails()
        {
            DataTable dt = new();
            try
            {
                string query = "select * from Light_Tbl";

                using (SqlConnection sqlConnection = new SqlConnection(Con))
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
        public static bool deletedata(string deletedata)
        {
            try
            {
                string query = "DELETE FROM Light_Tbl WHERE LightID = @LightID";

                using (SqlConnection sqlConnection = new SqlConnection(Con))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@LightID", SqlDbType.Int).Value = Convert.ToString(deletedata);
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
