using IndicatorsApi.Application.Abstraction.Messaging;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Users.GetUserByEmail;

/// <summary>
/// Gets user by email.
/// </summary>
/// <param name="Email">User email.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetUserByEmailQuery(string Email)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<UserByEmailResponse>
{
}
