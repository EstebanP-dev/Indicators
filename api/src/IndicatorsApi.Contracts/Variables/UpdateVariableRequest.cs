namespace IndicatorsApi.Contracts.Variables;

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">Variable id.</param>
/// <param name="Name">Variable name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateVariableRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter