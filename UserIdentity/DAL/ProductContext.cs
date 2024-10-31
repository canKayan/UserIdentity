using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using UserIdentity.Identity;
using UserIdentity.Models;


namespace UserIdentity.DAL
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("productConnection")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Firm> Firms { get; set; }

        public static ProductContext Create()
        {
            return new ProductContext();
        }
    }
}