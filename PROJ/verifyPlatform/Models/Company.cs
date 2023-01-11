using System.ComponentModel.DataAnnotations;

namespace verifyPlatform.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmplSize { get; set; }
        public string EmplSizeProof { get; set; }
        public string RevenueSize { get; set; }
        public string RevenueSizeProof { get; set; }
        public string Industry { get; set; }
        public string Phone { get; set; }
        public string? Comment { get; set; }
    }

}
