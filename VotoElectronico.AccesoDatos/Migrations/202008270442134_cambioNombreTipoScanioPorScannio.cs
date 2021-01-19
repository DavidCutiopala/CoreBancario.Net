namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioNombreTipoScanioPorScannio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pe04_Escanio",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreTipoEscanio = c.String(nullable: false),
                        EleccionId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Pe04_TipoEscanio");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pe04_TipoEscanio",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreTipoEscanio = c.String(nullable: false),
                        EleccionId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Pe04_Escanio");
        }
    }
}
