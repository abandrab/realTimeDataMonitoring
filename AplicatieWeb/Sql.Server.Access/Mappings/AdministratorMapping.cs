using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class AdministratorMapping : EntityTypeConfiguration<Administrator>
    {
        public AdministratorMapping()
        {
            HasKey(e => e.Id);
            HasRequired(d => d.UserDetails)
                .WithOptional(u => u.Administrator)
                .WillCascadeOnDelete(true);
        }
    }
}
