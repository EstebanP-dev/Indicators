# IQueryHandler

Es una interfaz que implementa las clases base heredando de la interfaz [IRequestHandler](https://github.com/jbogard/MediatR), solo para consultas.

```csharp
/// <summary>
/// Interface of <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </summary>
/// <typeparam name="TQuery">Query type.</typeparam>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
        where TQuery : IQuery<TResponse>
{
}
```
