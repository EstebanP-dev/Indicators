namespace IndicatorsApi.Contracts.Features.IndicatorTypes.GetIndicatorTypeById;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class IndicatorTypeByIdResponse(int Id, string Name);
