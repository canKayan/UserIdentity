using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using UserIdentity.Models;
using UserIdentity.Controllers;

namespace UserIdentity.DAL
{
    public class ProductInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            var firms = new List<Firm>
            {
                new Firm(){ FirmName = "Apple" },
                new Firm(){ FirmName = "Samsung" },
                new Firm(){ FirmName = "LG" },
                new Firm(){ FirmName = "Siemens" }
            };
            context.Firms.AddRange(firms);
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product(){ ProductName = "iPhone X", Price = 1000.0, InStock = true, Firm_Id = firms.ElementAt(0).Id},
                new Product(){ ProductName = "iPhone 12", Price = 1500.0, InStock = false, Firm_Id = firms.ElementAt(0).Id },
                new Product(){ ProductName = "iPhone 13", Price = 1700.0, InStock = true, Firm_Id = firms.ElementAt(0).Id },
                new Product(){ ProductName = "iPhone 14", Price = 2000.0, InStock = true, Firm_Id = firms.ElementAt(0).Id },
                new Product(){ ProductName = "Samsung S22", Price = 2500.0, InStock = true, Firm_Id = firms.ElementAt(1).Id}
            };
            context.Products.AddRange(products);
            context.SaveChanges();

        }
    }
}