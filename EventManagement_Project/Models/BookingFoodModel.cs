namespace EventManagement_Project.Models
{
    public class BookingFoodModel
    {
        public int BookingFoodID { get; set; }
        public string? FoodType { get;}
        public string? MealType { get; set; }
        public int DishType { get; set; }
        public int DishName { get; set; }
        public int Createdby { get; set; }
        public DateTime CeatedDate { get; set; }
        public int BookingID { get; set; }

    }
}
