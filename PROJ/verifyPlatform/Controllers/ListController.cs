using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using verifyPlatform.Data;
using verifyPlatform.Models;
using Microsoft.Net.Http.Headers;

namespace verifyPlatform.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            List<ListGroup> listGroups = _context.ListGroups.ToList();
            return View(listGroups);
        }

        public JsonResult CreateList(string[] choices)
        {

            double unixTime = (DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            int unixTimeInt = Convert.ToInt32(unixTime);
            string utunixTimeString = Convert.ToString(unixTimeInt);
            string NameList = choices[0] + utunixTimeString;
            string StringConnection = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PlatformOnlineVerefy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string Command = @"DECLARE @TableName nvarchar(max) = '" + NameList + "';" +
                "DECLARE @Sql NVARCHAR(max) = 'create table '" +
                " + @TableName +'([Id][int] PRIMARY KEY IDENTITY(1, 1) NOT NULL," +
                "[LeadId] [int] NOT NULL," +
                "[Status] [nvarchar] (max) NULL," +
                "[UserName][nvarchar] (max) NULL," +
                "[Verdict][nvarchar] (max) NULL)';" +
                "EXECUTE sp_executesql @Sql";
            string sql = string.Format("Insert Into ListGroups" +
                   "(ListName, Status, RulesTitle, RulesGEO, RulesEmplpyeesSize, RulesRevenueSize, RulesIndustrie) Values(@ListName, @Status, @RulesTitle, @RulesGEO, @RulesEmplpyeesSize, @RulesRevenueSize, @RulesIndustrie)");
            try
            {
                SqlConnection conn = new SqlConnection(StringConnection);
                SqlCommand cmd = new SqlCommand(Command, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                using (SqlCommand cmdInsert = new SqlCommand(sql, conn))
                {
                    // add parameters
                    cmdInsert.Parameters.AddWithValue("@ListName", NameList);
                    cmdInsert.Parameters.AddWithValue("@Status", "No");
                    cmdInsert.Parameters.AddWithValue("@RulesTitle", choices[1]);
                    cmdInsert.Parameters.AddWithValue("@RulesGEO", choices[2]);
                    cmdInsert.Parameters.AddWithValue("@RulesEmplpyeesSize", choices[3]);
                    cmdInsert.Parameters.AddWithValue("@RulesRevenueSize", choices[4]);
                    cmdInsert.Parameters.AddWithValue("@RulesIndustrie", choices[5]);
                    cmdInsert.ExecuteNonQuery();
                }
                Console.WriteLine("Rules Insert");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string CommandKey = @"DECLARE @TableName nvarchar(max) = '" + NameList + "';" +
                "DECLARE @Sql1 NVARCHAR(max) = 'ALTER TABLE '+@TableName+' WITH CHECK ADD  CONSTRAINT [FK_'+@TableName+'_Leads_Id] FOREIGN KEY([LeadId])';" +
                "EXECUTE sp_executesql @Sql1";
            try
            {
                SqlConnection conn = new SqlConnection(StringConnection);
                SqlCommand cmdkey = new SqlCommand(CommandKey, conn);
                cmdkey.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(choices);
        }

        public JsonResult DeleteList(string[] choices)
        {
            string NameList = choices[0];
            string StringConnection = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PlatformOnlineVerefy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string dellist = string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + NameList + "]') AND type in (N'U')) " +
                "DROP TABLE " + NameList + "");
            string delstr = string.Format("DELETE FROM ListGroups WHERE ListName = '" + NameList + "'");
            try
            {
                SqlConnection conn = new SqlConnection(StringConnection);
                conn.Open();
                using (SqlCommand cmdDel = new SqlCommand(dellist, conn))
                {
                    //add 
                    cmdDel.ExecuteNonQuery();
                }
                using (SqlCommand cmdDel = new SqlCommand(delstr, conn))
                {
                    //add parameters
                    cmdDel.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(choices);
        }

        public JsonResult ViewList(string[] choices)
        {
            string NameList = choices[0];
            string StringConnection = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PlatformOnlineVerefy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string viewrules = string.Format("SELECT [RulesTitle], " +
                "[RulesGEO], " +
                "[RulesEmplpyeesSize]," +
                "[RulesRevenueSize]," +
                "[RulesIndustrie] FROM ListGroups WHERE ListName = '" + NameList + "'");
            try
            {
                using (SqlConnection conn = new SqlConnection(StringConnection))
                {
                    conn.Open();
                    // add parameters
                    SqlCommand cmdViewRules = new SqlCommand(viewrules, conn);
                    SqlDataReader reader = cmdViewRules.ExecuteReader();
                    if (reader.HasRows) // is isset data
                    {
                        while (reader.Read()) // read in line
                        {
                            HttpContext.Response.Cookies.Append("RulesTitle", reader.GetValue(0).ToString());
                            HttpContext.Response.Cookies.Append("RulesGEO", reader.GetValue(1).ToString());
                            HttpContext.Response.Cookies.Append("RulesEmplpyeesSize", reader.GetValue(2).ToString());
                            HttpContext.Response.Cookies.Append("RulesRevenueSize", reader.GetValue(3).ToString());
                            HttpContext.Response.Cookies.Append("RulesIndustrie", reader.GetValue(4).ToString());
                            HttpContext.Response.Cookies.Append("ListName", choices[0]);
                            Console.WriteLine();
                        }
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var cookie = new CookieHeaderValue("id", "12345");
            Console.WriteLine(HttpContext.Request.Cookies["RulesTitle"]);
            return Json("text");
        }
     }
}
