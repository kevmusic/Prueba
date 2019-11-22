using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using static University.Models.Utilities.Encripcion;

namespace University.Models
{
    public class Usuario
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Rol")]
        public int? RolID { get; set; }

        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; }

        public static (bool Guardado, string Mensaje) GuardarUsuario(Usuario usuario)
        {
            usuario.Password = Encriptar(usuario.Password);

            using (ElContext context = new ElContext())
            {
                context.Entry(usuario).State = EntityState.Added;
                try
                {
                    context.SaveChanges();
                    return (true, "OK");
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }
        }

        public static (bool Autenticado, string Mensaje) AutenticarUsuario(Usuario usuario)
        {
            Usuario dbUsuario;
            using (ElContext context = new ElContext())
            {
                dbUsuario = context.Usuarios.FirstOrDefault(f => f.Username == usuario.Username);
            }

            if (dbUsuario == null)
                return (false, "No existe el usuario");

            string decryptedPassword = Desencriptar(dbUsuario.Password);
            if (decryptedPassword != usuario.Password)
                return (false, "Contraseña incorrecta");

            return (true, "OK");
        }
    }
}