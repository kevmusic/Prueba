using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace University.Models
{
    public class Ente
    {
        [Key]
        public int EnteID { get; set; }
        [Required(ErrorMessage = "Debe registrar el nombre completo", AllowEmptyStrings = false)]
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [DisplayName("Sexo")]
        public int? SexoID { get; set; }

        [ForeignKey("SexoID")]
        public virtual Sexo Sexo { get; set; }

        public virtual IList<Poliza> Polizas { get; set; }

        public static Ente ObtenerEntePorID(int EnteID)
        {
            using (ElContext context = new ElContext())
            {
                return context.Entes.SingleOrDefault(e => e.EnteID == EnteID);
            }
        }

        public static IEnumerable<Ente> ObtenerEntes()
        {
            using (ElContext context = new ElContext())
            {
                return context.Entes.AsEnumerable();
            }
        }
    }
}