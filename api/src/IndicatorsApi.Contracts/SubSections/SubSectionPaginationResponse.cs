namespace IndicatorsApi.Contracts.SubSections;

/// <summary>
/// Gets the section pagination response.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class SubSectionPaginationResponse(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
