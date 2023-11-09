namespace IndicatorsApi.Contracts.Meanings;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateMeaningRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter