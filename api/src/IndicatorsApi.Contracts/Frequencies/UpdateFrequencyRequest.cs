namespace IndicatorsApi.Contracts.Frequencies;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Id">Frequency id.</param>
/// <param name="Description">Frequency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateFrequencyRequest(int Id, string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter