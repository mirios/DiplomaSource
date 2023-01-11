using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace verifyPlatform.Models
{
    public class List
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Leads")]
        public int LeadId { get; set; }
        public Lead Lead { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string Verdict { get; set; }
    }
}
