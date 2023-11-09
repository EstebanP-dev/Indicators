using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Application.Features.Sources.GetSourceById;

/// <summary>
/// Get Source By Id Query.
/// </summary>
/// <param name="Id">Source id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetSourceByIdQuery(int Id)
    : IQuery<Source>;
