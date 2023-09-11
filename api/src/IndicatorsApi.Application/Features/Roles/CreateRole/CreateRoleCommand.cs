namespace IndicatorsApi.Application.Features.Roles.CreateRole;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Role name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateRoleCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter