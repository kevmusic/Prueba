using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace University.Models
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int RolID { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}