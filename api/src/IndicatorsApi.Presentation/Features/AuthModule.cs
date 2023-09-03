using IndicatorsApi.Application.Features.Auth.Login;
using IndicatorsApi.Application.Features.Users.Login;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Auth endpoints.
/// </summary>
public class AuthModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthModule"/> class.
    /// </summary>
    public AuthModule()
        : base($"{Settings.BASEAPI}/auth")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async ([FromBody] LoginRequest request, ISender sender) =>
        {
            LoginCommand command = request.Adapt<LoginCommand>();

            ErrorOr<string> result = await sender.Send(command)
                .ConfigureAwait(true);

            return Result(result);
        });
    }
}
