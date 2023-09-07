namespace IndicatorsApi.Contracts.Features.Meanings.GetMeaningById;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">Meaning id.</param>
/// <param name="Name">Meaning name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class MeaningByIdResponse(int Id, string Name);
