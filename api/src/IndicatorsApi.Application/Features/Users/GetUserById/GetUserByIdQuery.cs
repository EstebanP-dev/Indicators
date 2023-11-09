using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Users.GetUserById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">User id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetUserByIdQuery(string Id)
    : IQuery<User>;
