namespace Sql.Server.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Doctors", "Id", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Doctors", "Id", "dbo.UserDetails", "Id", cascadeDelete: true);
        }
    }
}
