namespace IndicatorsApi.Contracts.Sources;

/// <summary>
/// Gets source by id response.
/// </summary>
/// <param name="Id">Source id.</param>
/// <param name="Name">Source name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class SourceByIdResponse(int Id, string Name);
