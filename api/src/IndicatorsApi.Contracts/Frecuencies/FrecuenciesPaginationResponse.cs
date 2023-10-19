namespace IndicatorsApi.Contracts.Frecuencies;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Frecuency id.</param>
/// <param name="Description">Frecuency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class FrecuencyPaginationResponse(int Id, string Description);
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter