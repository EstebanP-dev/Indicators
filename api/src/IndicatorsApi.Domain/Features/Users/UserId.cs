using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User id type.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UserId(string Value);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
