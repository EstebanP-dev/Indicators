namespace IndicatorsApi.Application.Features.SubSections.CreateSubSection;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">SubSection name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateSubSectionCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter