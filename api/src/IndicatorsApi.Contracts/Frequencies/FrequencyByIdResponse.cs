namespace IndicatorsApi.Contracts.Frequencies;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Frequency id.</param>
/// <param name="Description">Frequency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class FrequencyByIdResponse(string Id, string Description);