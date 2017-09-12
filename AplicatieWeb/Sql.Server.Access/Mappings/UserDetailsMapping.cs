using Domain.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sql.Server.Access.Mappings
{
    public class UserDetailsMapping : EntityTypeConfiguration<UserDetails>
    {

        public UserDetailsMapping()
        {
            HasOptional(u => u.Patient)
                .WithRequired(p => p.UserDetails)
                .WillCascadeOnDelete(true);
            HasOptional(u => u.Doctor)
               .WithRequired(p => p.UserDetails)
               .WillCascadeOnDelete(true);
            HasOptional(u => u.Assistant)
               .WithRequired(p => p.UserDetails)
               .WillCascadeOnDelete(true);
            HasOptional(u => u.Administrator)
               .WithRequired(p => p.UserDetails)
               .WillCascadeOnDelete(true);
            HasOptional(u => u.Address)
               .WithRequired(p => p.UserDetails)
               .WillCascadeOnDelete(true);
        }
    }
}
