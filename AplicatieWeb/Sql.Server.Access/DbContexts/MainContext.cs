using System.Data.Entity;
using Domain.Model.Models;
using Domain.Model.Models.SqlServer;
using Sql.Server.Access.Mappings;

namespace Sql.Server.Access
{
    public class MainContext : DbContext
    {
        public MainContext() : base("AppDb")
        {
         //   Configuration.LazyLoadingEnabled = true;
           // Configuration.ProxyCreationEnabled = true;
        }
        public IDbSet<UserDetails> UserDetails { get; set; }
        public IDbSet<Administrator> Administrators { get; set; }
        public IDbSet<Assistant> Assistants { get; set; }
        public IDbSet<Patient> Patients { get; set; }
        public IDbSet<Doctor> Doctors { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Coordinator> Coordinators { get; set; }
        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<Request> Requests { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RequestMapping());
            modelBuilder.Configurations.Add(new PatientMapping());
            modelBuilder.Configurations.Add(new AddressMapping());
            modelBuilder.Configurations.Add(new AssistantMapping());
            modelBuilder.Configurations.Add(new AdministratorMapping());
            modelBuilder.Configurations.Add(new DoctorMapping());
        }
    }
}