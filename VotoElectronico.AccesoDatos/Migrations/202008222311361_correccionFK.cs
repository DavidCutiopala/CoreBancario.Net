namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correccionFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ge01_Mensaje", "CodigoAux", "dbo.Ge02_TipoMensaje");
            DropIndex("dbo.Ge01_Mensaje", new[] { "CodigoAux" });
            DropTable("dbo.Ge01_Mensaje");
            DropTable("dbo.Ge02_TipoMensaje");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ge02_TipoMensaje",
                c => new
                    {
                        CodigoAux = c.String(nullable: false, maxLength: 200, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 200, unicode: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CodigoAux);
            
            CreateTable(
                "dbo.Ge01_Mensaje",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 200, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 200, unicode: false),
                        CodigoAux = c.String(maxLength: 200, unicode: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateIndex("dbo.Ge01_Mensaje", "CodigoAux");
            AddForeignKey("dbo.Ge01_Mensaje", "CodigoAux", "dbo.Ge02_TipoMensaje", "CodigoAux");
        }
    }
}
