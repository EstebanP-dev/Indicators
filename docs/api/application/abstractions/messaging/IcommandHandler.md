# ICommandHandler

Es una interfaz que implementa las clases base heredando de la interfaz [IRequestHandler](https://github.com/jbogard/MediatR), solo para comandos.

```csharp
/// <summary>
/// Interface of <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">Command type.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ErrorOr<Success>>
        where TCommand : ICommand
{
}

/// <summary>
/// Interface of <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">Command type.</typeparam>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
```
