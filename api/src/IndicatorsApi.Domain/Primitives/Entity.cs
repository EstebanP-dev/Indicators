using IndicatorsApi.Domain.Errors;

namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Entity base class.
/// </summary>
/// <typeparam name="TEntityId">Id type.</typeparam>
public abstract class Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <param name="id">Entity id.</param>
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets or sets and sets the entity id.
    /// </summary>
    /// <value>
    /// The entity id.
    /// </value>
    required public TEntityId Id { get; set; }

    /// <summary>
    /// Tries to fetch entities by their IDs and add them to the provided <see cref="Entity{TEntityId}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to fetch and add to the <see cref="Entity{TEntityId}"/>.</typeparam>
    /// /// <typeparam name="TEntityKey">The type of the <typeparamref name="TEntity"/>'s key.</typeparam>
    /// <param name="repository"><typeparamref name="TEntity"/> repository instance.</param>
    /// <param name="entityIds">The IDs of the entities to fetch.</param>
    /// <param name="addEntityAction">An action that defines how to add an entity of type <typeparamref name="TEntity"/> to the <see cref="Entity{TEntityId}"/>.</param>
    /// <param name="removeEntityAction">An action that defines how to remove an entity of type <typeparamref name="TEntity"/> to the <see cref="Entity{TEntityId}"/>.</param>
    /// <param name="clearEntityAction">An action that defines how to clear a list of types, <typeparamref name="TEntity"/>, to the <see cref="Entity{TEntityId}"/>.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns true if all entities were successfully fetched and added to the <see cref="Entity{TEntityId}"/>.
    /// Returns false if any of the entities were not found.
    /// </returns>
    /// <remarks>
    /// This method performs two main functions:
    /// 1. Fetches entities of type <typeparamref name="TEntity"/> in bulk based on the provided IDs.
    /// 2. Checks if the count of fetched entities matches the count of provided IDs to ensure all entities were found.
    /// 3. If all entities are found, it adds them to the provided <see cref="Entity{TEntityId}"/> using the provided action.
    /// </remarks>
    public async Task<ErrorOr<Success>> TryAddEntities<TEntity, TEntityKey>(
        IRepository<TEntity, TEntityKey>? repository,
        ICollection<TEntityKey>? entityIds,
        Action<TEntity>? addEntityAction,
        Action<TEntity>? removeEntityAction,
        Action? clearEntityAction,
        CancellationToken cancellationToken)
        where TEntity : Entity<TEntityKey>
    {
        if (repository is null)
        {
            return DomainErrors.NotNullOrEmptyProperty(nameof(repository));
        }

        if (entityIds is null)
        {
            return DomainErrors.NotNullOrEmptyProperty(nameof(entityIds));
        }

        if (addEntityAction is null)
        {
            return DomainErrors.NotNullOrEmptyProperty(nameof(addEntityAction));
        }

        if (removeEntityAction is null)
        {
            return DomainErrors.NotNullOrEmptyProperty(nameof(removeEntityAction));
        }

        if (clearEntityAction is null)
        {
            return DomainErrors.NotNullOrEmptyProperty(nameof(clearEntityAction));
        }

        if (entityIds.Count == 0)
        {
            clearEntityAction.Invoke();
        }

        var childrenEntities = await repository
            .GetBulkIdsAsync(entityIds.ToArray(), cancellationToken)
            .ConfigureAwait(false);

        var arrayChildrenEntities = childrenEntities.ToArray();

#pragma warning disable S6605
        if (arrayChildrenEntities.Any(x => !entityIds.Contains(x.Id)))
#pragma warning restore S6605
        {
            return DomainErrors.BulkNotFound;
        }

        foreach (var childEntity in arrayChildrenEntities)
        {
            addEntityAction(childEntity);
        }

        return Result.Success;
    }
}
