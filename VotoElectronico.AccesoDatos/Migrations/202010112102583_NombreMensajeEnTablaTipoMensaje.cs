namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NombreMensajeEnTablaTipoMensaje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ge02_TipoMensaje", "NombreMensaje", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ge02_TipoMensaje", "NombreMensaje");
        }
    }
}
