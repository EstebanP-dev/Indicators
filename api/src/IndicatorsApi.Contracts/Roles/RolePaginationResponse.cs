namespace IndicatorsApi.Contracts.Roles;

/// <summary>
/// Gets role pagination response.
/// </summary>
/// <param name="Id">Role id.</param>
/// <param name="Name">Role name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class RolePaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter