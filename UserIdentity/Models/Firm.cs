using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserIdentity.Models
{
    public class Firm
    {
        public int Id { get; set; }
        public string FirmName { get; set; }

        public List<Product> Products { get; set; }
    }
}