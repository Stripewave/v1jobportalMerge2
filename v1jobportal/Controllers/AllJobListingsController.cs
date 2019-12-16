using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using v1jobportal.Data;
using v1jobportal.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

namespace v1jobportal.Controllers
{
    [Authorize]
    public class AllJobListingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        SqlConnection con = new SqlConnection("Server=OLYMPUS-CLOUD\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True");
        public AllJobListingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult JobManagement()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AllJobListings.ToListAsync());
        }

        public async Task<IActionResult> ViewJobs()
        {
            return View(await _context.AllJobListings.ToListAsync());
        }

        public async Task<IActionResult> AllJobLinks()
        {
            return View(await _context.AllJobListings.ToListAsync());
        }

        public IActionResult ApplicantData()
        {
            return View();
        }



        // GET: AllJobListings
        [AllowAnonymous]
        public async Task<IActionResult> ApplicantData(int? id_imputed)
        {
            if (id_imputed == null)
            {
                return NotFound();
            }

            var AllJobCandidates = await _context.SubmittedApplication.FirstOrDefaultAsync(m => m.Jd_JobId == id_imputed);

            if (AllJobCandidates == null)
            {
                return NotFound();
            }

            return View(AllJobCandidates);

        }

        // GET: AllJobListings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allJobListings = await _context.AllJobListings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allJobListings == null)
            {
                return NotFound();
            }

            return View(allJobListings);
        }

        /// <summary>
        /// Return view ID
        /// </summary>
        /// <param name="allJobListings"></param>
        /// <returns></returns>
        /// 

        public async Task<IActionResult> ViewId(int? id)
        {
            var allJobListings = await _context.AllJobListings.FindAsync(id);
            if (allJobListings == null)
            {
                return NotFound();
            }
            return View("ApplicationBioData/Create");  
        }

        public IActionResult GenarateJobLinks()
        {
            ViewBag.JobID_Found = "https://localhost:44384/AllJobListings/JobLink?camelot=" + Request.Cookies["loged_job"].ToString() + "&hydra_ramela=" + EncryptString("command-1", "E546C8DF278CD5931069B522E695D4F2") + "xctm-route=" + EncryptString("cracken-hades", "E546C8DF278CD5931069B522E695D4F2");

            return View();
        }


        public IActionResult Edit()
        {
            return View();
        }


        // GET: AllJobListings/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllJobListings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        
        public async Task<IActionResult> Create([Bind("Id,JobTitle,DutyStation,Duties,Qualifications,Deadline,JobId")] AllJobListings allJobListings)
        {

            if (ModelState.IsValid)
            {
                _context.Add(allJobListings);
                await _context.SaveChangesAsync();

                int last_id_inserted = allJobListings.Id;

                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("loged_job", last_id_inserted.ToString());

                return RedirectToAction("GenarateJobLinks", "AllJobListings");

            }

            return View(allJobListings);
            
        }

        // GET: AllJobListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allJobListings = await _context.AllJobListings.FindAsync(id);
            if (allJobListings == null)
            {
                return NotFound();
            }
            return View(allJobListings);
            
        }

        // POST: AllJobListings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,DutyStation,Duties,Qualifications,Deadline")] AllJobListings allJobListings)
        {
            if (id != allJobListings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allJobListings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllJobListingsExists(allJobListings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(allJobListings);
        }
        //get id of clicked job
        

        // GET: AllJobListings/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allJobListings = await _context.AllJobListings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allJobListings == null)
            {
                return NotFound();
            }

            return View(allJobListings);
        }

        // POST: AllJobListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allJobListings = await _context.AllJobListings.FindAsync(id);
            _context.AllJobListings.Remove(allJobListings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllJobListingsExists(int id)
        {
            return _context.AllJobListings.Any(e => e.Id == id);
        }

        ///Importing code from Admin controller
        ///

        public static string DecryptSystem(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }



        /*public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }*/

        public async Task<IActionResult> DisplayJobContent_ByID(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AvailableJob_Data = await _context.AllJobListings.FindAsync(id);
            if (AvailableJob_Data == null)
            {
                return NotFound();
            }
            return View(AvailableJob_Data);
        }



        // public string JobLink(string camelot)
        [AllowAnonymous]
        public async Task<IActionResult> JobLink(string camelot)
        {
         
            var IntJobID = Convert.ToInt32(camelot);

            string query = "SELECT * FROM AllJobListings WHERE Id=" + IntJobID + "";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                var TY = cmd.ExecuteReader();

                ArrayList JobData = new ArrayList();

                while (TY.Read())
                {
                    object[] values = new object[TY.FieldCount];
                    TY.GetValues(values);
                    JobData.Add(values);
                }

                TY.Close();
                con.Close();

                string jsonString;
                jsonString = JsonSerializer.Serialize(JobData);

                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("applying_job", jsonString);

                con.Close();
            }

            return View();
        }


        public static string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        public IActionResult GenarateJobLinksF()
        {
            var request = HttpContext.Request;
            ViewBag.JobID_Found = "https://localhost:44384/Admin/JobLink?camelot=" + EncryptString(Request.Cookies["loged_job"].ToString(), "E546C8DF278CD5931069B522E695D4F2") + "&hydra_ramela=" + EncryptString("command-1", "E546C8DF278CD5931069B522E695D4F2") + "xctm-route=" + EncryptString("cracken-hades", "E546C8DF278CD5931069B522E695D4F2");

            return View(ViewBag.JobID_Found);
        }
    }
}
