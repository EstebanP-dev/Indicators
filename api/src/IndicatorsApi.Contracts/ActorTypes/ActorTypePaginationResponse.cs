namespace IndicatorsApi.Contracts.ActorTypes;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">ActorType id.</param>
/// <param name="Name">ActorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class ActorTypePaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter