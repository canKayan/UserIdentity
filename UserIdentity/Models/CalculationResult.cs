using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserIdentity.Models
{
    public class CalculationResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InputValue { get; set; }
        public int CalculatedValue { get; set; }  // Hesaplanan sonuç
    }
}