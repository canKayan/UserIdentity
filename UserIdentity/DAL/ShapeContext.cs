using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserIdentity.Models;

namespace UserIdentity.DAL
{
    public class ShapeContext : DbContext
    {
        public ShapeContext() : base("shapesConnection")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Shape> Shapes { get; set; }

        public static ShapeContext Create()
        {
            return new ShapeContext();
        }
    }
}