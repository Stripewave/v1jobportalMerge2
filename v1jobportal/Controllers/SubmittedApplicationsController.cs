using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.Extensions.DependencyInjection;

namespace v1jobportal.Controllers
{
    public class SubmittedApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmittedApplicationsController(ApplicationDbContext context)
        {
            _context = context;
            var cloud = context;
        }
        SqlConnection con = new SqlConnection("Server=OLYMPUS-CLOUD\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True");



        // GET: SubmittedApplications
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubmittedApplication.ToListAsync());
        }

        public async Task<IActionResult> FilterPaused()
        {
            return View();
        }


        // GET: SubmittedApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submittedApplication == null)
            {
                return NotFound();
            }

            return View(submittedApplication);
        }
        
        public async Task<IActionResult> ProcessByJobId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplication.FirstOrDefaultAsync(m => m.Id == id);

            if (submittedApplication == null)
            {
                return NotFound();
            }

            return View(submittedApplication);
        }

        

        public async Task<IActionResult> ApplicantData(SubmittedApplication Viewit, int? JobCandidate)
        {
            //int FG = Convert
                string query = "SELECT * FROM SubmittedApplication WHERE Jd_JobId="+JobCandidate+"";
                query += " SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                  cmd.Connection = con;
                  con.Open();
                  var TY = cmd.ExecuteReader();

                    ArrayList al = new ArrayList();
                    
                    while (TY.Read())
                    {
                        object[] values = new object[TY.FieldCount];
                        TY.GetValues(values);
                        al.Add(values);
                    }

                    TY.Close();
                    con.Close();

                    string jsonString;
                   jsonString = JsonSerializer.Serialize(al);

                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("candidate_json_string", jsonString);
                     
                con.Close();
                }
            
            return View(Viewit);
        }

        public async Task<IActionResult> ShortListedCandidates(SubmittedApplication Viewit, int? JobCandidates)
        {
            //int FG = Convert
            string query = "SELECT * FROM ShortListedCandidates WHERE job_id=" + JobCandidates + " AND status=1";
            query += "SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                var TY = cmd.ExecuteReader();

                ArrayList al = new ArrayList();

                while (TY.Read())
                {
                    object[] values = new object[TY.FieldCount];
                    TY.GetValues(values);
                    al.Add(values);
                }

                TY.Close();
                con.Close();

                string jsonString;
                jsonString = JsonSerializer.Serialize(al);

                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("candidate_json_string", jsonString);

                con.Close();
            }

            return View(Viewit);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ProcessCandidates([FromForm] string lucky_candidate, string CandidateName, int? JobId, string JobTitle, string candidate_name)
        {
            
            string query = "INSERT INTO ShortListedCandidates(job_id,candidate_id,job_title,status,candidate_name) VALUES('"+JobId+"','"+lucky_candidate+"','"+JobTitle+"','1','"+ candidate_name + "')";

            using (SqlCommand cmd = new SqlCommand(query,con))
            {
                cmd.Connection = con;
                con.Open();
                
                var TY = cmd.ExecuteNonQuery();
                con.Close();

                

                con.Close();
            }


            return View();
        }

        [HttpPost]
        public void ExperiencedCandidates(int JOB_ID, int ExperienceOfYears)
        {
            try
            {
                    ArrayList CandidatesFoundArray = new ArrayList();
                    using (SqlCommand ExperienceYears = new SqlCommand("SELECT * FROM EmploymentHistoryModel WHERE JobId="+JOB_ID+" AND TotalYearsWorked="+ExperienceOfYears+""))
                    {
                        ExperienceYears.Connection = con;
                        con.Open();

                        var ExpericedCandidatesFound = ExperienceYears.ExecuteReader();

                        while (ExpericedCandidatesFound.Read())
                        {

                            object[] FinalDataObject = new object[ExpericedCandidatesFound.FieldCount];
                            ExpericedCandidatesFound.GetValues(FinalDataObject);
                            CandidatesFoundArray.Add(FinalDataObject);

                        }

                        string jsonCandidateObject = JsonSerializer.Serialize(CandidatesFoundArray);
                        CookieOptions cookies = new CookieOptions();
                        cookies.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Append("experinced_", jsonCandidateObject);
                        
                        ExpericedCandidatesFound.Close();
                        con.Close();
                    }
                
            }
            catch (Exception cb)
            {
                //handle error gracefully
                Response.Cookies.Append("experinced_response_error_", cb.ToString());
            }
            

        }


        [HttpPost]
        public IActionResult CertifiedCandidates(string JOB_CERT_ID)
        {
            try
            {
                ArrayList CertCandidatesFoundArray = new ArrayList();
                using (SqlCommand CertExperienceYears = new SqlCommand("SELECT * FROM CertificationsAndAwards WHERE JobId='" + JOB_CERT_ID + "'"))
                {
                    CertExperienceYears.Connection = con;
                    con.Open();

                    var CertExpericedCandidatesFound = CertExperienceYears.ExecuteReader();

                    while (CertExpericedCandidatesFound.Read())
                    {

                        object[] FinalDataObject = new object[CertExpericedCandidatesFound.FieldCount];
                        CertExpericedCandidatesFound.GetValues(FinalDataObject);
                        CertCandidatesFoundArray.Add(FinalDataObject);

                    }

                    string jsonCandidateObject = JsonSerializer.Serialize(CertCandidatesFoundArray);
                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("CertExperinced_", jsonCandidateObject);


                    CertExpericedCandidatesFound.Close();
                    con.Close();
                }

            }
            catch (Exception cb)
            {
                //handle error gracefully
                Response.Cookies.Append("cert_experinced_response_error_", cb.ToString());
            }

            return View();
        }


        [HttpPost]
        public IActionResult FilterRequest(string education, string JobID, string experience, string certficate_wanted, string language)
        {
            string EducationDetails_query = "SELECT * FROM EducaitonDetails WHERE '"+education+"' IN (QualificationAttained, QualificationAttained2, QualificationAttained3, QualificationAttained4, QualificationAttained5) AND JobId="+JobID+"";

            using (SqlCommand cmd1 = new SqlCommand(EducationDetails_query))
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
                    Response.Cookies.Append("loaded_prototype_", jsonString);
                    Response.Cookies.Append("years_", experience);
                    Response.Cookies.Append("cert_", certficate_wanted);
                    Response.Cookies.Append("language_", language);
                    Response.Cookies.Append("trigger_", "0");

                    TY.Close();
                    con.Close();
                }
                catch (Exception gh)
                {
                    //handle error well
                }
               
            }

            return View();
        }




        public IActionResult RemoveCandidate([FromForm] string unlucky_candidate, int? JobId)
        {

            string query = "UPDATE ShortListedCandidates SET status=2 WHERE job_id="+JobId+" AND candidate_id="+unlucky_candidate+"";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                con.Open();

                var TY = cmd.ExecuteNonQuery();
                con.Close();



                con.Close();
            }


            return View();
        }

        public async Task<IActionResult> FilteringCloud()
        {
            return View();
        }

        // GET: SubmittedApplications/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jd_JobId,Jd_JobTitle,Bd_FirstName,Bd_MiddleName,Bd_LastName,Bd_Residence,Bd_PrimaryPhoneNumber,Bd_AlternativePhoneNumber,Bd_LanguagesSpoken,Bd_WorkedWithIDRC,Ed_NameOfInstitution,Ed_QualificationAttained,Ed_DateQualificationWasAttained,Eh_EmployerName,Eh_EmploymentStartDate,Eh_EmploymentEndDate,Eh_JobTitle,Eh_SummaryOfDuties,Eh_SummaryOfAchievments,Ca_CertificationName,Ca_CertifyingInstitutionName,Ca_CertificationDate,Ud_CertificationAwardName,Ud_InstitutionName,Ud_DateOfCertification")] SubmittedApplication submittedApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submittedApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submittedApplication);
        }

        // GET: SubmittedApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplication.FindAsync(id);
            if (submittedApplication == null)
            {
                return NotFound();
            }
            return View(submittedApplication);
        }

        // POST: SubmittedApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jd_JobId,Jd_JobTitle,Bd_FirstName,Bd_MiddleName,Bd_LastName,Bd_Residence,Bd_PrimaryPhoneNumber,Bd_AlternativePhoneNumber,Bd_LanguagesSpoken,Bd_WorkedWithIDRC,Ed_NameOfInstitution,Ed_QualificationAttained,Ed_DateQualificationWasAttained,Eh_EmployerName,Eh_EmploymentStartDate,Eh_EmploymentEndDate,Eh_JobTitle,Eh_SummaryOfDuties,Eh_SummaryOfAchievments,Ca_CertificationName,Ca_CertifyingInstitutionName,Ca_CertificationDate,Ud_CertificationAwardName,Ud_InstitutionName,Ud_DateOfCertification")] SubmittedApplication submittedApplication)
        {
            if (id != submittedApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submittedApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmittedApplicationExists(submittedApplication.Id))
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
            return View(submittedApplication);
        }

        // GET: SubmittedApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submittedApplication == null)
            {
                return NotFound();
            }

            return View(submittedApplication);
        }

        // POST: SubmittedApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submittedApplication = await _context.SubmittedApplication.FindAsync(id);
            _context.SubmittedApplication.Remove(submittedApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmittedApplicationExists(int id)
        {
            return _context.SubmittedApplication.Any(e => e.Id == id);
        }
    }
}
