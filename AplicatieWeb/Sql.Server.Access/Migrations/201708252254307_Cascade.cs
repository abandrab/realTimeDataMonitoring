namespace Sql.Server.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            AddForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            AddForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors", "Id");
        }
    }
}
