using System.ComponentModel.DataAnnotations;

namespace EventManagement_Project.Models
{
    public class CountryModel
    {
        [Key]
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}