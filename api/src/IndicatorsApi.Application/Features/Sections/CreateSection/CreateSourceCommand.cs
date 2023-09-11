namespace IndicatorsApi.Application.Features.Sections.CreateSection;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Section name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateSectionCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter