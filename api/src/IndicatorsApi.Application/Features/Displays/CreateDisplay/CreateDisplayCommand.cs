namespace IndicatorsApi.Application.Features.Displays.CreateDisplay;

/// <summary>
/// Create display command.
/// </summary>
/// <param name="Name">Display name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateDisplayCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter