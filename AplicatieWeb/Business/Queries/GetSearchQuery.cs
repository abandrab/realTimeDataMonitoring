using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetSearchQuery : IQuery
    {
        public GetSearchQuery(string searchString)
        {
            this.SearchString = searchString;
        }
        public string SearchString { get; private set; }
    }
}
