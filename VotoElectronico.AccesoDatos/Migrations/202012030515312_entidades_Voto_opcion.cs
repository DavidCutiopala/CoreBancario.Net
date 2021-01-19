namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entidades_Voto_opcion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mv02_Opcion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProcesoElectoralId = c.Long(nullable: false),
                        ListaId = c.Long(nullable: false),
                        CandidatoId = c.Long(),
                        VotoId = c.Long(nullable: false),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pe06_Candidato", t => t.CandidatoId)
                .ForeignKey("dbo.Pe05_Lista", t => t.ListaId)
                .ForeignKey("dbo.Pe01_ProcesoElectoral", t => t.ProcesoElectoralId)
                .ForeignKey("dbo.Mv01_Voto", t => t.VotoId)
                .Index(t => t.CandidatoId)
                .Index(t => t.ListaId)
                .Index(t => t.ProcesoElectoralId)
                .Index(t => t.VotoId);
            
            CreateTable(
                "dbo.Mv01_Voto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Mascara = c.String(),
                        Estado = c.String(maxLength: 10),
                        UsuarioCreacion = c.String(maxLength: 80),
                        FechaCreacion = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioModificacion = c.String(maxLength: 80),
                        FechaModificacion = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mv02_Opcion", "VotoId", "dbo.Mv01_Voto");
            DropForeignKey("dbo.Mv02_Opcion", "ProcesoElectoralId", "dbo.Pe01_ProcesoElectoral");
            DropForeignKey("dbo.Mv02_Opcion", "ListaId", "dbo.Pe05_Lista");
            DropForeignKey("dbo.Mv02_Opcion", "CandidatoId", "dbo.Pe06_Candidato");
            DropIndex("dbo.Mv02_Opcion", new[] { "VotoId" });
            DropIndex("dbo.Mv02_Opcion", new[] { "ProcesoElectoralId" });
            DropIndex("dbo.Mv02_Opcion", new[] { "ListaId" });
            DropIndex("dbo.Mv02_Opcion", new[] { "CandidatoId" });
            DropTable("dbo.Mv01_Voto");
            DropTable("dbo.Mv02_Opcion");
        }
    }
}
