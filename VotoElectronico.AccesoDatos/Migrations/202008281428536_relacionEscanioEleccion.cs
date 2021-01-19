namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relacionEscanioEleccion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe04_Escanio", "NombreEscanio", c => c.String(nullable: false));
            DropColumn("dbo.Pe04_Escanio", "NombreTipoEscanio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pe04_Escanio", "NombreTipoEscanio", c => c.String(nullable: false));
            DropColumn("dbo.Pe04_Escanio", "NombreEscanio");
        }
    }
}
