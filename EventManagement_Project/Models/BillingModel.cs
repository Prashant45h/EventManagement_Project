namespace EventManagement_Project.Models
{
    public class BillingModel
    {
        public string? BookingNo { get; set; }
        public int? BookingID { get; set; }
        public string? BookingDate { get; set; }
        public int? TotalVenueCost { get; set; }
        public int? TotalEquipmentCost { get; set; }
        public int? TotalFoodCost { get; set; }
        public int? TotalFlowerCost { get; set; }
        public int? TotalLightCost { get; set; }
        public int? TotalAmount { get; set; }
    }
}
