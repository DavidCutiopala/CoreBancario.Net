using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VotoElectronico.Entidades;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;

namespace EcVotoElectronico
{
    public class VotoDbContext : DbContext
    {
        #region Tablas
        public DbSet<Sg01_Usuario> Sg01_Usuarios { get; set; }
        public DbSet<Sg02_Persona> Sg02_Personas { get; set; }
        public DbSet<Sg03_Cargo> Sg03_Cargos { get; set; }
        public DbSet<Sg04_UsuarioCargo> Sg04_UsuarioCargos { get; set; }
        public DbSet<Sg07_Rol> Sg07_Roles { get; set; }
        public DbSet<Sg08_UsuarioRol> Sg08_UsuarioRoles { get; set; }
        public DbSet<Pe03_TipoEleccion> Pe03_TipoElecciones { get; set; }
        public DbSet<Pe02_Eleccion> Pe02_Elecciones { get; set; }
        public DbSet<Pe04_Escanio> Pe04_Escanios { get; set; }
        public DbSet<Pe01_ProcesoElectoral> Pe01_ProcesoElectorales { get; set; }
        public DbSet<Pe05_Lista> Pe05_Listas { get; set; }
        public DbSet<Pe06_Candidato> Pe06_Candidatos { get; set; }
        public DbSet<Mi01_PadronVotacion> Mi01_PadronVotaciones { get; set; }
        public DbSet<Ge01_Mensaje> Ge01_Mensajes { get; set; }
        public DbSet<Ge02_TipoMensaje> Ge02_TipoMensajes { get; set; }

        public DbSet<Sg05_Permiso> Sg05_Permisos { get; set; }
        public DbSet<Sg06_Recurso> Sg06_Recursos { get; set; }
        public DbSet<Mv01_Voto> Mv01_Votos { get; set; }
        public DbSet<Mv02_Opcion> Mv02_Opciones { get; set; }

        public DbSet<CB01_Cliente> CB01_Clientes { get; set; }
        public DbSet<CB02_Cuenta> CB02_Cuentas { get; set; }
        public DbSet<CB03_Movimiento> CB03_Movimientos { get; set; }
        public DbSet<CB04_Usuario> CB04_Usuarios { get; set; }

        #endregion


        public VotoDbContext()
            : base("name=Default")
        {
            var tipo = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Configure domain classes using modelBuilder here..
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties()
                       .Where(p => p.Name == "Id")
                       .Configure(p => p.IsKey());

            modelBuilder.Properties<DateTime>()
                        .Configure(property => property.HasColumnType("datetime2"));

            modelBuilder.Properties<decimal>()
                .Configure(property => property.HasPrecision(20, 6));

        }
    }


}
