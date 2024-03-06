using EventManagement_Project.Models;
using System.Data.SqlClient;
using System.Data;

namespace EventManagement_Project.Repository_Class
{
    public class EquipmentRepos
    {
        private static string _connString;

        public EquipmentRepos(string connectionString)
        {
            EquipmentRepos._connString = connectionString;
        }
        public static bool Equipmentdatasave(string[] Equipmentmodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connString))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_InsertEquipment", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("EquipmentName", SqlDbType.VarChar).Value = Equipmentmodel[1];
                        cmd.Parameters.Add("EquipmentFilename", SqlDbType.VarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("EquipmentFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("EquipmentCost", SqlDbType.Int).Value = Equipmentmodel[5];
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

        public static bool EditEquipment(string[] Equipmentmodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connString))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_UpdateEquipment", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("EquipmentID", SqlDbType.VarChar).Value = Equipmentmodel[0];
                        cmd.Parameters.Add("EquipmentName", SqlDbType.VarChar).Value = Equipmentmodel[1];
                        cmd.Parameters.Add("EquipmentFilename", SqlDbType.VarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("EquipmentFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("EquipmentCost", SqlDbType.Int).Value = Equipmentmodel[5];
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

        public static DataTable GetEquipmentDataForEdit(string Equipmentedit)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Equipment_Tbl where EquipmentID = @EquipmentID";
                using (SqlConnection sqlConnection = new SqlConnection(_connString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@EquipmentID", SqlDbType.VarChar).Value = Convert.ToInt32(Equipmentedit);
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
        public static DataTable GetEquipmentDetails()
        {
            DataTable dt = new();
            try
            {
                string query = "select * from Equipment_Tbl";

                using (SqlConnection sqlConnection = new SqlConnection(_connString))
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
        public static bool deletedata(string deleteEquipment)
        {
            try
            {
                string query = "DELETE FROM Equipment_Tbl WHERE EquipmentID = @EquipmentID";

                using (SqlConnection sqlConnection = new SqlConnection(_connString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@EquipmentID", SqlDbType.Int).Value = Convert.ToString(deleteEquipment);
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
