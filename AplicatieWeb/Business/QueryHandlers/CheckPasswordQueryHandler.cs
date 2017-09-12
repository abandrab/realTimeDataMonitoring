using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;
using Common.Layer.Auth;

namespace Business.QueryHandlers
{
    public class CheckPasswordQueryHandler : IQueryHandler<CheckPasswordQuery, CheckPasswordQueryResult>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public CheckPasswordQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            this.userDetailsRepository = userDetailsRepository;
        }

        public CheckPasswordQueryResult Retrieve(CheckPasswordQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var user = userDetailsRepository.GetBy(u => u.Email.Equals(query.Email)).FirstOrDefault();
            if(user != null)
            {
                if (PasswordManager.ValidatePassword(query.Password, user.Password))
                {
                    return new CheckPasswordQueryResult(true);
                }
            }
            return new CheckPasswordQueryResult(false);
        }
    }
}
