namespace IndicatorsApi.Contracts.Actors;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">ActorType id.</param>
/// <param name="Name">ActorType name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class ActorTypeByIdResponse(int Id, string Name);
