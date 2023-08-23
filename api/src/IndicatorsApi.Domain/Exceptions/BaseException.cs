namespace IndicatorsApi.Domain.Exceptions;

/// <summary>
/// Base exception.
/// </summary>
[Serializable]
public class BaseException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseException"/> class.
    /// </summary>
    public BaseException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public BaseException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseException"/> class.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inheritance stack track exception.</param>
    public BaseException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseException"/> class.
    /// </summary>
    /// <param name="info"><see cref="SerializationInfo"/> data.</param>
    /// <param name="context"><see cref="StreamingContext"/> data.</param>
    protected BaseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
