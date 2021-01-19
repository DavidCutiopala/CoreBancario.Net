namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditoriaATablasListaYCandidato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe06_Candidato", "Estado", c => c.String(maxLength: 10));
            AddColumn("dbo.Pe06_Candidato", "UsuarioCreacion", c => c.String(maxLength: 80));
            AddColumn("dbo.Pe06_Candidato", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Pe06_Candidato", "UsuarioModificacion", c => c.String(maxLength: 80));
            AddColumn("dbo.Pe06_Candidato", "FechaModificacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pe06_Candidato", "FechaModificacion");
            DropColumn("dbo.Pe06_Candidato", "UsuarioModificacion");
            DropColumn("dbo.Pe06_Candidato", "FechaCreacion");
            DropColumn("dbo.Pe06_Candidato", "UsuarioCreacion");
            DropColumn("dbo.Pe06_Candidato", "Estado");
        }
    }
}
