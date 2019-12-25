using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using v1jobportal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using v1jobportal.Models;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace v1jobportal.Controllers
{
    public class UploadingController : Controller
    {
        private IHostingEnvironment _environment;

       
        public IActionResult Index()
        {
            return View();
        }

        SqlConnection con = new SqlConnection("Server=OLYMPUS-CLOUD\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True");

        public async Task<IActionResult> UploadingDocuments(string Bjid, string emid)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("_bid", Bjid);
            Response.Cookies.Append("_emid", emid);

            return View();
        }
        private readonly IHostingEnvironment _hostingEnv;


        [HttpPost]
        public async Task<IActionResult> ProcessUpload(IFormFile applicant_cv, string JobId, string EmpId)
        {
            if (applicant_cv == null || applicant_cv.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", applicant_cv.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                Random GEN = new Random();
                int ID_CORE = GEN.Next(100, 90000000);

                string query = "INSERT INTO ApplicantDocuments(Id,Jd_JobId,Emp_Id,UploadCv,UploadCoverLetter) VALUES('" + ID_CORE + "','" + JobId + "','" + EmpId + "','" + path + "','not-available')";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    con.Open();

                    var TY = await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
                await applicant_cv.CopyToAsync(stream);
            }
            ViewBag.UploadResponse = "Your File Was Was SuccesFully Uploaded, Thankyou!";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpladCover(IFormFile cover_letter, string JobId, string EmpId)
        {
            if (cover_letter == null || cover_letter.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", cover_letter.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                Random GEN = new Random();
                int ID_CORE = GEN.Next(100, 90000000);

                string query = "UPDATE ApplicantDocuments SET UploadCoverLetter='" + path + "' WHERE Emp_Id=" + EmpId + " AND Jd_JobId=" + JobId + "";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    con.Open();

                    var TY = await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
                await cover_letter.CopyToAsync(stream);
            }
            ViewBag.UploadResponse = "Your File Was Was SuccesFully Uploaded, Thankyou!";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UlternateUpload_Method(IFormFile applicant_cv, string JobId, string EmpId)
        {
            try
            {
                var uploads = Path.Combine("uploads");
                
                    if (applicant_cv.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, EmpId + applicant_cv.FileName), FileMode.Create))
                        {
                            var SavePath = "uploads/" + EmpId + applicant_cv.FileName;

                            Random GEN = new Random();
                            int ID_CORE = GEN.Next(100, 90000000);

                            string query = "INSERT INTO ApplicantDocuments(Id,Jd_JobId,Emp_Id,UploadCv,UploadCoverLetter) VALUES('" + ID_CORE + "','" + JobId + "','" + EmpId + "','" + SavePath + "','not-available')";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Connection = con;
                                con.Open();

                                var TY = await cmd.ExecuteNonQueryAsync();
                                con.Close();
                            }
                            await applicant_cv.CopyToAsync(fileStream);
                        }
                    }
                
                ViewBag.Response = "Your Document Was Uploaded SoccessFully, Thankyou!";
            }
            catch (Exception cxv)
            {
               
            }
            
            return View();
        }


        public async Task<IActionResult> UpladCover(ICollection<IFormFile> cover_letter, string JobId, string EmpId)
        {
            try
            {
                var uploads = Path.Combine("uploads");
                foreach (var file in cover_letter)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, EmpId + file.FileName), FileMode.Create))
                        {
                            var SavePath = "uploads/" + EmpId + file.FileName;
                            string query = "UPDATE ApplicantDocuments SET UploadCoverLetter='" + SavePath + "' WHERE Emp_Id=" + EmpId + " AND Jd_JobId=" + JobId + "";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Connection = con;
                                con.Open();

                                var TY = await cmd.ExecuteNonQueryAsync();
                                con.Close();
                            }
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
                ViewBag.Response = "Your Document Was Uploaded SoccessFully, Thankyou!";
            }
            catch (Exception bin_down)
            {

            }
            
            return View();
        }


        public async Task<IActionResult> Documents(string alfrea, string txp)
        {
                try
                {
                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("_cvls", alfrea);
                    Response.Cookies.Append("_cvls2", txp);
            }
                catch (Exception gh)
                {
                    //handle error gracefully
                }

                return View();
        }


        [HttpPost]
        public async Task<IActionResult> ManageDocuments(int Job_Id, string Candidate)
        {
            int GHM = Convert.ToInt32(Job_Id);
            string loadDocs_query = "SELECT * FROM ApplicantDocuments WHERE Jd_JobId='"+GHM+"' AND Emp_Id='"+Candidate+"'";

            using (SqlCommand cmd1 = new SqlCommand(loadDocs_query))
            {
                try
                {
                    cmd1.Connection = con;
                    con.Open();
                    var TY = cmd1.ExecuteReader();

                    ArrayList al = new ArrayList();

                    while (TY.Read())
                    {

                        object[] values = new object[TY.FieldCount];
                        TY.GetValues(values);
                        al.Add(values);
                    }

                    string jsonString;
                    jsonString = JsonSerializer.Serialize(al);
                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("_empdocs", jsonString);

                    TY.Close();
                    con.Close();

                }
                catch (Exception VB)
                {
                    //handle Error Gracefully
                }

            }
                return View();
        }
    }
}