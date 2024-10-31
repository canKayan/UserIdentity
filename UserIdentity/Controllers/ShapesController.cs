using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.DAL;

namespace UserIdentity.Controllers
{
    public class ShapesController : Controller
    {
        private ShapeContext context = new ShapeContext();
        // GET: Shapes
        public ActionResult List()
        {
            return View(context.Shapes.ToList());
        }

    }
}