namespace Sql.Server.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Patients", "Id", "dbo.UserDetails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Patients", "Id", "dbo.UserDetails", "Id");
        }
    }
}
