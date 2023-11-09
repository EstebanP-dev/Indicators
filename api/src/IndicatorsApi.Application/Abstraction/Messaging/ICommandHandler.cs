namespace IndicatorsApi.Application.Abstraction.Messaging;

/// <summary>
/// Interface of <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">Command type.</typeparam>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}