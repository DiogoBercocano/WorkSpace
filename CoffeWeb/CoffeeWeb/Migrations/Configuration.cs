namespace CoffeeWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeWeb.Models.CoffeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoffeeWeb.Models.CoffeeContext context)
        {
            context.Produtos.AddOrUpdate(p => p.Nome,
            new Models.Produto { Nome = "Café Expresso", Descricao = "Puro e forte", Preco = 5.50m, ImagemUrl = "~/Content/img/expresso.jpg" },
            new Models.Produto { Nome = "Cappuccino", Descricao = "Com espuma de leite", Preco = 8.90m, ImagemUrl = "~/Content/img/cappuccino.jpg" }
            );
            // Adiciona o usuário administrador padrão
            context.Usuarios.AddOrUpdate(u => u.Email,
                new Models.Usuario
                {
                    Nome = "Administrador",
                    Telefone = "11999998888",
                    Email = "admin@cafe.com",
                    Senha = "123" // Em um sistema real, a senha deve ser criptografada (Hash)!
                }
            );
        }
    }
}