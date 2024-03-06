namespace EventManagement_Project.Models
{
    public class EquipmentModel
    {
        public int EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        public string? EquipmentFilename { get; set; }
        public string? EquipmentFilepath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Createdate { get; set;}
        public int EquipmentCost { get; set;}

    }
}
