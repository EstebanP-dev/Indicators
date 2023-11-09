namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">Actor id.</param>
/// <param name="Name">Actor name.</param>
/// <param name="ActorType">Actor's type.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class ActorByIdResponse(string Id, string Name, ActorTypePaginationResponse ActorType);
