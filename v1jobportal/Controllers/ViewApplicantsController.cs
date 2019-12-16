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
    public class ViewApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViewApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ViewApplicants
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllJobListings.ToListAsync());
        }

        public async Task<IActionResult> AllCandidates(int? id_imputed)
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

            return (View(AllJobCandidates));

        }

        // GET: ViewApplicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allJobListings = await _context.AllJobListings.FirstOrDefaultAsync(m => m.Id == id);

            if (allJobListings == null)
            {
                return NotFound();
            }

            return View(allJobListings);
        }

        // GET: ViewApplicants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViewApplicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,DutyStation,Duties,Doctorate,MastersDegree,bachelorsDegree,Diploma,Certificate,Deadline")] AllJobListings allJobListings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allJobListings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allJobListings);
        }

        // GET: ViewApplicants/Edit/5
        public async Task<IActionResult> ViewJobApplicants1(int? id)
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

        // POST: ViewApplicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewJobApplicants1(int id, [Bind("Id,JobTitle,DutyStation,Duties,Doctorate,MastersDegree,bachelorsDegree,Diploma,Certificate,Deadline")] ViewApplicants ApplyingCandidates)
        {
            if (id != ApplyingCandidates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ApplyingCandidates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllJobListingsExists(ApplyingCandidates.Id))
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
            return View(ApplyingCandidates);
        }

        // GET: ViewApplicants/Delete/5
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

        // POST: ViewApplicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
    }
}
