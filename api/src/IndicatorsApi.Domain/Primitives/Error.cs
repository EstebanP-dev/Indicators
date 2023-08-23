using IndicatorsApi.Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// A value object that represents an error.
/// </summary>
public sealed class Error : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">Error's code.</param>
    /// <param name="message">Error's message.</param>
    /// <param name="exception">Error's exception.</param>
    public Error(string code, string message, BaseException? exception = null)
    {
        Code = code;
        Message = message;
        Exception = exception;
    }

    /// <summary>
    /// Gets code of the error.
    /// </summary>
    /// <value>
    /// Code of the error.
    /// </value>
    public string Code { get; private set; }

    /// <summary>
    /// Gets message of the error.
    /// </summary>
    /// <value>
    /// Message of the error.
    /// </value>
    public string Message { get; private set; }

    /// <summary>
    /// Gets exception of the error.
    /// </summary>
    /// <value>
    /// Exception of the error.
    /// </value>
    public BaseException? Exception { get; private set; }

    /// <summary>
    /// Returns error's message.
    /// </summary>
    /// <param name="error"><see cref="Error"/> instance.</param>
    public static implicit operator string(Error error)
        => error?.Message ?? string.Empty;

    /// <summary>
    /// Converts the value of this instance to its equivalent string representation.
    /// </summary>
    /// <returns>An <see cref="Error"/>'s message.</returns>
    public override string ToString() => this;

    /// <summary>
    /// Return the atomic values of the <see cref="Error"/> instance.
    /// </summary>
    /// <returns>Returns an <see cref="IEnumerable{T}"/> of <see cref="Error"/> values.</returns>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }
}
