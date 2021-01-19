namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarTablasMensajesAcontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ge01_Mensaje",
                c => new
                    {
                        CodigoMensaje = c.String(nullable: false, maxLength: 200, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 8000, unicode: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ge01_Mensaje", "TipoMensajeId", "dbo.Ge02_TipoMensaje");
            DropIndex("dbo.Ge01_Mensaje", new[] { "TipoMensajeId" });
            DropTable("dbo.Ge02_TipoMensaje");
            DropTable("dbo.Ge01_Mensaje");
        }
    }
}
