namespace Common.Layer.CqrsCore
{
    public interface IQueryDispatcher
    {
        TQueryResult Dispatch<TQuery, TQueryResult>(TQuery query)
            where TQuery : IQuery
            where TQueryResult : IQueryResult;
    }
}
