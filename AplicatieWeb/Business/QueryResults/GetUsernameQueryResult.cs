using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class GetUsernameQueryResult : IQueryResult
    {
        public GetUsernameQueryResult(string username)
        {
            this.Username = username;
        }
        public string Username { get; private set; }
    }
}
