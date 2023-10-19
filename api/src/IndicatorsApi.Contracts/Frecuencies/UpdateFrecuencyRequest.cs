namespace IndicatorsApi.Contracts.Frecuencies;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Id">Frecuency id.</param>
/// <param name="Description">Frecuency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateFrecuencyRequest(string Id, string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter