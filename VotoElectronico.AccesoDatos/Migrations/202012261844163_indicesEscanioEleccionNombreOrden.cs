namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indicesEscanioEleccionNombreOrden : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pe04_Escanio", new[] { "EleccionId" });
            AddColumn("dbo.Pe02_Eleccion", "CantidadEscanios", c => c.Int(nullable: false));
            AddColumn("dbo.Pe02_Eleccion", "EtiquetaEscanios", c => c.String());
            AddColumn("dbo.Pe04_Escanio", "Orden", c => c.Int(nullable: false));
            AlterColumn("dbo.Pe04_Escanio", "NombreEscanio", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Pe04_Escanio", new[] { "NombreEscanio", "EleccionId" }, unique: true, name: "IX_NombreEleccion");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pe04_Escanio", "IX_NombreEleccion");
            AlterColumn("dbo.Pe04_Escanio", "NombreEscanio", c => c.String(nullable: false));
            DropColumn("dbo.Pe04_Escanio", "Orden");
            DropColumn("dbo.Pe02_Eleccion", "EtiquetaEscanios");
            DropColumn("dbo.Pe02_Eleccion", "CantidadEscanios");
            CreateIndex("dbo.Pe04_Escanio", "EleccionId");
        }
    }
}
