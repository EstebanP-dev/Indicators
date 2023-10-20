namespace IndicatorsApi.Contracts.Frequencies;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Description">Frequency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateFrequencyRequest(string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter