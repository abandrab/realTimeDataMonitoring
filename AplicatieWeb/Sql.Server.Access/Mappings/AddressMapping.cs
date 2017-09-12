using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class AddressMapping : EntityTypeConfiguration<Address>
    {
        public AddressMapping()
        {
            HasKey(e => e.Id);
            HasRequired(d => d.UserDetails)
                .WithOptional(u => u.Address)
                .WillCascadeOnDelete(true);
        }
    }
}
