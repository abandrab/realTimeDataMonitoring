using Domain.Model.Models.SqlServer;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class RequestMapping : EntityTypeConfiguration<Request>
    {
        public RequestMapping()
        {
            HasKey(r => new { r.PatientId });

            HasRequired(t => t.Doctor)
                .WithMany(t => t.Requests)
                .HasForeignKey(t => t.DoctorId)
                .WillCascadeOnDelete(true);
        }
    }
}
