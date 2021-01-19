namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionTablaRecursos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sg06_Recurso", "Recurso_Id", "dbo.Sg06_Recurso");
            DropIndex("dbo.Sg06_Recurso", new[] { "Recurso_Id" });
            AddColumn("dbo.Sg06_Recurso", "Sg06_Recurso_Id", c => c.Long());
            CreateIndex("dbo.Sg06_Recurso", "Sg06_Recurso_Id");
            AddForeignKey("dbo.Sg06_Recurso", "Sg06_Recurso_Id", "dbo.Sg06_Recurso", "Id");
            DropColumn("dbo.Sg06_Recurso", "RecursoIdPadre");
            DropColumn("dbo.Sg06_Recurso", "Recurso_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sg06_Recurso", "Recurso_Id", c => c.Long());
            AddColumn("dbo.Sg06_Recurso", "RecursoIdPadre", c => c.Long(nullable: false));
            DropForeignKey("dbo.Sg06_Recurso", "Sg06_Recurso_Id", "dbo.Sg06_Recurso");
            DropIndex("dbo.Sg06_Recurso", new[] { "Sg06_Recurso_Id" });
            DropColumn("dbo.Sg06_Recurso", "Sg06_Recurso_Id");
            CreateIndex("dbo.Sg06_Recurso", "Recurso_Id");
            AddForeignKey("dbo.Sg06_Recurso", "Recurso_Id", "dbo.Sg06_Recurso", "Id");
        }
    }
}
