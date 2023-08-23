using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Application.Abstraction.Messaging;

/// <summary>
/// Query interface base on <seealso cref="IRequest{TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">Returns value type.</typeparam>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
#pragma warning restore CA1040 // Avoid empty interfaces
{
}
