namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Id">Actor id.</param>
/// <param name="Name">Actor name.</param>
/// <param name="ActorType">Actor's type.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateActorRequest(string Id, string Name, int ActorType);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter