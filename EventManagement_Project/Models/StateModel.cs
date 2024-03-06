using System.ComponentModel.DataAnnotations;

namespace EventManagement_Project.Models
{
    public class StateModel
    {
        [Key]
        public int StateID { get; set; }
        public string? StateName { get; set; }
        public int? CountryID { get; set; }
    }
}
