namespace IndicatorsApi.Application.Features.SubSections.DeleteSubSection;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">SubSection id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteSubSectionCommand(string Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
