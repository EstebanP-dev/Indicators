namespace IndicatorsApi.Contracts.Variables;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Variable id.</param>
/// <param name="Name">Variable name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class VariablePaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter