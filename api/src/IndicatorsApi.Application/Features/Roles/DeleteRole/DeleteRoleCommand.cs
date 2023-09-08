namespace IndicatorsApi.Application.Features.Roles.DeleteRole;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Role id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteRoleCommand(int Id)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
