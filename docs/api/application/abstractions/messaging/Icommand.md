# ICommand

Es una interfaz que implementa las clases base heredando de la interfaz [IRequest](https://github.com/jbogard/MediatR), solo para comandos.

```csharp
/// <summary>
/// Interface of <see cref="IRequest"/> and <seealso cref="ICommandBase"/>.
/// </summary>
public interface ICommand : IRequest<ErrorOr<Success>>, ICommandBase
{
}

/// <summary>
/// Interface of <see cref="IRequest{TResponse}"/> and <seealso cref="ICommandBase"/>.
/// </summary>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>, ICommandBase
{
}
```