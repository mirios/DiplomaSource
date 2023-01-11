using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace verifyPlatform.Models
{
    public class ListGroup
    {
        [Key]
        public int Id { get; set; }
        public string ListName { get; set; }
        public string Status { get; set; }
        public string RulesTitle { get; set; }
        public string RulesGEO { get; set; }
        public string RulesEmplpyeesSize { get; set; }
        public string RulesRevenueSize { get; set; }
        public string RulesIndustrie { get; set; }
    }
}
