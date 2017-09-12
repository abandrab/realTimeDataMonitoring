using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class GetDoctorInfosQueryResult : IQueryResult
    {
        public GetDoctorInfosQueryResult(string id)
        {
            this.Id = id;
        }
        public string Id { get; private set; }
    }
}
