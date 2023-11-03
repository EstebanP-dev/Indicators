using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Specifications;

/// <summary>
/// Implement extended methods to apply the specification.
/// </summary>
public static class SpecificationEvaluator
{
    /// <summary>
    /// Implement the specification query.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TEntityId">Entity id type.</typeparam>
    /// <param name="inputQueryable">Current query.</param>
    /// <param name="specification">specification instance.</param>
    /// <returns>The query with specification implemented.</returns>
    public static IQueryable<TEntity> GetQuery<TEntity, TEntityId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TEntityId> specification)
        where TEntity : Entity<TEntityId>
    {
        ArgumentNullException.ThrowIfNull(inputQueryable);
        ArgumentNullException.ThrowIfNull(specification);

        IQueryable<TEntity> queryable = inputQueryable;

        if (specification.Criteria is not null)
        {
            queryable = queryable
                .Where(specification.Criteria);
        }

        _ = specification
            .IncludeExpressions
            .Aggregate(
                queryable,
                (current, includeExpression) =>
                    current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            queryable = queryable.OrderBy(
                specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(
                specification.OrderByDescendingExpression);
        }

        if (specification.IsSplitQuery)
        {
            queryable = queryable.AsSplitQuery();
        }

        return queryable;
    }
}
