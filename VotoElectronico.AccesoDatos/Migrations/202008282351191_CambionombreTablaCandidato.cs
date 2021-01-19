namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambionombreTablaCandidato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pe06_Candidato",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonaId = c.Long(nullable: false),
                        TipoEscanioId = c.Long(nullable: false),
                        ListaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Pe06_PostulanteEscanio");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pe06_PostulanteEscanio",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonaId = c.Long(nullable: false),
                        TipoEscanioId = c.Long(nullable: false),
                        ListaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Pe06_Candidato");
        }
    }
}
