using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class AssistantMapping : EntityTypeConfiguration<Assistant>
    {
        public AssistantMapping()
        {
            HasKey(e => e.Id);
            HasRequired(d => d.UserDetails)
                .WithOptional(u => u.Assistant)
                .WillCascadeOnDelete(true);
        }
    }
}
