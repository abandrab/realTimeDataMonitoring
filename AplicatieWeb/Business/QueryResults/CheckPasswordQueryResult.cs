using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class CheckPasswordQueryResult : IQueryResult
    {
        public CheckPasswordQueryResult(bool correctPassword)
        {
            CorrectPassword = correctPassword;
        }
        public bool CorrectPassword { get; private set; }
    }
}
