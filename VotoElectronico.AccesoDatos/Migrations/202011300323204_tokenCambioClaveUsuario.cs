namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tokenCambioClaveUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sg01_Usuario", "TokenCambioClave", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sg01_Usuario", "TokenCambioClave");
        }
    }
}
