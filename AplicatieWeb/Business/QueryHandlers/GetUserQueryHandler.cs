using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public GetUserQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            this.userDetailsRepository = userDetailsRepository;
        }
        public GetUserQueryResult Retrieve(GetUserQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var userDetails = this.userDetailsRepository.GetBy(uc => (uc.Email == query.Email) && (uc.Password == query.Password))
                .FirstOrDefault();
            return new GetUserQueryResult(userDetails);
        }
    }
}
