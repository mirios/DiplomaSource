using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using verifyPlatform.Models;

namespace verifyPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListGroup> ListGroups { get; set; }
        
    }
}
