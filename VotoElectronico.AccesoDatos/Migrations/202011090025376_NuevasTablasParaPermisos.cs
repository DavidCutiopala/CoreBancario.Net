namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevasTablasParaPermisos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sg05_Permiso",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CargoId = c.Long(nullable: false),
                        RecursoId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg03_Cargo", t => t.CargoId)
                .ForeignKey("dbo.Sg06_Recurso", t => t.RecursoId)
                .Index(t => t.CargoId)
                .Index(t => t.RecursoId);
            
            CreateTable(
                "dbo.Sg06_Recurso",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodigoRecurso = c.String(nullable: false, maxLength: 20),
                        NombreRecurso = c.String(maxLength: 50),
                        DescripcionRecurso = c.String(maxLength: 100),
                        ValorRecursoString = c.String(maxLength: 200),
                        ValorRecursoBoolean = c.String(maxLength: 100),
                        RecursoIdPadre = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        Recurso_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg06_Recurso", t => t.Recurso_Id)
                .Index(t => t.Recurso_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sg06_Recurso", "Recurso_Id", "dbo.Sg06_Recurso");
            DropForeignKey("dbo.Sg05_Permiso", "RecursoId", "dbo.Sg06_Recurso");
            DropForeignKey("dbo.Sg05_Permiso", "CargoId", "dbo.Sg03_Cargo");
            DropIndex("dbo.Sg06_Recurso", new[] { "Recurso_Id" });
            DropIndex("dbo.Sg05_Permiso", new[] { "RecursoId" });
            DropIndex("dbo.Sg05_Permiso", new[] { "CargoId" });
            DropTable("dbo.Sg06_Recurso");
            DropTable("dbo.Sg05_Permiso");
        }
    }
}
