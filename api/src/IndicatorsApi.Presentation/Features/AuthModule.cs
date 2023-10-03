using IndicatorsApi.Application.Features.Auth;
using IndicatorsApi.Contracts.Auth;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Auth endpoints.
/// </summary>
public sealed class AuthModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthModule"/> class.
    /// </summary>
    public AuthModule()
        : base("auth")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", Login)
            .WithName(nameof(Login))
            .WithTags("Authentication")
            .AllowAnonymous();
    }

    private static async Task<IResult> Login([FromBody] LoginRequest request, ISender sender)
    {
        LoginCommand command = request.Adapt<LoginCommand>();

        ErrorOr<LoginResponse> result = await sender
            .Send(command)
            .ConfigureAwait(true);

        return Result(value: result);
    }
}
