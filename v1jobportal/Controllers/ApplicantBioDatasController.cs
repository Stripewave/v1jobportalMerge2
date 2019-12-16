using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;

namespace v1jobportal.Controllers
{
    public class ApplicantBioDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantBioDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantBioDatas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantBioData.ToListAsync());
        }

        // GET: ApplicantBioDatas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantBioData = await _context.ApplicantBioData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantBioData == null)
            {
                return NotFound();
            }

            return View(applicantBioData);
        }

        // GET: ApplicantBioDatas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantBioDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,Residence,PrimaryPhoneNumber,AlternativePhoneNumber,LanguagesSpoken,WorkedWithIDRC")] ApplicantBioData applicantBioData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantBioData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","EducationDetails");
            }
            return View("~/EducationDetailsController/Create");
        }
        

        // GET: ApplicantBioDatas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantBioData = await _context.ApplicantBioData.FindAsync(id);
            if (applicantBioData == null)
            {
                return NotFound();
            }
            return View(applicantBioData);
        }

        // POST: ApplicantBioDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Residence,PrimaryPhoneNumber,AlternativePhoneNumber,LanguagesSpoken,WorkedWithIDRC")] ApplicantBioData applicantBioData)
        {
            if (id != applicantBioData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantBioData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantBioDataExists(applicantBioData.Id))
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
            return View(applicantBioData);
        }

        // GET: ApplicantBioDatas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantBioData = await _context.ApplicantBioData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantBioData == null)
            {
                return NotFound();
            }

            return View(applicantBioData);
        }
        //Return to previous Page
        [Authorize]
        public IActionResult Back()
        {
            return View();
        }

        // POST: ApplicantBioDatas/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantBioData = await _context.ApplicantBioData.FindAsync(id);
            _context.ApplicantBioData.Remove(applicantBioData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool ApplicantBioDataExists(int id)
        {
            return _context.ApplicantBioData.Any(e => e.Id == id);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }

            ViewBag.Message = "File successfully uploaded";

            return View();
        }
    }
}
