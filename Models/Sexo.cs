using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Sexos")]
    public class Sexo
    {
        [Key]
        public int SexoID { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1)]
        [DisplayName("Sexo")]
        public string Descripcion { get; set; }
    }
}