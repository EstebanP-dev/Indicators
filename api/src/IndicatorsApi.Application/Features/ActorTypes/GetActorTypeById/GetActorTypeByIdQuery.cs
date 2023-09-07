using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Application.Features.ActorTypes.GetActorTypeById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">ActorType id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetActorTypeByIdQuery(int Id)
    : IQuery<ActorType>;
