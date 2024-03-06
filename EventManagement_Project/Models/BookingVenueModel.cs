namespace EventManagement_Project.Models
{
    public class BookingVenueModel
    {
        public int BookingVenuID { get; set; }
        public int VenuID { get; set; }
        public int EventTypeID { get; set; }
        public int GuestCount { get;}
        public int Createdby { get; set; }
        public DateTime Createdate { get; set;}
        public int BookingID { get; set;}
    }
}
