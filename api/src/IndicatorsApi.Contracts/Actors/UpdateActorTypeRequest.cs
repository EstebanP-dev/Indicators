namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">ActorType id.</param>
/// <param name="Name">ActorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateActorTypeRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter