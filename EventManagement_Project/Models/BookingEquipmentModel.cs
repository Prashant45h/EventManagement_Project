namespace EventManagement_Project.Models
{
    public class BookingEquipmentModel
    {
        public int BookingEquipmentID { get; set; }
        public int EquipmentID { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set;}
        public int BookingID { get; set; }
    }
}
