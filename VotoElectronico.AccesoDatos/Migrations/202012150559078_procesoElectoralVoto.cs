namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class procesoElectoralVoto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mv02_Opcion", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral");
            DropIndex("dbo.Mv02_Opcion", new[] { "ProcesoElectoralId" });
            AddColumn("dbo.Mv01_Voto", "ProcesoElectoralId", c => c.Long(nullable: false));
            CreateIndex("dbo.Mv01_Voto", "ProcesoElectoralId");
            AddForeignKey("dbo.Mv01_Voto", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral", "Id");
            DropColumn("dbo.Mv02_Opcion", "ProcesoElectoralId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mv02_Opcion", "ProcesoElectoralId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Mv01_Voto", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral");
            DropIndex("dbo.Mv01_Voto", new[] { "ProcesoElectoralId" });
            DropColumn("dbo.Mv01_Voto", "ProcesoElectoralId");
            CreateIndex("dbo.Mv02_Opcion", "ProcesoElectoralId");
            AddForeignKey("dbo.Mv02_Opcion", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral", "Id");
        }
    }
}
