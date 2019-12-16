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
    public class EducationDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EducationDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducaitonDetails.ToListAsync());
        }

        // GET: EducationDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationDetails = await _context.EducaitonDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationDetails == null)
            {
                return NotFound();
            }

            return View(educationDetails);
        }

        // GET: EducationDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameOfInstitution,QualificationAttained,DateQualificationWasAttained")] EducationDetails educationDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "EmploymentHistory");
            }
            return View("~/EmploymentHistoryController/Create");
        }

        // GET: EducationDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationDetails = await _context.EducaitonDetails.FindAsync(id);
            if (educationDetails == null)
            {
                return NotFound();
            }
            return View(educationDetails);
        }

        // POST: EducationDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameOfInstitution,QualificationAttained,DateQualificationWasAttained")] EducationDetails educationDetails)
        {
            if (id != educationDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationDetailsExists(educationDetails.Id))
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
            return View(educationDetails);
        }

        // GET: EducationDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationDetails = await _context.EducaitonDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationDetails == null)
            {
                return NotFound();
            }

            return View(educationDetails);
        }

        // POST: EducationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationDetails = await _context.EducaitonDetails.FindAsync(id);
            _context.EducaitonDetails.Remove(educationDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationDetailsExists(int id)
        {
            return _context.EducaitonDetails.Any(e => e.Id == id);
        }
    }
}
