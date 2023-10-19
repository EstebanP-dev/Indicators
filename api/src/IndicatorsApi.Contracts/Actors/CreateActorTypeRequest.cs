namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">ActorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateActorTypeRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter