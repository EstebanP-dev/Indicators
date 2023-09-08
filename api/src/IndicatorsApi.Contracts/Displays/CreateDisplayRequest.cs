namespace IndicatorsApi.Contracts.Displays;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">Display name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateDisplayRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter