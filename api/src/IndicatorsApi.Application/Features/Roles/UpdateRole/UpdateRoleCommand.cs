namespace IndicatorsApi.Application.Features.Roles.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateRoleCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
