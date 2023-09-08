# Entity{T}

Es la clase base de las entidades que le asigna unos comportamientos especificos de la interfaz [IEquatable](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=net-7.0).

```csharp
/// <summary>
/// Entity base class.
/// </summary>
/// <typeparam name="TEntityId">Id type.</typeparam>
public abstract class Entity<TEntityId>
    : IEquatable<Entity<TEntityId>>
    where TEntityId : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <param name="id">Instance of entity id.</param>
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the entity id.
    /// </summary>
    /// <value>
    /// The entity id.
    /// </value>
    public TEntityId Id { get; private set; }

    /// <summary>
    /// Gets if the entities ids are equals.
    /// </summary>
    /// <param name="left">Left entity id.</param>
    /// <param name="right">Right entity id.</param>
    /// <returns>Return if the left is equals to the right entity id.</returns>
    public static bool operator ==(Entity<TEntityId>? left, Entity<TEntityId>? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    /// <summary>
    /// Gets if the entities ids are not equals.
    /// </summary>
    /// <param name="left">Left entity id.</param>
    /// <param name="right">Right entity id.</param>
    /// <returns>Return if the left is not equals to the right entity id.</returns>
    public static bool operator !=(Entity<TEntityId>? left, Entity<TEntityId>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() == GetType())
        {
            return false;
        }

        if (obj is not Entity<TEntityId> entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
```
