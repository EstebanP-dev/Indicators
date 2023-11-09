namespace IndicatorsApi.Contracts.Displays;

/// <summary>
/// Gets display pagination response.
/// </summary>
/// <param name="Id">Display id.</param>
/// <param name="Name">Display name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DisplayPaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter