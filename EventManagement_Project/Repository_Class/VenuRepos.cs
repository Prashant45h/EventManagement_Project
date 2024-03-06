using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using EventManagement_Project.Models;

namespace EventManagement_Project.Repository_Class
{
    public class VenuRepos
    {
        private static string _connectionString;

        public VenuRepos(string connectionString)
        {
            VenuRepos._connectionString = connectionString;
        }

		public static bool Venudatasave(string[] venumodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
		{
			try
			{
				using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
				{
					Sqlcon.Open();

					using (SqlCommand cmd = new SqlCommand("SP_InsertVenu", Sqlcon))
					{
						cmd.Connection = Sqlcon;
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("VenuName", SqlDbType.VarChar).Value = venumodel[1];
						cmd.Parameters.Add("VenuFilename", SqlDbType.VarChar).Value = file.FileName;

						string filePath = Path.Combine(uploadDirectory, file.FileName);
						cmd.Parameters.Add("VenuFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

						cmd.Parameters.Add("Cretatedby", SqlDbType.Int).Value = Team.storedRoleId;
						cmd.Parameters.Add("VenuCost", SqlDbType.Int).Value = venumodel[5];
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


		public static bool EditVenu(string[] venumodel, IFormFile file, string uploadDirectory,string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_UpdateVenu", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("VenuID", SqlDbType.VarChar).Value = venumodel[0];
                        cmd.Parameters.Add("VenuName", SqlDbType.VarChar).Value = venumodel[1];
                        cmd.Parameters.Add("VenuFilename", SqlDbType.VarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("VenuFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("Cretatedby", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("VenuCost", SqlDbType.Int).Value = venumodel[5];
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

        public static DataTable GetVenuDataForEdit(string Venuedit)
        {
            DataTable dt = new DataTable();
            try
                {
                string query = "select * from Venu_Tbl where VenuID = @VenuID";
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@VenuID", SqlDbType.VarChar).Value = Convert.ToInt32(Venuedit);
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

        public static DataTable GetVenuDetails()
        {
            DataTable dt = new();
            try
            {
                string query = "select * from Venu_Tbl";

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
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

        public static bool deletedata(string deletevenu)
        {
            try
            {
                string query = "DELETE FROM Venu_Tbl WHERE VenuID = @VenuID";

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@VenuID", SqlDbType.Int).Value = Convert.ToString(deletevenu);
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

        //public static bool deletedata(string deletevenu)
        //{
        //    try
        //    {
        //        string queryImagePath = "SELECT VenuFilepath FROM Venu_Tbl WHERE VenuID = @VenuID";
        //        string imagePath = null;

        //        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        //        using (SqlCommand sqlCommandImagePath = new SqlCommand(queryImagePath, sqlConnection))
        //        {
        //            sqlConnection.Open();
        //            sqlCommandImagePath.CommandType = CommandType.Text;
        //            sqlCommandImagePath.Parameters.Add("@VenuID", SqlDbType.Int).Value = Convert.ToInt32(deletevenu);
        //            object result = sqlCommandImagePath.ExecuteScalar();
        //            if (result != null && result != DBNull.Value)
        //            {
        //                imagePath = ((string)result).Replace("\\", "/");
        //            }

        //            sqlConnection.Close();
        //        }

        //        if (!string.IsNullOrEmpty(imagePath))
        //        {
        //            if (System.IO.File.Exists(imagePath))
        //            {
        //                System.IO.File.Delete(imagePath);
        //            }
        //            else
        //            {
        //                throw new FileNotFoundException("Image file not found at the specified path.", imagePath);
        //            }
        //        }

        //        string queryDelete = "DELETE FROM Venu_Tbl WHERE VenuID = @VenuID";

        //        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        //        using (SqlCommand sqlCommandDelete = new SqlCommand(queryDelete, sqlConnection))
        //        {
        //            sqlConnection.Open();
        //            sqlCommandDelete.CommandType = CommandType.Text;
        //            sqlCommandDelete.Parameters.Add("@VenuID", SqlDbType.Int).Value = Convert.ToInt32(deletevenu);
        //            sqlCommandDelete.ExecuteNonQuery();
        //            sqlConnection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //        return false;
        //    }
        //    return true;
        //}



    }
}
