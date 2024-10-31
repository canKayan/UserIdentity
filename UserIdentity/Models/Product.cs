using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserIdentity.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }

        [ForeignKey("Firm")]
        public int Firm_Id { get; set; }


        public virtual Firm Firm { get; set; }
    }
}