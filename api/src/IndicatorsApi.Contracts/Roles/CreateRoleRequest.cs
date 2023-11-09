namespace IndicatorsApi.Contracts.Roles;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">Role name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateRoleRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter