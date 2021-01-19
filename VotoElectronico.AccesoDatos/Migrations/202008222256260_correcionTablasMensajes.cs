namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcionTablasMensajes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ge01_Mensaje", "TipoMensajeId", "dbo.Ge02_TipoMensaje");
            DropIndex("dbo.Ge01_Mensaje", new[] { "TipoMensajeId" });
            AlterColumn("dbo.Ge01_Mensaje", "CodigoAux", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Ge01_Mensaje", "Descripcion", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Ge01_Mensaje", "TipoMensajeId", c => c.String());
            AlterColumn("dbo.Ge01_Mensaje", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Ge01_Mensaje", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Ge01_Mensaje", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Ge02_TipoMensaje", "CodigoAux", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Ge02_TipoMensaje", "Descripcion", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Ge02_TipoMensaje", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Ge02_TipoMensaje", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Ge02_TipoMensaje", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg01_Usuario", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Sg01_Usuario", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Sg01_Usuario", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Mi01_PadronVotacion", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Mi01_PadronVotacion", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Mi01_PadronVotacion", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe02_Eleccion", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pe02_Eleccion", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Pe02_Eleccion", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe03_TipoEleccion", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pe03_TipoEleccion", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Pe03_TipoEleccion", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe04_TipoEscanio", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pe04_TipoEscanio", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Pe04_TipoEscanio", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg02_Persona", "NombreUno", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Sg02_Persona", "NombreDos", c => c.String(maxLength: 100));
            AlterColumn("dbo.Sg02_Persona", "ApellidoUno", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Sg02_Persona", "ApellidoDos", c => c.String(maxLength: 100));
            AlterColumn("dbo.Sg02_Persona", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Sg02_Persona", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Sg02_Persona", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg04_UsuarioCargo", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Sg04_UsuarioCargo", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Sg04_UsuarioCargo", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg03_Cargo", "Estado", c => c.String(maxLength: 10));
            AlterColumn("dbo.Sg03_Cargo", "UsuarioCreacion", c => c.String(maxLength: 80));
            AlterColumn("dbo.Sg03_Cargo", "FechaCreacion", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropPrimaryKey("dbo.Ge02_TipoMensaje");
            AddPrimaryKey("dbo.Ge02_TipoMensaje", "CodigoAux");
            CreateIndex("dbo.Ge01_Mensaje", "CodigoAux");
            AddForeignKey("dbo.Ge01_Mensaje", "CodigoAux", "dbo.Ge02_TipoMensaje", "CodigoAux");
            DropColumn("dbo.Ge02_TipoMensaje", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ge02_TipoMensaje", "Id", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.Ge01_Mensaje", "CodigoAux", "dbo.Ge02_TipoMensaje");
            DropIndex("dbo.Ge01_Mensaje", new[] { "CodigoAux" });
            DropPrimaryKey("dbo.Ge02_TipoMensaje");
            AddPrimaryKey("dbo.Ge02_TipoMensaje", "Id");
            AlterColumn("dbo.Sg03_Cargo", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg03_Cargo", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Sg03_Cargo", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Sg04_UsuarioCargo", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg04_UsuarioCargo", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Sg04_UsuarioCargo", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Sg02_Persona", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg02_Persona", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Sg02_Persona", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Sg02_Persona", "ApellidoDos", c => c.String());
            AlterColumn("dbo.Sg02_Persona", "ApellidoUno", c => c.String(nullable: false));
            AlterColumn("dbo.Sg02_Persona", "NombreDos", c => c.String());
            AlterColumn("dbo.Sg02_Persona", "NombreUno", c => c.String(nullable: false));
            AlterColumn("dbo.Pe04_TipoEscanio", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe04_TipoEscanio", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Pe04_TipoEscanio", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Pe03_TipoEleccion", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe03_TipoEleccion", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Pe03_TipoEleccion", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Pe02_Eleccion", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe02_Eleccion", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Pe02_Eleccion", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Pe01_ProcesoElectoral", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Mi01_PadronVotacion", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Mi01_PadronVotacion", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Mi01_PadronVotacion", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Sg01_Usuario", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sg01_Usuario", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Sg01_Usuario", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Ge02_TipoMensaje", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Ge02_TipoMensaje", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Ge02_TipoMensaje", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Ge02_TipoMensaje", "Descripcion", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Ge02_TipoMensaje", "CodigoAux", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Ge01_Mensaje", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Ge01_Mensaje", "UsuarioCreacion", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Ge01_Mensaje", "Estado", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Ge01_Mensaje", "TipoMensajeId", c => c.Long(nullable: false));
            AlterColumn("dbo.Ge01_Mensaje", "Descripcion", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Ge01_Mensaje", "CodigoAux", c => c.String(maxLength: 8000, unicode: false));
            CreateIndex("dbo.Ge01_Mensaje", "TipoMensajeId");
            AddForeignKey("dbo.Ge01_Mensaje", "TipoMensajeId", "dbo.Ge02_TipoMensaje", "Id");
        }
    }
}
