using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersQueryResult>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public GetAllUsersQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            this.userDetailsRepository = userDetailsRepository;
        }
        public GetAllUsersQueryResult Retrieve(GetAllUsersQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var users = this.userDetailsRepository.GetAll();
            return new GetAllUsersQueryResult(users.ToList());
        }
    }
}
