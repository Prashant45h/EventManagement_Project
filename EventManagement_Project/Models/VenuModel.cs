namespace EventManagement_Project.Models
{
    public class VenuModel
    {
        public int VenuID { get; set; }
        public string? VenuName { get; set;}
        public string? VenuFilename { get; set; }
        public string VenuFilepath { get; set; } = string.Empty;
        public int Cretatedby { get; set; }
        public DateTime Createdate { get; set; }
        public int VenuCost { get; set; }
    }
}
    