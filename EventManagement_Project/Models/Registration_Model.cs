namespace EventManagement_Project.Models
{
    public class Registration_Model
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string? Mobile_No { get; set; }
        public string? Email_ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int RoleID { get; set; }
        public DateTime CreatedOn { get; set; }





    }
}
