namespace IndicatorsApi.Contracts.Variables;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">Variable name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateVariableRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter