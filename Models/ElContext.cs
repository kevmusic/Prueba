using System.Data.Entity;
using University.Migrations;

namespace University.Models
{
    public class ElContext : DbContext
    {
        public ElContext() : base("ElContextConn")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ElContext, Configuration>());
        }

        public virtual DbSet<Poliza> Polizas { get; set; }
        public virtual DbSet<Ente> Entes { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }

        public System.Data.Entity.DbSet<University.Models.ViewModel.Login> Logins { get; set; }
    }
}