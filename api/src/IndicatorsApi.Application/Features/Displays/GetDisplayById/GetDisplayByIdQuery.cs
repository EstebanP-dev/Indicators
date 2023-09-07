using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Application.Features.Displays.GetDisplayById;

/// <summary>
/// Get Display By Id Query.
/// </summary>
/// <param name="Id">Display id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetDisplayByIdQuery(int Id)
    : IQuery<Display>;
