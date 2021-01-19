namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosTablaProcesoElectoral : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe01_ProcesoElectoral", "NombreProcesoElectoral", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("dbo.Pe01_ProcesoElectoral", "FechaInicio", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Pe01_ProcesoElectoral", "FechaFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pe01_ProcesoElectoral", "FechaFin");
            DropColumn("dbo.Pe01_ProcesoElectoral", "FechaInicio");
            DropColumn("dbo.Pe01_ProcesoElectoral", "NombreProcesoElectoral");
        }
    }
}
