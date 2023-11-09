namespace IndicatorsApi.Contracts.Sources;

/// <summary>
/// Create source request.
/// </summary>
/// <param name="Id">Source id.</param>
/// <param name="Name">Source name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSourceRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter