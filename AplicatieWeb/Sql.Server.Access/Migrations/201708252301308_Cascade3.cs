namespace Sql.Server.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Administrators", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Assistants", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Doctors", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Addresses", "Id", "dbo.UserDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Administrators", "Id", "dbo.UserDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Assistants", "Id", "dbo.UserDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Doctors", "Id", "dbo.UserDetails", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Assistants", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Administrators", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Addresses", "Id", "dbo.UserDetails");
            AddForeignKey("dbo.Doctors", "Id", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Assistants", "Id", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Administrators", "Id", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.UserDetails", "Id");
        }
    }
}
