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
    public class ApplicantDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantDocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantDocuments.ToListAsync());
        }

        // GET: ApplicantDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantDocuments = await _context.ApplicantDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantDocuments == null)
            {
                return NotFound();
            }

            return View(applicantDocuments);
        }

        // GET: ApplicantDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CertificationAwardName,InstitutionName,DateOfCertification")] ApplicantDocuments applicantDocuments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantDocuments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "ApplicantSummaries");
            }
            return View("~/ApplicantSummariesController/Create");
        }

        // GET: ApplicantDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantDocuments = await _context.ApplicantDocuments.FindAsync(id);
            if (applicantDocuments == null)
            {
                return NotFound();
            }
            return View(applicantDocuments);
        }

        // POST: ApplicantDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CertificationAwardName,InstitutionName,DateOfCertification")] ApplicantDocuments applicantDocuments)
        {
            if (id != applicantDocuments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantDocuments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantDocumentsExists(applicantDocuments.Id))
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
            return View(applicantDocuments);
        }

        // GET: ApplicantDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantDocuments = await _context.ApplicantDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantDocuments == null)
            {
                return NotFound();
            }

            return View(applicantDocuments);
        }

        // POST: ApplicantDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantDocuments = await _context.ApplicantDocuments.FindAsync(id);
            _context.ApplicantDocuments.Remove(applicantDocuments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantDocumentsExists(int id)
        {
            return _context.ApplicantDocuments.Any(e => e.Id == id);
        }
    }
}
