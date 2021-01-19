namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnificarMigraciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ge01_Mensaje",
                c => new
                    {
                        CodigoMensaje = c.String(nullable: false, maxLength: 200, unicode: false),
                        Titulo = c.String(maxLength: 200, unicode: false),
                        Descripcion = c.String(maxLength: 8000, unicode: false),
                        TipoMensajeId = c.String(maxLength: 100, unicode: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CodigoMensaje)
                .ForeignKey("dbo.Ge02_TipoMensaje", t => t.TipoMensajeId)
                .Index(t => t.TipoMensajeId);
            
            CreateTable(
                "dbo.Ge02_TipoMensaje",
                c => new
                    {
                        TipoMensajeId = c.String(nullable: false, maxLength: 100, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 200, unicode: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TipoMensajeId);
            
            CreateTable(
                "dbo.Mi01_PadronVotacion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProcesoElectoralId = c.Long(nullable: false),
                        VotoRealizado = c.Boolean(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg01_Usuario", t => t.UsuarioId)
                .ForeignKey("dbo.Pe01_ProcesoElectoral", t => t.ProcesoElectoralId)
                .Index(t => t.UsuarioId)
                .Index(t => t.ProcesoElectoralId);
            
            CreateTable(
                "dbo.Pe01_ProcesoElectoral",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreProcesoElectoral = c.String(nullable: false, maxLength: 200, unicode: false),
                        FechaInicio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaFin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EleccionId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe02_Eleccion", t => t.EleccionId)
                .Index(t => t.EleccionId);
            
            CreateTable(
                "dbo.Pe02_Eleccion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreEleccion = c.String(nullable: false),
                        TipoEleccionId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe03_TipoEleccion", t => t.TipoEleccionId)
                .Index(t => t.TipoEleccionId);
            
            CreateTable(
                "dbo.Pe04_Escanio",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreEscanio = c.String(nullable: false),
                        EleccionId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe02_Eleccion", t => t.EleccionId)
                .Index(t => t.EleccionId);
            
            CreateTable(
                "dbo.Pe06_Candidato",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ListaId = c.Long(nullable: false),
                        PersonaId = c.Long(nullable: false),
                        EscanioId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe04_Escanio", t => t.EscanioId)
                .ForeignKey("dbo.Pe05_Lista", t => t.ListaId)
                .ForeignKey("dbo.Sg02_Persona", t => t.PersonaId)
                .Index(t => t.EscanioId)
                .Index(t => t.ListaId)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "dbo.Pe05_Lista",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreLista = c.String(nullable: false),
                        ProcesoElectoralId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe01_ProcesoElectoral", t => t.ProcesoElectoralId)
                .Index(t => t.ProcesoElectoralId);
            
            CreateTable(
                "dbo.Sg02_Persona",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreUno = c.String(nullable: false, maxLength: 100),
                        NombreDos = c.String(maxLength: 100),
                        ApellidoUno = c.String(nullable: false, maxLength: 100),
                        ApellidoDos = c.String(maxLength: 100),
                        Identificacion = c.String(nullable: false, maxLength: 20),
                        Telefono = c.String(nullable: false, maxLength: 20),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sg01_Usuario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 60, unicode: false),
                        EnvioEmailActivacion = c.Boolean(nullable: false),
                        Clave = c.String(maxLength: 100, unicode: false),
                        NombreUsuario = c.String(nullable: false, maxLength: 100, unicode: false),
                        Token = c.String(),
                        InicioSesion = c.Boolean(nullable: false),
                        PersonaId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg02_Persona", t => t.PersonaId)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "dbo.Sg04_UsuarioCargo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CargoId = c.Long(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg03_Cargo", t => t.CargoId)
                .ForeignKey("dbo.Sg01_Usuario", t => t.UsuarioId)
                .Index(t => t.CargoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Sg03_Cargo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreCargo = c.String(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pe03_TipoEleccion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreTipoEleccion = c.String(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mi01_PadronVotacion", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral");
            DropForeignKey("dbo.Pe02_Eleccion", "TipoEleccionId", "dbo.Pe03_TipoEleccion");
            DropForeignKey("dbo.Pe01_ProcesoElectoral", "EleccionId", "dbo.Pe02_Eleccion");
            DropForeignKey("dbo.Pe04_Escanio", "EleccionId", "dbo.Pe02_Eleccion");
            DropForeignKey("dbo.Sg04_UsuarioCargo", "UsuarioId", "dbo.Sg01_Usuario");
            DropForeignKey("dbo.Sg04_UsuarioCargo", "CargoId", "dbo.Sg03_Cargo");
            DropForeignKey("dbo.Sg01_Usuario", "PersonaId", "dbo.Sg02_Persona");
            DropForeignKey("dbo.Mi01_PadronVotacion", "UsuarioId", "dbo.Sg01_Usuario");
            DropForeignKey("dbo.Pe06_Candidato", "PersonaId", "dbo.Sg02_Persona");
            DropForeignKey("dbo.Pe05_Lista", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral");
            DropForeignKey("dbo.Pe06_Candidato", "ListaId", "dbo.Pe05_Lista");
            DropForeignKey("dbo.Pe06_Candidato", "EscanioId", "dbo.Pe04_Escanio");
            DropForeignKey("dbo.Ge01_Mensaje", "TipoMensajeId", "dbo.Ge02_TipoMensaje");
            DropIndex("dbo.Mi01_PadronVotacion", new[] { "ProcesoElectoralId" });
            DropIndex("dbo.Pe02_Eleccion", new[] { "TipoEleccionId" });
            DropIndex("dbo.Pe01_ProcesoElectoral", new[] { "EleccionId" });
            DropIndex("dbo.Pe04_Escanio", new[] { "EleccionId" });
            DropIndex("dbo.Sg04_UsuarioCargo", new[] { "UsuarioId" });
            DropIndex("dbo.Sg04_UsuarioCargo", new[] { "CargoId" });
            DropIndex("dbo.Sg01_Usuario", new[] { "PersonaId" });
            DropIndex("dbo.Mi01_PadronVotacion", new[] { "UsuarioId" });
            DropIndex("dbo.Pe06_Candidato", new[] { "PersonaId" });
            DropIndex("dbo.Pe05_Lista", new[] { "ProcesoElectoralId" });
            DropIndex("dbo.Pe06_Candidato", new[] { "ListaId" });
            DropIndex("dbo.Pe06_Candidato", new[] { "EscanioId" });
            DropIndex("dbo.Ge01_Mensaje", new[] { "TipoMensajeId" });
            DropTable("dbo.Pe03_TipoEleccion");
            DropTable("dbo.Sg03_Cargo");
            DropTable("dbo.Sg04_UsuarioCargo");
            DropTable("dbo.Sg01_Usuario");
            DropTable("dbo.Sg02_Persona");
            DropTable("dbo.Pe05_Lista");
            DropTable("dbo.Pe06_Candidato");
            DropTable("dbo.Pe04_Escanio");
            DropTable("dbo.Pe02_Eleccion");
            DropTable("dbo.Pe01_ProcesoElectoral");
            DropTable("dbo.Mi01_PadronVotacion");
            DropTable("dbo.Ge02_TipoMensaje");
            DropTable("dbo.Ge01_Mensaje");
        }
    }
}
