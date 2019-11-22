using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Poliza
    {
        [Key]
        public int PolizaID { get; set; }
        [Required]
        public string NumeroPoliza { get; set; }
        public int AseguradoID { get; set; }

        [ForeignKey("AseguradoID")]
        public virtual Ente Asegurado { get; set; }
    }
}