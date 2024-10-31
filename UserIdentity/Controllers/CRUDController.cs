using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.DAL;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    public class CRUDController : Controller
    {
        private ProductContext context = new ProductContext();
        // GET: CRUD
        public ActionResult List()
        {
            return View(context.Products.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return View("Not Found");
            }
            var product = context.Products.Find(id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Product model, double? price, bool inStock)
        {
            var product = context.Products.Find(model.Id);

            if (price != null)
            {
                product.Price = model.Price;
            }
            if (product.InStock != inStock)
            {
                product.InStock = inStock;
            }

            context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("Not Found");
            }
            var product = context.Products.Find(id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Product model)
        {
            var product = context.Products.Find(model.Id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Firms = context.Firms.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Add(Product model)
        {
            //   var firm_name  = context.Firms.Select(f => f.FirmName).Single();
            var firm = context.Firms.SingleOrDefault(f => f.FirmName == model.Firm.FirmName); // Finds firm by name
            if (firm != null)
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    InStock = model.InStock,
                    Firm = firm
                };
                context.Products.Add(product);
                context.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    InStock = model.InStock,
                    Firm = model.Firm
                };
                context.Products.Add(product);
                context.SaveChanges();

                return RedirectToAction("List");
            }

        }
    }
}