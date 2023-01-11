using Microsoft.AspNetCore.Identity;
using verifyPlatform.Models;



namespace verifyPlatform.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Company
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(new List<Company>()
                    {
                        new Company()
                        {
                            Name = "Microsoft",
                            EmplSize = "100000",
                            EmplSizeProof = "yahoo.finance",
                            RevenueSize = "100000",
                            RevenueSizeProof = "yahoo.finance",
                            Industry = "Software",
                            Phone = "+ 1 406 571 23-49",
                            Comment = "Some text"
                        },
                        new Company()
                        {
                            Name = "Microsoft",
                            EmplSize = "100000",
                            EmplSizeProof = "yahoo.finance",
                            RevenueSize = "100000",
                            RevenueSizeProof = "yahoo.finance",
                            Industry = "Software",
                            Phone = "+ 1 406 571 23-49",
                            Comment = "Some text"
                        }
                    });
                    context.SaveChanges();
                }

                // Lead
                if (!context.Leads.Any())
                {
                    context.Leads.AddRange(new List<Lead>()
                    {
                        new Lead()
                        {
                            FirstName = "Jon",
                            LastName = "O'Neel",
                            Email = "jon.oneel@microsoft.com",
                            Title = "Marketing Specialist",
                            Proof = "linkedin.com",
                            Country = "USA",
                            State = "MA",
                            City = "Boston",
                            Streeat = "Brodvey Str.",
                            ZipCode = "10031",
                            CompanyId = 1,
                            Comment = "Some text"
                         },
                        new Lead()
                        {
                            FirstName = "Jon",
                            LastName = "O'Neel",
                            Email = "jon.oneel@microsoft.com",
                            Title = "Marketing Specialist",
                            Proof = "linkedin.com",
                            Country = "USA",
                            State = "MA",
                            City = "Boston",
                            Streeat = "Brodvey Str.",
                            ZipCode = "10031",
                            CompanyId = 2,
                            Comment = "Some text"
                         }
                    });
                    context.SaveChanges();
                }

                
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {



                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "miroslawwlasenko@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "miroslawwlasenko",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        FirstName = "Miroslav",
                        LastName = "Vlasenko"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        FirstName = "Miroslav",
                        LastName = "Vlasenko"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}