namespace IndicatorsApi.Application.Features.Sections.UpdateSection;

/// <summary>
/// Update command
/// </summary>
/// <param name="Id">Section id.</param>
/// <param name="Name">Section name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSectionCommand(string Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
