using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserIdentity.Models;

namespace UserIdentity.DAL
{
    public class ShapeInitializer : DropCreateDatabaseIfModelChanges<ShapeContext>
    {
        protected override void Seed(ShapeContext context)
        {
            

            var shapes = new List<Shape>
            {
                new Shape(){ ShapeName = "Square", SideLength1 = 4.0, SideLength2 = 4.0},
                new Shape(){ ShapeName = "Rectangle", SideLength1 = 3.0, SideLength2 = 5.0},
            };
            context.Shapes.AddRange(shapes);
            context.SaveChanges();

        }
    }
}