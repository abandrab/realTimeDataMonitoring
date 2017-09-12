using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using System.Linq;

namespace Business.QueryHandlers
{
    public class GetEmailQueryHandler : IQueryHandler<GetEmailQuery, GetEmailQueryResult>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public GetEmailQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            this.userDetailsRepository = userDetailsRepository;
        }
        public GetEmailQueryResult Retrieve(GetEmailQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var user = this.userDetailsRepository.GetBy(p => p.Email == query.Email).FirstOrDefault();
            if(user == null)
            {
                return new GetEmailQueryResult(false);
            }
            return new GetEmailQueryResult(true);
        }
    }
}
