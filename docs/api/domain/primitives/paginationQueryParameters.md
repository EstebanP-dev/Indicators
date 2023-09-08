# PaginationQueryParameters

Es una clase que contiene los parametros ncesarios para paginar los datos. Esta información es dada por el usuario en la petición al API.

```csharp

/// <summary>
/// Pagination parameters.
/// </summary>
public sealed record class PaginationQueryParameters(int Page, int Rows, string? Exclude);
```

**NOTA:**

- Las clases [record](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) crean internamente los métodos get y set de las propiedades especificadas y se instancian en el constructor. Permiten tener una clase mucho más limpia.
- Las clases [sealed](https://learn.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/sealed) no pueden ser heredadas por otros objetos.