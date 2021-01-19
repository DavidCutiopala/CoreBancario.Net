namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TABLASNUEVAS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sg06_Recurso", "Orden", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sg06_Recurso", "Orden");
        }
    }
}
