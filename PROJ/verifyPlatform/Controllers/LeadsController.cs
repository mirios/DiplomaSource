using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using verifyPlatform.Data;
using verifyPlatform.Models;

namespace verifyPlatform.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Contact()
        {
            List<string> rules = new List<string>();
            rules.Insert(0, HttpContext.Request.Cookies["RulesTitle"]);
            rules.Insert(1, HttpContext.Request.Cookies["RulesGEO"]);
            rules.Insert(2, HttpContext.Request.Cookies["RulesEmplpyeesSize"]);
            rules.Insert(3, HttpContext.Request.Cookies["RulesRevenueSize"]);
            rules.Insert(4, HttpContext.Request.Cookies["RulesIndustrie"]);
            return View(rules);
        }

        private bool key;
        public JsonResult AddNewContact(string[] choices)
        {
            if (choices == null)
                Console.WriteLine("NULL");

            foreach (var company in _context.Companies.ToList())
            {
                if (company.Name == choices[10])
                {
                    Lead lead1 = new Lead
                    {
                        FirstName = choices[0],
                        LastName = choices[1],
                        Email = choices[2],
                        Title = choices[3],
                        Proof = choices[4],
                        Country = choices[5],
                        State = choices[6],
                        City = choices[7],
                        Streeat = choices[8],
                        ZipCode = choices[9],
                        CompanyId = company.Id
                    };
                    _context.Leads.AddRange(lead1);
                    _context.SaveChanges();
                    Console.WriteLine(company.Name);
                    key = true;
                    break;
                }
            }
            Console.WriteLine(key);

            if (key != true)
            {
                Company company1 = new Company
                {
                    Name = choices[10],
                    EmplSize = choices[11],
                    EmplSizeProof = choices[12],
                    RevenueSize = choices[13],
                    RevenueSizeProof = choices[14],
                    Industry = choices[15],
                    Phone = choices[16]
                };
                Lead lead1 = new Lead
                {
                    FirstName = choices[0],
                    LastName = choices[1],
                    Email = choices[2],
                    Title = choices[3],
                    Proof = choices[4],
                    Country = choices[5],
                    State = choices[6],
                    City = choices[7],
                    Streeat = choices[8],
                    ZipCode = choices[9],
                    Company = company1
                };
                _context.Companies.AddRange(company1);
                _context.Leads.AddRange(lead1);
                _context.SaveChanges();
            }
            string NameList = HttpContext.Request.Cookies["ListName"];
            string StringConnection = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PlatformOnlineVerefy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string seedlist = string.Format("Insert Into " + NameList + " (LeadId, Verdict) Values(@LeadId, @Verdict)");
            List<Lead> lead = _context.Leads.ToList();
            try
            {
                SqlConnection conn = new SqlConnection(StringConnection);
                conn.Open();
                foreach (var item in lead)
                {
                    using (SqlCommand cmdSeed = new SqlCommand(seedlist, conn))
                    {
                        if (item.Email == choices[2])
                        {
                            //Добавить параметры
                            cmdSeed.Parameters.AddWithValue("@LeadId", item.Id);
                            cmdSeed.Parameters.AddWithValue("@Verdict", choices[17]);
                            cmdSeed.ExecuteNonQuery();
                        }
                    }
                }
                Console.WriteLine("Table was Created!");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cook = HttpContext.Request.Cookies["RulesTitle"];
            return Json(cook);
        }
    }
}
