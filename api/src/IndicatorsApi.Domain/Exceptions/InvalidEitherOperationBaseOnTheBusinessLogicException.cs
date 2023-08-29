using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Exceptions;

/// <summary>
/// Exception thrown when an invalid business logic operation was used with <see cref="Either{TLeft, TRight}"/> class.
/// </summary>
#pragma warning disable CA2237 // Mark ISerializable types with serializable
#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class InvalidEitherOperationBaseOnTheBusinessLogicException
#pragma warning restore CA1032 // Implement standard exception constructors
#pragma warning restore CA2237 // Mark ISerializable types with serializable
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEitherOperationBaseOnTheBusinessLogicException"/> class.
    /// </summary>
    public InvalidEitherOperationBaseOnTheBusinessLogicException()
        : base("An invalid business logic operation was used. Please verify the previus validation.")
    {
    }
}
