namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoFotoEnCandidato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe06_Candidato", "Foto", c => c.String(maxLength: 60));
            DropColumn("dbo.Pe06_Candidato", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pe06_Candidato", "Logo", c => c.String(maxLength: 60));
            DropColumn("dbo.Pe06_Candidato", "Foto");
        }
    }
}
