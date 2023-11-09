namespace IndicatorsApi.Contracts.SubSections;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Id">SubSection id.</param>
/// <param name="Name">SubSection name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSubSectionRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter