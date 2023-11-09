using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Application.Features.SubSections.GetSubSectionById;

/// <summary>
/// Get section by id query.
/// </summary>
/// <param name="Id">Section id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetSubSectionByIdQuery(string Id)
    : IQuery<SubSection>;
