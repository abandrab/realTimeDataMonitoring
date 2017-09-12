using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class DoctorMapping : EntityTypeConfiguration<Doctor>
    {
        public DoctorMapping()
        {
            HasKey(e => e.Id);
            //HasRequired(d => d.UserDetails)
            //    .WithOptional(u => u.Doctor)
            //    .WillCascadeOnDelete(true);
        }
    }
}
