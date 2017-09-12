using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class PatientMapping : EntityTypeConfiguration<Patient>
    {
        public PatientMapping()
        {
            HasKey(e => e.Id);
            HasRequired(d => d.UserDetails)
                .WithOptional(u => u.Patient)
                .WillCascadeOnDelete(true);

            HasOptional(t => t.Request)
                .WithRequired(t => t.Patient);
            HasOptional(t => t.Doctor)
                    .WithMany(t => t.Patients)
                    .HasForeignKey(t => t.DoctorId)
                    .WillCascadeOnDelete(true);
        }
    }
}
