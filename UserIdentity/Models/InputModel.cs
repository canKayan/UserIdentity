using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserIdentity.Models
{
    public class InputModel
    {
        public string Name { get; set; }  // Kullanıcıdan gelen isim
        public int Value { get; set; }    // Kullanıcıdan gelen değer
    }

}