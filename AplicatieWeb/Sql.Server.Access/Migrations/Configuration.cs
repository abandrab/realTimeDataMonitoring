namespace Sql.Server.Access.Migrations
{
    using Domain.Model.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MainContext context)
        {
            
        }
    }
}
