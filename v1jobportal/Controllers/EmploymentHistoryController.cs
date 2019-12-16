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
    public class EmploymentHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmploymentHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmploymentHistory
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmploymentHistoryModel.ToListAsync());
        }

        // GET: EmploymentHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistoryModel = await _context.EmploymentHistoryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistoryModel == null)
            {
                return NotFound();
            }

            return View(employmentHistoryModel);
        }

        // GET: EmploymentHistory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployerName,EmploymentStartDate,EmploymentEndDate,JobTitle,SummaryOfDuties,SummaryOfAchievments")] EmploymentHistoryModel employmentHistoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentHistoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","CertificationsAndAwards");
            }
            return View("~/CertificationsAndAwardsController/Create");
        }

        // GET: EmploymentHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistoryModel = await _context.EmploymentHistoryModel.FindAsync(id);
            if (employmentHistoryModel == null)
            {
                return NotFound();
            }
            return View(employmentHistoryModel);
        }

        // POST: EmploymentHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployerName,EmploymentStartDate,EmploymentEndDate,JobTitle,SummaryOfDuties,SummaryOfAchievments")] EmploymentHistoryModel employmentHistoryModel)
        {
            if (id != employmentHistoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentHistoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentHistoryModelExists(employmentHistoryModel.Id))
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
            return View(employmentHistoryModel);
        }

        // GET: EmploymentHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistoryModel = await _context.EmploymentHistoryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistoryModel == null)
            {
                return NotFound();
            }

            return View(employmentHistoryModel);
        }

        // POST: EmploymentHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentHistoryModel = await _context.EmploymentHistoryModel.FindAsync(id);
            _context.EmploymentHistoryModel.Remove(employmentHistoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentHistoryModelExists(int id)
        {
            return _context.EmploymentHistoryModel.Any(e => e.Id == id);
        }
    }
}
