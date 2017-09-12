namespace Common.Layer.CqrsCore
{
    public interface IQueryHandler<in TQuery, out TQueryResult>
        where TQuery : IQuery
        where TQueryResult : IQueryResult
    {
        TQueryResult Retrieve(TQuery query);
    }
}
