using System.ComponentModel.DataAnnotations;

namespace EventManagement_Project.Models
{
    public class Role_Model
    {
        [Key]
        public int RoleID { get; set; }
        public string? Rolename { get; set; }
    }
}
