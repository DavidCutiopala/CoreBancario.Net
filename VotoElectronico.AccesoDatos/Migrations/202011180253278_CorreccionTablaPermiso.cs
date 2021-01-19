namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionTablaPermiso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sg05_Permiso", "CargoId", "dbo.Sg03_Cargo");
            DropIndex("dbo.Sg05_Permiso", new[] { "CargoId" });
            DropColumn("dbo.Sg05_Permiso", "CargoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sg05_Permiso", "CargoId", c => c.Long(nullable: false));
            CreateIndex("dbo.Sg05_Permiso", "CargoId");
            AddForeignKey("dbo.Sg05_Permiso", "CargoId", "dbo.Sg03_Cargo", "Id");
        }
    }
}
