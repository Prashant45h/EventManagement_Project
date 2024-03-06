namespace EventManagement_Project.Models
{
    public class LightModel
    {
        public int LightID { get; set; }
        public string? LightType { get; set; }
        public string? LightName { get; set; }
        public string? LightFilename { get; set; }
        public string? LightFilepath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Createdate { get; set;}
        public int LightCost { get; set;}

    }
}
