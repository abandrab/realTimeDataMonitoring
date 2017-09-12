using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class GetEmailQueryResult : IQueryResult
    {
        public GetEmailQueryResult(bool exists)
        {
            this.Exists = exists;
        }
        public bool Exists { get; private set; }
    }
}
