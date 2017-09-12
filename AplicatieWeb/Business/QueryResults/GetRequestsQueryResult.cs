using Common.Layer.CqrsCore;
using Domain.Model.Models.SqlServer;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetRequestsQueryResult : IQueryResult
    {
        public GetRequestsQueryResult(List<Request> requests)
        {
            Requests = requests;
        }
        public List<Request> Requests { get; private set; }
    }
}
