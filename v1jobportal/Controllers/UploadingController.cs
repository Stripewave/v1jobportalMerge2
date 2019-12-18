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
        public async Task<IActionResult> ProcessUpload(ICollection<IFormFile> applicant_cv, string JobId, string EmpId)
        {
            var uploads = Path.Combine("uploads");
            foreach (var file in applicant_cv)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, EmpId+file.FileName), FileMode.Create))
                    {
                        var SavePath = "uploads/" + EmpId + file.FileName;
                        string query = "INSERT INTO ApplicantDocuments(Id,Jd_JobId,Emp_Id,UploadCv,UploadCoverLetter) VALUES('"+file.Length+"','" + JobId + "','" + EmpId + "','" + SavePath + "','not-available')";

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
            return View();
        }


        public async Task<IActionResult> UpladCover(ICollection<IFormFile> cover_letter, string JobId, string EmpId)
        {
            var uploads = Path.Combine("uploads");
            foreach (var file in cover_letter)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, EmpId + file.FileName), FileMode.Create))
                    {
                        var SavePath = "uploads/" + EmpId + file.FileName;
                        string query = "UPDATE ApplicantDocuments SET UploadCoverLetter='"+SavePath+ "' WHERE Emp_Id="+EmpId+"";

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
            return View();
        }

        public async Task<IActionResult> ViewDocs(ICollection<IFormFile> cover_letter, string JobId, string EmpId)
        {
            var uploads = Path.Combine("uploads");
            foreach (var file in cover_letter)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, EmpId + file.FileName), FileMode.Create))
                    {
                        var SavePath = "uploads/" + EmpId + file.FileName;
                        string query = "UPDATE ApplicantDocuments SET UploadCoverLetter='" + SavePath + "' WHERE Emp_Id=" + EmpId + "";

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
            return View();
        }
    }
}