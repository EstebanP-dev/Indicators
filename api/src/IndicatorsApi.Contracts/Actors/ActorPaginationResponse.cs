namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Actor id.</param>
/// <param name="Name">Actor name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class ActorPaginationResponse(string Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter