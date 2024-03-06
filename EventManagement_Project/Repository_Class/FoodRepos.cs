using EventManagement_Project.Models;
using System.Data.SqlClient;
using System.Data;

namespace EventManagement_Project.Repository_Class
{
    public class FoodRepos
    {
        private static string Sqlconn;

        public FoodRepos(string Sqlconn)
        {
            FoodRepos.Sqlconn = Sqlconn;
        }

        public static List<DishTypeModel> Featchdishtype()
        {
            List<DishTypeModel> dishetype = new List<DishTypeModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Sqlconn))
                {
                    connection.Open();

                    string query = "select ID,Dishtype from Dishtypes";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DishTypeModel list = new DishTypeModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Dishtype = Convert.ToString(reader["Dishtype"]),
                                };
                                dishetype.Add(list);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return dishetype;
        }

        public static bool Fooddatasave(string[] Foodmodel, IFormFile file, string uploadDirectory,string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(Sqlconn))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_InsertFood", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("FoodType", SqlDbType.NVarChar).Value = Foodmodel[1];
                        cmd.Parameters.Add("MealType", SqlDbType.NVarChar).Value = Foodmodel[2];
                        cmd.Parameters.Add("DishType", SqlDbType.NVarChar).Value = Foodmodel[3];
                        cmd.Parameters.Add("FoodName", SqlDbType.NVarChar).Value = Foodmodel[4];
                        cmd.Parameters.Add("FoodFileName", SqlDbType.NVarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("FoodFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("FoodCost", SqlDbType.Int).Value = Foodmodel[8];
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
        public static bool EditFood(string[] Foodmodel, IFormFile file, string uploadDirectory, string filePathInDatabase)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(Sqlconn))
                {
                    Sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_UpdateFood", Sqlcon))
                    {
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("FoodID", SqlDbType.Int).Value = Foodmodel[0];
                        cmd.Parameters.Add("FoodType", SqlDbType.NVarChar).Value = Foodmodel[1];
                        cmd.Parameters.Add("MealType", SqlDbType.NVarChar).Value = Foodmodel[2];
                        cmd.Parameters.Add("DishType", SqlDbType.NVarChar).Value = Foodmodel[3];
                        cmd.Parameters.Add("FoodName", SqlDbType.NVarChar).Value = Foodmodel[4];
                        cmd.Parameters.Add("FoodFileName", SqlDbType.NVarChar).Value = file.FileName;

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        cmd.Parameters.Add("FoodFilepath", SqlDbType.NVarChar).Value = filePathInDatabase;

                        cmd.Parameters.Add("CreatedBy", SqlDbType.Int).Value = Team.storedRoleId;
                        cmd.Parameters.Add("FoodCost", SqlDbType.Int).Value = Foodmodel[8];
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

        public static DataTable GetFoodDataForEdit(string Dishedit)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Food_Tbl where FoodID = @FoodID";
                using (SqlConnection sqlConnection = new SqlConnection(Sqlconn))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@FoodID", SqlDbType.VarChar).Value = Convert.ToInt32(Dishedit);
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


        public static DataTable GetFoodDetails()
        {
            DataTable dt = new();
            try
            {
                string query = "select * from Food_Tbl";

                using (SqlConnection sqlConnection = new SqlConnection(Sqlconn))
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

        public static bool deletedata(string deletefood)
        {
            try
            {
                string query = "DELETE FROM Food_Tbl WHERE FoodID = @FoodID";

                using (SqlConnection sqlConnection = new SqlConnection(Sqlconn))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@FoodID", SqlDbType.Int).Value = Convert.ToString(deletefood);
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
