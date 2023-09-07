namespace IndicatorsApi.Contracts.Features.Meanings.GetMeaningsPagination;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Meaning id.</param>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class MeaningPaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter