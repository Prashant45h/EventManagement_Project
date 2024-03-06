namespace EventManagement_Project.Models
{
    public class FoodModel
    {
        public int FoodID { get; set; }
        public string? FoodType { get; set; }
        public string? MealType { get; set;}
        public string? DishType { get; set; }
        public string? FoodName { get; set; }
        public string? FoodFileName { get; set; }
        public string? FoodFilepath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Createdate { get; set; }
        public int FoodCost { get; set; }
    }
}
