using System;
using System.ComponentModel.DataAnnotations;

namespace CozyCo.Models
{
    public class Property
    {
        public string Address { get; set; }

        [Display(Name ="Address cont.")]
        public string Address2 { get; set; }

        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Image { get; set; }
    }
}
