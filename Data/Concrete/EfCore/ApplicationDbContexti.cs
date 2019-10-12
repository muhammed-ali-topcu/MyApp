using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Concrete.EfCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Tables
        public DbSet<Company> Companies { get; set; }
       // public DbSet<Company> Companys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Page> pages { get; set; }
       // public DbSet<User> Users { get; set; }

        /*
        public DbSet<Branch> Branches { get; set; }
                public DbSet<Expense> Expenses { get; set; }
        public DbSet<GnlComboPrm> gnlComboPrms { get; set; }
        public DbSet<ProductQuntity> GetProductQuntities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPageAbitity> UserPageAbitities { get; set; }
        */


        /*
        //https://stackoverflow.com/questions/45775267/how-to-validate-models-before-savechanges-in-entityframework-core-2
        public virtual DbSet<Model> Models { get; set; }
        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker
                .Entries()
                .Where(_ => _.State == EntityState.Added ||
                            _.State == EntityState.Modified);

            var errors = new List<ValidationResult>(); // all errors are here
            foreach (var e in changedEntities)
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(
                    e.Entity, vc, errors, validateAllProperties: true);
            }

            return base.SaveChanges();
        }*/


    }

    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Field { get; set; }

        [Range(15, 25)]
        public int RangeField { get; set; }
    }

}
