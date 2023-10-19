namespace IndicatorsApi.Contracts.Frecuencies;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Description">Frecuency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateFrecuencyRequest(string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter