using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CozyCo.Models
{
    public class RegisterViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }

        [DataType(DataType.Password), Required]
        [Compare("Password", ErrorMessage ="Make sure your password match")]
        public string ConfirmPassword { get; set; }
    }
}
