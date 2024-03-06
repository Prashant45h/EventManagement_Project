namespace EventManagement_Project.Models
{
    public class CompleteBookingModel
    {
        public VenuModel BookingVenue { get; set; }
        public EquipmentModel BookingEquipment { get; set; }
        public FoodModel BookingFood { get; set; }
        public FlowerModel BookingFlower { get; set; }
        public LightModel BookingLight { get; set; }
        public BillingModel BillingModel { get; set; }
    }
}
