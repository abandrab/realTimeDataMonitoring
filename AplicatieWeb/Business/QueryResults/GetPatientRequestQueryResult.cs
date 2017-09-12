using Common.Layer.CqrsCore;
using Domain.Model.Models.SqlServer;

namespace Business.QueryResults
{
    public class GetPatientRequestQueryResult : IQueryResult
    {
        public GetPatientRequestQueryResult(Request request)
        {
            Request = request;
        }
        public Request Request { get; private set; }
    }
}
