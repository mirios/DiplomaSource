using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace verifyPlatform.Models
{
    public class Lead
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Proof { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Streeat { get; set; }
        public string ZipCode { get; set; }
        [ForeignKey("Companies")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string? Comment { get; set; }
    }
}
