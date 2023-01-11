using Microsoft.AspNetCore.Identity;

namespace verifyPlatform.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
