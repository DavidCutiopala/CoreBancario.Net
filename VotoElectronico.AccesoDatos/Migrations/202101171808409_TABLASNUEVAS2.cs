namespace VotoElectronico.AccesoDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TABLASNUEVAS2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CB01_Cliente",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50, unicode: false),
                        Apellido = c.String(maxLength: 50, unicode: false),
                        Cedula = c.String(maxLength: 30, unicode: false),
                        Direccion = c.String(maxLength: 100, unicode: false),
                        Telefono = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CB02_Cuenta",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Saldo = c.Decimal(nullable: false, precision: 20, scale: 6),
                        FechaCreacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NumeroCuenta = c.String(maxLength: 10, unicode: false),
                        TipoCuenta = c.String(maxLength: 100, unicode: false),
                        ClienteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CB01_Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.CB03_Movimiento",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FechaCreacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TipoMovimiento = c.String(maxLength: 64, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 20, scale: 6),
                        SaldoFinal = c.Decimal(nullable: false, precision: 20, scale: 6),
                        CuentaOrigen = c.String(maxLength: 10, unicode: false),
                        CuentaDestino = c.String(maxLength: 10, unicode: false),
                        CuentaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CB02_Cuenta", t => t.CuentaId)
                .Index(t => t.CuentaId);
            
            CreateTable(
                "dbo.CB04_Usuario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Usuario = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 100, unicode: false),
                        ClienteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CB01_Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CB04_Usuario", "ClienteId", "dbo.CB01_Cliente");
            DropForeignKey("dbo.CB03_Movimiento", "CuentaId", "dbo.CB02_Cuenta");
            DropForeignKey("dbo.CB02_Cuenta", "ClienteId", "dbo.CB01_Cliente");
            DropIndex("dbo.CB04_Usuario", new[] { "ClienteId" });
            DropIndex("dbo.CB03_Movimiento", new[] { "CuentaId" });
            DropIndex("dbo.CB02_Cuenta", new[] { "ClienteId" });
            DropTable("dbo.CB04_Usuario");
            DropTable("dbo.CB03_Movimiento");
            DropTable("dbo.CB02_Cuenta");
            DropTable("dbo.CB01_Cliente");
        }
    }
}
