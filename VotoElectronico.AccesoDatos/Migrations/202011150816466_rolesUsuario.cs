namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolesUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sg07_Rol",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreRol = c.String(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sg08_UsuarioRol",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RolId = c.Long(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sg07_Rol", t => t.RolId)
                .ForeignKey("dbo.Sg01_Usuario", t => t.UsuarioId)
                .Index(t => t.RolId)
                .Index(t => t.UsuarioId);
            
            AddColumn("dbo.Sg05_Permiso", "RolId", c => c.Long(nullable: false));
            CreateIndex("dbo.Sg05_Permiso", "RolId");
            AddForeignKey("dbo.Sg05_Permiso", "RolId", "dbo.Sg07_Rol", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sg08_UsuarioRol", "UsuarioId", "dbo.Sg01_Usuario");
            DropForeignKey("dbo.Sg08_UsuarioRol", "RolId", "dbo.Sg07_Rol");
            DropForeignKey("dbo.Sg05_Permiso", "RolId", "dbo.Sg07_Rol");
            DropIndex("dbo.Sg08_UsuarioRol", new[] { "UsuarioId" });
            DropIndex("dbo.Sg08_UsuarioRol", new[] { "RolId" });
            DropIndex("dbo.Sg05_Permiso", new[] { "RolId" });
            DropColumn("dbo.Sg05_Permiso", "RolId");
            DropTable("dbo.Sg08_UsuarioRol");
            DropTable("dbo.Sg07_Rol");
        }
    }
}
