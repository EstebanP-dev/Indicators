using IndicatorsApi.Application.Features.Sources.CreateSource;
using IndicatorsApi.Application.Features.Sources.DeleteSource;
using IndicatorsApi.Application.Features.Sources.GetSourceById;
using IndicatorsApi.Application.Features.Sources.GetSourcesPagination;
using IndicatorsApi.Application.Features.Sources.UpdateSection;
using IndicatorsApi.Contracts.Sources;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Sources;

/// <summary>
/// Source endpoints.
/// </summary>
public sealed class SourceModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SourceModule"/> class.
    /// </summary>
    public SourceModule()
        : base("sources")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateSource)
            .WithTags("Sources")
            .WithName(nameof(CreateSource));

        app
            .MapPut("/", UpdateSource)
            .WithTags("Sources")
            .WithName(nameof(UpdateSource));

        app
            .MapDelete("/{id}", DeleteSource)
            .WithTags("Sources")
            .WithName(nameof(DeleteSource));

        app
            .MapGet("/", GetSources)
            .WithTags("Sources")
            .WithName(nameof(GetSources));

        app
            .MapGet("/{id}", GetSource)
            .WithTags("Sources")
            .WithName(nameof(GetSource));
    }

    private static async Task<IResult> CreateSource(
        [FromBody] CreateSourceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateSourceCommand command = request.Adapt<CreateSourceCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateSource(
        int id,
        [FromBody] UpdateSourceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateSourceCommand query = request.Adapt<UpdateSourceCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteSource(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteSourceCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetSources(
        [AsParameters] PaginationQueryParameters parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetSourcesPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<Source>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<Source>, Pagination<SourcePaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetSource(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetSourceByIdQuery query = new(Id: id);

        ErrorOr<Source> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<Source, SourceByIdResponse>(value: result);
    }
}
