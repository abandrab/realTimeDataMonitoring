using Autofac;
using Vanguard;

namespace Common.Layer.CqrsCore
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext context;

        public QueryDispatcher(IComponentContext context)
        {
            Guard.ArgumentNotNull(context, nameof(context));
            this.context = context;
        }

        public TQueryResult Dispatch<TQuery, TQueryResult>(TQuery query)
            where TQuery : IQuery
            where TQueryResult : IQueryResult
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryHandler = context.Resolve<IQueryHandler<TQuery, TQueryResult>>();
            return queryHandler.Retrieve(query);
        }
    }
}
