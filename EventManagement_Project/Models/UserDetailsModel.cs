namespace EventManagement_Project.Models
{
    public class UserDetailsModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }

        public string CountryName { get; set; }

        public string Mobileno { get; set; }

        public string EmailID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public int? RoleID { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
