using System.Linq.Expressions;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Specifications;

/// <summary>
/// Abstraction to the specification design pattern.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TEntityId">Entity id type.</typeparam>
public abstract class Specification<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Specification{TEntity, TEntityId}"/> class.
    /// </summary>
    /// <param name="criteria">Criteria expression.</param>
    protected Specification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets if the query should be splited.
    /// </summary>
    /// <value>
    /// If the query should be splited.
    /// </value>
    public bool IsSplitQuery { get; protected set; }

    /// <summary>
    /// Gets the criteria expression.
    /// </summary>
    /// <value>
    /// The criteria expression.
    /// </value>
    public Expression<Func<TEntity, bool>>? Criteria { get; }

    /// <summary>
    /// Gets the include expressions.
    /// </summary>
    /// <value>
    /// The include expressions.
    /// </value>
    public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();

    /// <summary>
    /// Gets the order by expression.
    /// </summary>
    /// <value>
    /// The order by expression.
    /// </value>
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    /// <summary>
    /// Gets the order by descending expression.
    /// </summary>
    /// <value>
    /// The order by descending expression.
    /// </value>
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    /// <summary>
    /// Add a new expression in <see cref="IncludeExpressions"/>.
    /// </summary>
    /// <param name="includeExpression">Include expression.</param>
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);
    }

    /// <summary>
    /// Instance a new order by expression in <see cref="OrderByExpression"/>.
    /// </summary>
    /// <param name="orderByExpression">Order by expression.</param>
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderByExpression = orderByExpression;
    }

    /// <summary>
    /// Instance a new order by descending expression in <see cref="OrderByExpression"/>.
    /// </summary>
    /// <param name="orderByDescendingExpression">Order by descending expression.</param>
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescendingExpression = orderByDescendingExpression;
    }
}
