using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using v1jobportal.Models;
using v1jobportal.ViewModels;

namespace v1jobportal.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

        public DbSet<EmploymentHistoryModel> EmploymentHistoryModel { get; set; }
        public DbSet<EducationDetails> EducaitonDetails { get; set; }
        public DbSet<CertificationsAndAwards> CertificationsAndAwards { get; set; }
        public DbSet<ApplicantBioData> ApplicantBioData { get; set; }
        //
        public DbSet<ApplicantSummary> ApplicantSummary { get; set; }
        public DbSet<AllJobListings> AllJobListings { get; set; }
        public DbSet<SubmittedApplication> SubmittedApplication { get; set; }
        public DbSet<PendingApplications> PendingApplications { get; set; }
        public DbSet<EditRoleViewModel> EditRoleViewModels { get; set; }
        public DbSet<ApplicantDocuments> ApplicantDocuments { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }

        public DbSet<v1jobportal.Models.DisplayLinkJobDetailsModel> DisplayLinkJobDetailsModel { get; set; }

    }
}
