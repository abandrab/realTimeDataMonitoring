using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class GetCoordinatorQueryResult : IQueryResult
    {
        public GetCoordinatorQueryResult(string ip)
        {
            this.IP = ip;
        }
        public string IP { get; private set; }
    }
}
