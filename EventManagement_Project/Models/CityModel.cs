using System.ComponentModel.DataAnnotations;

namespace EventManagement_Project.Models
{
    public class CityModel
    {
        [Key]
        public int CityID { get; set; }
        public string? CityName { get; set; }
        public int? StateID { get; set; }

    }
}
