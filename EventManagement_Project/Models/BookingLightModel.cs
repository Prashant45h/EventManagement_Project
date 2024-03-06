namespace EventManagement_Project.Models
{
    public class BookingLightModel
    {
        public int BookingLightID { get; set; }
        public string? LightType { get;}
        public int LightIDSelected { get; set; }
        public int BookingID { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set;}


    }
}
