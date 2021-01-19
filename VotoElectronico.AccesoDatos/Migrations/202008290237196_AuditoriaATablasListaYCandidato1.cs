namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditoriaATablasListaYCandidato1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pe05_Lista", "Estado", c => c.String(maxLength: 10));
            AddColumn("dbo.Pe05_Lista", "UsuarioCreacion", c => c.String(maxLength: 80));
            AddColumn("dbo.Pe05_Lista", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Pe05_Lista", "UsuarioModificacion", c => c.String(maxLength: 80));
            AddColumn("dbo.Pe05_Lista", "FechaModificacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pe05_Lista", "FechaModificacion");
            DropColumn("dbo.Pe05_Lista", "UsuarioModificacion");
            DropColumn("dbo.Pe05_Lista", "FechaCreacion");
            DropColumn("dbo.Pe05_Lista", "UsuarioCreacion");
            DropColumn("dbo.Pe05_Lista", "Estado");
        }
    }
}
