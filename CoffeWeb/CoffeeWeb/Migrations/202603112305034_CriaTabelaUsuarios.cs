namespace CoffeeWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaTabelaUsuarios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Telefone = c.String(),
                        Email = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
        }
    }
}
