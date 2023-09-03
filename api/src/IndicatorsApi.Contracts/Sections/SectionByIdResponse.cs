namespace IndicatorsApi.Contracts.Features.Sections.GetSectionById;

/// <summary>
/// Section response.
/// </summary>
/// <param name="Id">Section Id.</param>
/// <param name="Name">Section name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class SectionByIdResponse(int Id, string Name);
