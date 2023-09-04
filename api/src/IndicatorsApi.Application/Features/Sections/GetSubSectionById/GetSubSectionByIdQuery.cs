using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSubSectionById;

/// <summary>
/// Get section by id query.
/// </summary>
/// <param name="Id">Section id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetSubSectionByIdQuery(string Id)
    : IQuery<SubSection>;
