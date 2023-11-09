using IndicatorsApi.Application.Features.Sections.CreateSection;
using IndicatorsApi.Application.Features.Sections.DeleteSection;
using IndicatorsApi.Application.Features.Sections.GetSectionById;
using IndicatorsApi.Application.Features.Sections.GetSectionsPagination;
using IndicatorsApi.Application.Features.Sections.UpdateSection;
using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Sections;

/// <summary>
/// Section endpoints.
/// </summary>
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
        app
            .MapPost("/", CreateSection)
            .WithTags("Sections")
            .WithName(nameof(CreateSection));

        app
            .MapPut("/{id}", UpdateSection)
            .WithTags("Sections")
            .WithName(nameof(UpdateSection));

        app
            .MapDelete("/{id}", DeleteSection)
            .WithTags("Sections")
            .WithName(nameof(DeleteSection));

        app
            .MapGet("/", GetSections)
            .WithTags("Sections")
            .WithName(nameof(GetSections));

        app
            .MapGet("/{id}", GetSection)
            .WithTags("Sections")
            .WithName(nameof(GetSection));
    }

    private static async Task<IResult> CreateSection(
        [FromBody] CreateSectionRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateSectionCommand command = request.Adapt<CreateSectionCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateSection(
        string id,
        [FromBody] UpdateSectionRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateSectionCommand query = request.Adapt<UpdateSectionCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteSection(
        string id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteSectionCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetSections(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

        GetSectionsPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<Section>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<Section>, Pagination<SectionPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetSection(string id, ISender sender, CancellationToken cancellationToken)
    {
        GetSectionByIdQuery query = new(Id: id);

        ErrorOr<Section> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<Section, SectionByIdResponse>(value: result);
    }
}
