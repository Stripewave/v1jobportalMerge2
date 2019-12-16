using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;

namespace v1jobportal.Controllers
{
    public class PendingApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PendingApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PendingApplications
        public async Task<IActionResult> Index()
        {
            return View(await _context.PendingApplications.ToListAsync());
        }

        // GET: PendingApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingApplications = await _context.PendingApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pendingApplications == null)
            {
                return NotFound();
            }

            return View(pendingApplications);
        }

        // GET: PendingApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PendingApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jd_JobId,Jd_JobTitle,Deadline,Bd_FirstName,Bd_MiddleName,Bd_LastName,Bd_Residence,Bd_PrimaryPhoneNumber,Bd_AlternativePhoneNumber,Bd_LanguagesSpoken,Bd_WorkedWithIDRC,Ed_NameOfInstitution,Ed_QualificationAttained,Ed_DateQualificationWasAttained,Eh_EmployerName,Eh_EmploymentStartDate,Eh_EmploymentEndDate,Eh_JobTitle,Eh_SummaryOfDuties,Eh_SummaryOfAchievments,Ca_CertificationName,Ca_CertifyingInstitutionName,Ca_CertificationDate,Ud_CertificationAwardName,Ud_InstitutionName,Ud_DateOfCertification")] PendingApplications pendingApplications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pendingApplications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pendingApplications);
        }

        // GET: PendingApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingApplications = await _context.PendingApplications.FindAsync(id);
            if (pendingApplications == null)
            {
                return NotFound();
            }
            return View(pendingApplications);
        }

        // POST: PendingApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jd_JobId,Jd_JobTitle,Deadline,Bd_FirstName,Bd_MiddleName,Bd_LastName,Bd_Residence,Bd_PrimaryPhoneNumber,Bd_AlternativePhoneNumber,Bd_LanguagesSpoken,Bd_WorkedWithIDRC,Ed_NameOfInstitution,Ed_QualificationAttained,Ed_DateQualificationWasAttained,Eh_EmployerName,Eh_EmploymentStartDate,Eh_EmploymentEndDate,Eh_JobTitle,Eh_SummaryOfDuties,Eh_SummaryOfAchievments,Ca_CertificationName,Ca_CertifyingInstitutionName,Ca_CertificationDate,Ud_CertificationAwardName,Ud_InstitutionName,Ud_DateOfCertification")] PendingApplications pendingApplications)
        {
            if (id != pendingApplications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pendingApplications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PendingApplicationsExists(pendingApplications.Id))
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
            return View(pendingApplications);
        }

        // GET: PendingApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingApplications = await _context.PendingApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pendingApplications == null)
            {
                return NotFound();
            }

            return View(pendingApplications);
        }

        // POST: PendingApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pendingApplications = await _context.PendingApplications.FindAsync(id);
            _context.PendingApplications.Remove(pendingApplications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PendingApplicationsExists(int id)
        {
            return _context.PendingApplications.Any(e => e.Id == id);
        }
    }
}
