using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Data;
using v1jobportal.Models;
using v1jobportal.Controllers;
namespace v1jobportal.Controllers
{
    public class GeneratedLinksController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;

        public GeneratedLinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DisplayJobContent_ByID(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AvailableJob_Data = await _context.AllJobListings.FindAsync(id);
            if (AvailableJob_Data == null)
            {
                return NotFound();
            }
            return View(AvailableJob_Data);
        }

        public IActionResult available_jobs()
        {
            var JobIDWanted = Request.Cookies["job_id_main"];
            var JobDataFound=DisplayJobContent_ByID(JobIDWanted);

            //var employmentHistoryModel = _context.GeneratedLinks.FirstOrDefaultAsync(m => m.Id == JobDataFound);



            return View();
           
        }
    }
}