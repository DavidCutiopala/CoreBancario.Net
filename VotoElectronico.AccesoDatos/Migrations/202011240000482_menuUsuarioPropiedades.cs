namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuUsuarioPropiedades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sg06_Recurso", "RutaMenu", c => c.String(maxLength: 100));
            AddColumn("dbo.Sg06_Recurso", "Icono", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sg06_Recurso", "Icono");
            DropColumn("dbo.Sg06_Recurso", "RutaMenu");
        }
    }
}
