# Custom Exceptions

Las excepciones personalizadas, permiten tener una mejor trasabilidad e información con respecto a cualquier error que suceda en nuestra aplicación. Estas excepciones son usadas para ampliar la información de nuestro código y evitar los [Magic Strings](https://methodpoet.com/magic-strings/).

## Bad

```csharp
throw new ArgumentException("Please supply at least one argument.");
```

## Good

```csharp
class NeedsLeatsOneArgument : Exception
{
    public NeedsLeatsOneArgument(Exception? innerException = null)
        : base("Please supply at least one argument.", innerException)
    {}
}
```

Actualmente en el proyecto, no se han implementado los custom exceptions debido a que no ha sido necesario. En el transcurso del desarrollo se implementaran.