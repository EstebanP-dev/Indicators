namespace IndicatorsApi.Contracts.Variables;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">Variable id.</param>
/// <param name="Name">Variable name.</param>
/// <param name="CreatedDate">Variable created date.</param>
/// <param name="CreatedBy">Variable created by.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class VariableByIdResponse(int Id, string Name, DateTime CreatedDate, string CreatedBy);
