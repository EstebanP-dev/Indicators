using IndicatorsApi.Domain.Features.IndicatorTypes;

namespace IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypeById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetIndicatorTypeByIdQuery(int Id)
    : IQuery<IndicatorType>;
