namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionRecursoCircular : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sg06_Recurso", "Sg06_Recurso_Id", "dbo.Sg06_Recurso");
            DropIndex("dbo.Sg06_Recurso", new[] { "Sg06_Recurso_Id" });
            AddColumn("dbo.Sg06_Recurso", "RecursoId", c => c.Long());
            CreateIndex("dbo.Sg06_Recurso", "RecursoId");
            AddForeignKey("dbo.Sg06_Recurso", "RecursoId", "dbo.Sg06_Recurso", "Id");
            DropColumn("dbo.Sg06_Recurso", "Sg06_Recurso_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sg06_Recurso", "Sg06_Recurso_Id", c => c.Long());
            DropForeignKey("dbo.Sg06_Recurso", "RecursoId", "dbo.Sg06_Recurso");
            DropIndex("dbo.Sg06_Recurso", new[] { "RecursoId" });
            DropColumn("dbo.Sg06_Recurso", "RecursoId");
            CreateIndex("dbo.Sg06_Recurso", "Sg06_Recurso_Id");
            AddForeignKey("dbo.Sg06_Recurso", "Sg06_Recurso_Id", "dbo.Sg06_Recurso", "Id");
        }
    }
}
