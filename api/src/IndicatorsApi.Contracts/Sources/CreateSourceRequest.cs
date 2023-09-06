namespace IndicatorsApi.Contracts.Sources;

/// <summary>
/// Create source request.
/// </summary>
/// <param name="Name">Source name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateSourceRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter