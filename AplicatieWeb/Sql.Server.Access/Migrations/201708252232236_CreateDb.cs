namespace Sql.Server.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Role = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstChange = c.Boolean(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Sex = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Assistants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CNP = c.String(),
                        RangeNumber = c.String(),
                        Birthdate = c.DateTime(),
                        DoctorId = c.Guid(),
                        Assistant_Id = c.Guid(),
                        Coordinator_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assistants", t => t.Assistant_Id)
                .ForeignKey("dbo.Coordinators", t => t.Coordinator_Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DoctorId)
                .Index(t => t.Assistant_Id)
                .Index(t => t.Coordinator_Id);
            
            CreateTable(
                "dbo.Coordinators",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IP = c.String(nullable: false),
                        MaxNoPatients = c.Int(nullable: false),
                        Administrator_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrators", t => t.Administrator_Id)
                .Index(t => t.Administrator_Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorId = c.Int(nullable: false),
                        SensorType = c.Int(nullable: false),
                        Coordinator_Id = c.Guid(),
                        Patient_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordinators", t => t.Coordinator_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Coordinator_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        PatientId = c.Guid(nullable: false),
                        DoctorId = c.Guid(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Doctors", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Patients", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Sensors", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Requests", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Requests", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Sensors", "Coordinator_Id", "dbo.Coordinators");
            DropForeignKey("dbo.Patients", "Coordinator_Id", "dbo.Coordinators");
            DropForeignKey("dbo.Coordinators", "Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.Patients", "Assistant_Id", "dbo.Assistants");
            DropForeignKey("dbo.Assistants", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Administrators", "Id", "dbo.UserDetails");
            DropIndex("dbo.Requests", new[] { "DoctorId" });
            DropIndex("dbo.Requests", new[] { "PatientId" });
            DropIndex("dbo.Sensors", new[] { "Patient_Id" });
            DropIndex("dbo.Sensors", new[] { "Coordinator_Id" });
            DropIndex("dbo.Coordinators", new[] { "Administrator_Id" });
            DropIndex("dbo.Patients", new[] { "Coordinator_Id" });
            DropIndex("dbo.Patients", new[] { "Assistant_Id" });
            DropIndex("dbo.Patients", new[] { "DoctorId" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropIndex("dbo.Assistants", new[] { "Id" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropTable("dbo.Requests");
            DropTable("dbo.Sensors");
            DropTable("dbo.Coordinators");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Assistants");
            DropTable("dbo.Administrators");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Addresses");
        }
    }
}
