    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;

namespace v1jobportal.Controllers
{
    public class ApplicantSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantSummariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantSummaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantSummary.ToListAsync());
        }


    // GET: ApplicantSummaries/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSummary = await _context.ApplicantSummary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSummary == null)
            {
                return NotFound();
            }

            return View(applicantSummary);
        }

        // GET: ApplicantSummaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantSummaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicantSummaryData")] ApplicantSummary applicantSummary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantSummary);
                await _context.SaveChangesAsync();
                return RedirectToAction("index","PendingApplications");
            }
            return View("~/PendingApplications/index");
        }

        public async Task<IActionResult> CreateToPendingTable([Bind("Id,ApplicantSummaryData")] ApplicantSummary applicantSummary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantSummary);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "PendingApplications");
            }
            return View("~/PendingApplications/index");
        }

        // GET: ApplicantSummaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSummary = await _context.ApplicantSummary.FindAsync(id);
            if (applicantSummary == null)
            {
                return NotFound();
            }
            return View(applicantSummary);
        }

        // POST: ApplicantSummaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicantSummaryData")] ApplicantSummary applicantSummary)
        {
            if (id != applicantSummary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantSummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantSummaryExists(applicantSummary.Id))
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
            return View(applicantSummary);
        }

        // GET: ApplicantSummaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSummary = await _context.ApplicantSummary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSummary == null)
            {
                return NotFound();
            }

            return View(applicantSummary);
        }

        // POST: ApplicantSummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantSummary = await _context.ApplicantSummary.FindAsync(id);
            _context.ApplicantSummary.Remove(applicantSummary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantSummaryExists(int id)
        {
            return _context.ApplicantSummary.Any(e => e.Id == id);
        }
    }
}
