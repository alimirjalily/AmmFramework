using AmmFramework.Core.Contracts.ApplicationServices.Queries;
using AmmFramework.Core.RequestResponse.Queries;

namespace AmmFramework.Core.ApplicationService.Queries;

public abstract class QueryDispatcherDecorator : IQueryDispatcher
{
    #region Fields
    protected IQueryDispatcher _queryDispatcher;
    public abstract int Order { get; }
    #endregion

    #region Constructors
    public QueryDispatcherDecorator() { }
    #endregion

    #region Query Dispatcher
    public abstract Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    #endregion

    #region Methods
    public void SetQueryDispatcher(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    #endregion
}