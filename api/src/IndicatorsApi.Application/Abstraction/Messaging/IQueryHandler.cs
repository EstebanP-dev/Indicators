using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Application.Abstraction.Messaging;

/// <summary>
/// Interface of <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TQuery">Query type.</typeparam>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
{
}
