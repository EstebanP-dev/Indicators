# IQuery

Es una interfaz que implementa las clases base heredando de la interfaz [IRequest](https://github.com/jbogard/MediatR), solo para consultas.

```csharp
/// <summary>
/// Query interface base on <seealso cref="IRequest{TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">Returns value type.</typeparam>
public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
```
