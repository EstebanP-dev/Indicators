namespace IndicatorsApi.Contracts.Meanings;

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">Meaning id.</param>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateMeaningRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter