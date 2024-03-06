namespace EventManagement_Project.Models
{
    public class FlowerModel
    {
        public int FlowerID { get; set; }
        public string? FlowerName { get; set; }
        public string? FlowerFilename { get; set; }
        public string? FlowerFilepath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Createdate { get; set;}
        public int FlowerCost { get; set;}


    }
}
