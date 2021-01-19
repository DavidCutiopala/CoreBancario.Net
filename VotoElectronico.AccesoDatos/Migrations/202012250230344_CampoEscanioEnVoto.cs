namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoEscanioEnVoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mv02_Opcion", "EscanioId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mv02_Opcion", "EscanioId");
        }
    }
}
