namespace IndicatorsApi.Application.Features.SubSections.UpdateSubSection;

/// <summary>
/// Update command
/// </summary>
/// <param name="Id">SubSection id.</param>
/// <param name="Name">SubSection name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSubSectionCommand(string Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
