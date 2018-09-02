using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Models;
using SaaS_RMS.Models.Entities.Landing;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Inventory;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Entities.Vendor;
using SaaS_RMS.Models.Entities.Customer;

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
        public DbSet<Subscription> Subcriptions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        #endregion

        #region RESTAURANT DATA CONTEXT

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<RestaurantStatistics> RestaurantStatistics { get; set; }
        public DbSet<RestaurantQualification> RestaurantQualifications { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        
        #endregion

        #region RESTAURANT INVENTORY DATA CONTEXT
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        #endregion

        #region VENDOR DATA CONTEXT

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<CompanyVendor> CompanyVendors { get; set; }

        #endregion

        #region EMPLOYEE DATA CONTEXT

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePersonalData> EmployeePersonalDatas { get; set; }
        public DbSet<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }
        public DbSet<EmployeeBankData> EmployeeBankDatas { get; set; }
        public DbSet<EmployeeFamilyData> EmployeeFamilyDatas { get; set; }
        public DbSet<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        public DbSet<EmploymentPosition> EmploymentPositions { get; set; }
        public DbSet<EmployeePastWorkExperience> EmployeePastWorkExperiences { get; set; }
        public DbSet<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }
        
        #endregion

        #region ROLE DATA CONTEXT

        public new DbSet<Role> Roles { get; set; }

        #endregion

        #region LANDING PAGE MODELS

        public DbSet<LandingInfo> LandingInfo { get; set; }

        #endregion

        #region CUSTOMER DATA CONTEXT

        public DbSet<Customer> Customer { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        internal Task FindAsync(int v)
        {
            throw new NotImplementedException();
        }

        internal object Find(int? companyvendorid)
        {
            throw new NotImplementedException();
        }
    }
}
