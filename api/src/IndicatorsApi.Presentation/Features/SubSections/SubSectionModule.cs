using IndicatorsApi.Application.Features.SubSections.CreateSubSection;
using IndicatorsApi.Application.Features.SubSections.DeleteSubSection;
using IndicatorsApi.Application.Features.SubSections.GetSubSectionById;
using IndicatorsApi.Application.Features.SubSections.GetSubSectionsPagination;
using IndicatorsApi.Application.Features.SubSections.UpdateSubSection;
using IndicatorsApi.Contracts.SubSections;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.SubSections;

/// <summary>
/// SubSection endpoints.
/// </summary>
public sealed class SubSectionModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSectionModule"/> class.
    /// </summary>
    public SubSectionModule()
        : base("subsections")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateSubSection)
            .WithTags("SubSections")
            .WithName(nameof(CreateSubSection));

        app
            .MapPut("/{id}", UpdateSubSection)
            .WithTags("SubSections")
            .WithName(nameof(UpdateSubSection));

        app
            .MapDelete("/{id}", DeleteSubSection)
            .WithTags("SubSections")
            .WithName(nameof(DeleteSubSection));

        app
            .MapGet("/", GetSubSections)
            .WithTags("SubSections")
            .WithName(nameof(GetSubSections));

        app
            .MapGet("/{id}", GetSubSection)
            .WithTags("SubSections")
            .WithName(nameof(GetSubSection));
    }

    private static async Task<IResult> CreateSubSection(
        [FromBody] CreateSubSectionRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateSubSectionCommand command = request.Adapt<CreateSubSectionCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateSubSection(
        int id,
        [FromBody] UpdateSubSectionRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateSubSectionCommand query = request.Adapt<UpdateSubSectionCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteSubSection(
        string id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteSubSectionCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetSubSections(
        [AsParameters] PaginationQueryParameters parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

        GetSubSectionsPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<SubSection>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<SubSection>, Pagination<SubSectionPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetSubSection(string id, ISender sender, CancellationToken cancellationToken)
    {
        GetSubSectionByIdQuery query = new(Id: id);

        ErrorOr<SubSection> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<SubSection, SubSectionByIdResponse>(value: result);
    }
}
