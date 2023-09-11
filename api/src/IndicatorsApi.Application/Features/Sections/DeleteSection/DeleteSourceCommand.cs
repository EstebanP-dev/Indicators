namespace IndicatorsApi.Application.Features.Sections.DeleteSection;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Section id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteSectionCommand(string Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
