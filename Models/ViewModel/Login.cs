using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University.Models.ViewModel
{
    public class Login
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}