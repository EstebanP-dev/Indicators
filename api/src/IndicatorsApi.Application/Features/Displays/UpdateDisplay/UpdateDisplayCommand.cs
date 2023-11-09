using IndicatorsApi.Application.Abstraction.Enums;

namespace IndicatorsApi.Application.Features.Displays.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Display id.</param>
/// <param name="Name">Display name.</param>
/// <param name="Operations"><see cref="UpdateOperations"/> instance.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateDisplayCommand(int? Id, string? Name, UpdateOperations Operations)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
