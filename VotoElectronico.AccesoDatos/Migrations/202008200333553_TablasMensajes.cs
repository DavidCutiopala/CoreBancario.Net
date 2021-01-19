namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasMensajes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ge01_Mensaje",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodigoAux = c.String(maxLength: 8000, unicode: false),
                        Descripcion = c.String(maxLength: 8000, unicode: false),
                        TipoMensajeId = c.Long(nullable: false),
                        Estado = c.String(nullable: false, maxLength: 10),
                        UsuarioCreacion = c.String(nullable: false, maxLength: 80),
                        FechaCreacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ge02_TipoMensaje", t => t.TipoMensajeId)
                .Index(t => t.TipoMensajeId);
            
            CreateTable(
                "dbo.Ge02_TipoMensaje",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodigoAux = c.String(maxLength: 8000, unicode: false),
                        Descripcion = c.String(maxLength: 8000, unicode: false),
                        Estado = c.String(nullable: false, maxLength: 10),
                        UsuarioCreacion = c.String(nullable: false, maxLength: 80),
                        FechaCreacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
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
