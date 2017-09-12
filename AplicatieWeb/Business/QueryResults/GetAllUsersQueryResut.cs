using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetAllUsersQueryResult : IQueryResult
    {
        public GetAllUsersQueryResult(IEnumerable<UserDetails> Users)
        {
            this.Users = Users;
        }
        public IEnumerable<UserDetails> Users { get; private set; }
    }
}
