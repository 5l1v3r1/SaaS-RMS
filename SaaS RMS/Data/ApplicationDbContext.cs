using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Models;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;

namespace SaaS_RMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region SYSTEM DATA CONTEXT

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Lga> Lgas { get; set; }
        public DbSet<Package> Packages { get; set; }

        #endregion

        #region RESTAURANT DATA CONTEXT

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Department> Departments { get; set; }

        #endregion

        #region EMPLOYEE DATA CONTEXT

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePersonalData> EmployeePersonalDatas { get; set; }
        public DbSet<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }
        public DbSet<EmployeeBankData> EmployeeBankData { get; set; }


        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
