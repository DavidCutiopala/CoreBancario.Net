namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevosCamposEnTablas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe06_Candidato", "Logo", c => c.String(maxLength: 60));
            AddColumn("dbo.Pe05_Lista", "Logo", c => c.String(maxLength: 60));
            AddColumn("dbo.Pe05_Lista", "Eslogan", c => c.String(maxLength: 200));
            AddColumn("dbo.Sg02_Persona", "Telefono2", c => c.String(maxLength: 20));
            AddColumn("dbo.Sg02_Persona", "Email", c => c.String(nullable: false, maxLength: 60, unicode: false));
            DropColumn("dbo.Sg01_Usuario", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sg01_Usuario", "Email", c => c.String(nullable: false, maxLength: 60, unicode: false));
            DropColumn("dbo.Sg02_Persona", "Email");
            DropColumn("dbo.Sg02_Persona", "Telefono2");
            DropColumn("dbo.Pe05_Lista", "Eslogan");
            DropColumn("dbo.Pe05_Lista", "Logo");
            DropColumn("dbo.Pe06_Candidato", "Logo");
        }
    }
}
