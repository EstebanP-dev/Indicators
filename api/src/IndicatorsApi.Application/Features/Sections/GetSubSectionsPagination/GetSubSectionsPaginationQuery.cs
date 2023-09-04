using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSubSectionsPagination;

/// <summary>
/// Gets sections pagination query.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
#pragma warning disable CA1819 // Properties should not return arrays
public sealed record class GetSubSectionsPaginationQuery(int Page, int Rows, string[]? Excludes)
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    : IQuery<Pagination<SubSection>>;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter