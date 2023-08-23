namespace IndicatorsApi.Domain.Exceptions;

/// <summary>
/// Invalid error value object handler exception.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class InvalidErrorValueObjectHandlerException : BaseException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidErrorValueObjectHandlerException"/> class.
    /// </summary>
    public InvalidErrorValueObjectHandlerException()
        : base("If the result is success, the error has to be 'None' and if the result is not success, the error didn't have to be 'None'. Nevertheless, it isn't complied with this rule.", null)
    {
    }
}
