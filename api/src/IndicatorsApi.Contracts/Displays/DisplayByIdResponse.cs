namespace IndicatorsApi.Contracts.Displays;

/// <summary>
/// Gets display by id response.
/// </summary>
/// <param name="Id">Display id.</param>
/// <param name="Name">Display name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class DisplayByIdResponse(int Id, string Name);
