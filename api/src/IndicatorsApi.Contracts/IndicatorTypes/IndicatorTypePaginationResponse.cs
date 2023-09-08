namespace IndicatorsApi.Contracts.Features.IndicatorTypes.GetIndicatorTypesPagination;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class IndicatorTypePaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter