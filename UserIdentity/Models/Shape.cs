using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserIdentity.Models
{
    public class Shape
    {
        public int Id { get; set; }
        public string ShapeName { get; set; }
        public double SideLength1 { get; set; }
        public double? SideLength2 { get; set; }

        public double CalculateArea()
        {
            if (ShapeName == "Square")
            {
                return SideLength1 * SideLength1;  // Karenin alanı
            }
            else if (ShapeName == "Circle")
            {
                return Math.PI * SideLength1 * SideLength1;  // Dairenin alanı (πr²)
            }
            else if (ShapeName == "Rectangle" && SideLength2.HasValue)
            {
                return SideLength1 * SideLength2.Value;  // Dikdörtgenin alanı (en * boy)
            }

            return 0; // Diğer şekillerin hesaplamalarını buraya ekleyebilirsin
        }
    }
}