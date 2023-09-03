namespace IndicatorsApi.Domain.Exceptions;

/// <summary>
/// Exception thrown when one or more entities cannot be found.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public sealed class OneOrMoreEntitiesCannotBeFoundByIdException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneOrMoreEntitiesCannotBeFoundByIdException"/> class.
    /// </summary>
    /// <param name="ids">List of ids.</param>
    public OneOrMoreEntitiesCannotBeFoundByIdException(string ids)
        : base($"One or more entities cannot be found. Ids => '{ids}'")
    {
    }
}
