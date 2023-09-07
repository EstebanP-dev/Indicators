namespace IndicatorsApi.Contracts.Displays;

/// <summary>
/// Create display request.
/// </summary>
/// <param name="Id">Display id.</param>
/// <param name="Name">Display name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateDisplayRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter