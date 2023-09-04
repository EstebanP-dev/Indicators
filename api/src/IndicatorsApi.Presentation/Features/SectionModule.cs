using IndicatorsApi.Application.Features.Sections.GetSectionById;
using IndicatorsApi.Application.Features.Sections.GetSectionsPagination;
using IndicatorsApi.Application.Features.Sections.GetSubSectionById;
using IndicatorsApi.Application.Features.Sections.GetSubSectionsPagination;
using IndicatorsApi.Contracts.Features.Sections.GetSectionById;
using IndicatorsApi.Contracts.Features.Sections.GetSectionsPagination;
using IndicatorsApi.Contracts.Features.Sections.GetSubSectionById;
using IndicatorsApi.Contracts.Features.Sections.GetSubSectionsPagination;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <inheritdoc/>
public sealed class SectionModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionModule"/> class.
    /// </summary>
    public SectionModule()
        : base("sections")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/section", async ([FromQuery] int page, [FromQuery] int rows, [FromQuery] string? exclude, ISender sender, CancellationToken cancellationToken) =>
        {
            string[] ids = GetStringsFromExcludeParameter(exclude: exclude);

            GetSectionsPaginationQuery query = new(Page: page, Rows: rows, Excludes: ids);

            ErrorOr<Pagination<Section>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<Pagination<Section>, Pagination<SectionPaginationResponse>>(result);
        });

        app.MapGet("/subsection", async ([FromQuery] int page, [FromQuery] int rows, [FromQuery] string? exclude, ISender sender, CancellationToken cancellationToken) =>
        {
            string[] ids = GetStringsFromExcludeParameter(exclude: exclude);

            GetSubSectionsPaginationQuery query = new(Page: page, Rows: rows, Excludes: ids);

            ErrorOr<Pagination<SubSection>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<Pagination<SubSection>, Pagination<SubSectionPaginationResponse>>(result);
        });

        app.MapGet("/section/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetSectionByIdQuery query = new(Id: id);

            ErrorOr<Section> result = await sender
                .Send(
                    request: query,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<Section, SectionByIdResponse>(result);
        });

        app.MapGet("/subsection/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetSubSectionByIdQuery query = new(Id: id);

            ErrorOr<SubSection> result = await sender
                .Send(
                    request: query,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<SubSection, SubSectionByIdResponse>(result);
        });
    }
}
