using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.Application.Features.Meanings.GetMeaningById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Meaning id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetMeaningByIdQuery(int Id)
    : IQuery<Meaning>;
