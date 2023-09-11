namespace IndicatorsApi.Contracts.Sections;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Id">Section id.</param>
/// <param name="Name">Section name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSectionRequest(string Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter