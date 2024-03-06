namespace EventManagement_Project.Models
{
    public class BookingDetailsModel
    {
        public int BookingID { get; set; }

        public string Name { get; set; }
        public string? BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public int CreatedBy { get; set;  }
        public DateTime CreatedDate { get; set;}
        public string? BookingApprovel { get; set; }
        public DateTime? BookingApprovelDate { get; set; }
        public string? BookingCompleteFlag { get; set; }

    }
}
