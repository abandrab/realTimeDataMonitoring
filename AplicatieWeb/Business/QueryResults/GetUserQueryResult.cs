using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetUserQueryResult : IQueryResult
    {
        public GetUserQueryResult(UserDetails userDetails)
        {
            this.UserDetails = userDetails;
        }
        public UserDetails UserDetails { get; private set; }
    }
}
