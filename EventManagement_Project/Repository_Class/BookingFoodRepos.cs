using EventManagement_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EventManagement_Project.Repository_Class
{
    public class BookingFoodRepos
    {
        private static string SQLCON;

        public BookingFoodRepos(string SQLCON)
        {
            BookingFoodRepos.SQLCON = SQLCON;
        }
        public static List<DishTypeModel> Featchdishtype()
        {
            List<DishTypeModel> dishetype = new List<DishTypeModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLCON))
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

        public static List<FoodModel> Featchdishes()
        {
            List<FoodModel> dishes = new List<FoodModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLCON))
                {
                    connection.Open();

                    string query = "select FoodID,FoodName,FoodFilepath from Food_Tbl";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FoodModel foodlist = new FoodModel
                                {
                                    FoodID = Convert.ToInt32(reader["FoodID"]),
                                    FoodName = Convert.ToString(reader["FoodName"]),
                                    FoodFilepath = Convert.ToString(reader["FoodFilepath"]),

                                };
                                dishes.Add(foodlist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error In fetching Data.", ex);
            }

            return dishes;
        }


        public static bool BookingFood(string[] FoodModel , string[] Foodlist)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(SQLCON))
                {
                    Sqlcon.Open();

                    foreach (var foodItem in Foodlist)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_Foodbooking", Sqlcon))
                        {
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@FoodType", SqlDbType.NVarChar).Value = FoodModel[1];
                            cmd.Parameters.Add("@MealType", SqlDbType.NVarChar).Value = FoodModel[2];
                            cmd.Parameters.Add("@DishType", SqlDbType.Int).Value = FoodModel[3];
                            cmd.Parameters.Add("@DishName", SqlDbType.Int).Value = int.Parse(foodItem);
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
