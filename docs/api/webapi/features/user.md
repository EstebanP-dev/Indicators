# User Mapping Configuration

Esta clase hereda de la interfaz **IRegister**  de la libreria [Mapster](https://www.bing.com/search?pglt=41&q=mapster&cvid=10604c91f9364411b1f7261ed978fc4a&aqs=edge..69i57j69i59l4j69i60l3.728j0j1&FORM=ANNTA1&PC=U531). Esta clase realiza la comfiguración para el mapeo de las entidades.

```csharp
/// <inheritdoc/>
internal sealed class UserMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserByIdResponse>()
            .Map(dest => dest.Email, src => src.Id.Value)
            .Map(dest => dest.Roles, src => src.Roles)
            .ConstructUsing(src => new UserByIdResponse(src.Id.Value, src.Roles.Adapt<IEnumerable<RolePaginationResponse>>()));

        config.NewConfig<User, UserPaginationResponse>()
            .Map(dest => dest.Email, src => src.Id.Value);
    }
}
```
