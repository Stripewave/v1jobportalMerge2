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
    public class CertificationsAndAwardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificationsAndAwardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CertificationsAndAwards
        public async Task<IActionResult> Index()
        {
            return View(await _context.CertificationsAndAwards.ToListAsync());
        }

        // GET: CertificationsAndAwards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationsAndAwards = await _context.CertificationsAndAwards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificationsAndAwards == null)
            {
                return NotFound();
            }

            return View(certificationsAndAwards);
        }

        // GET: CertificationsAndAwards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CertificationsAndAwards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CertificationName,CertifyingInstitutionName,CertificationDate")] CertificationsAndAwards certificationsAndAwards)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificationsAndAwards);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "ApplicantDocuments");
            }
            return View("~/ApplicantDocumentsController/Create");
        }

        // GET: CertificationsAndAwards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationsAndAwards = await _context.CertificationsAndAwards.FindAsync(id);
            if (certificationsAndAwards == null)
            {
                return NotFound();
            }
            return View(certificationsAndAwards);
        }

        // POST: CertificationsAndAwards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CertificationName,CertifyingInstitutionName,CertificationDate")] CertificationsAndAwards certificationsAndAwards)
        {
            if (id != certificationsAndAwards.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificationsAndAwards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationsAndAwardsExists(certificationsAndAwards.Id))
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
            return View(certificationsAndAwards);
        }

        // GET: CertificationsAndAwards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationsAndAwards = await _context.CertificationsAndAwards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificationsAndAwards == null)
            {
                return NotFound();
            }

            return View(certificationsAndAwards);
        }

        // POST: CertificationsAndAwards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificationsAndAwards = await _context.CertificationsAndAwards.FindAsync(id);
            _context.CertificationsAndAwards.Remove(certificationsAndAwards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationsAndAwardsExists(int id)
        {
            return _context.CertificationsAndAwards.Any(e => e.Id == id);
        }
    }
}
