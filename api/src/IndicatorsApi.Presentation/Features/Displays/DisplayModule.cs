using IndicatorsApi.Application.Abstraction.Enums;
using IndicatorsApi.Application.Features.Displays.CreateDisplay;
using IndicatorsApi.Application.Features.Displays.DeleteDisplay;
using IndicatorsApi.Application.Features.Displays.GetDisplayById;
using IndicatorsApi.Application.Features.Displays.GetDisplaysPagination;
using IndicatorsApi.Application.Features.Displays.UpdateSection;
using IndicatorsApi.Contracts.Displays;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Displays;

/// <summary>
/// Display endpoints.
/// </summary>
public sealed class DisplayModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayModule"/> class.
    /// </summary>
    public DisplayModule()
        : base("displays")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateDisplay)
            .WithTags("Displays")
            .WithName(nameof(CreateDisplay));

        app
            .MapPut("/{id}", PutDisplay)
            .WithTags("Displays")
            .WithName(nameof(PutDisplay));

        app
            .MapPatch("/{id}", PatchDisplay)
            .WithTags("Displays")
            .WithName(nameof(PatchDisplay));

        app
            .MapDelete("/{id}", DeleteDisplay)
            .WithTags("Displays")
            .WithName(nameof(DeleteDisplay));

        app
            .MapGet("/", GetDisplays)
            .WithTags("Displays")
            .WithName(nameof(GetDisplays));

        app
            .MapGet("/{id}", GetDisplay)
            .WithTags("Displays")
            .WithName(nameof(GetDisplay));
    }

    private static async Task<IResult> CreateDisplay(
        [FromBody] CreateDisplayRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateDisplayCommand command = request.Adapt<CreateDisplayCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> PutDisplay(
        int id,
        [FromBody] UpdateDisplayRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateDisplayCommand query = new(id, request.Name, UpdateOperations.PUT);

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> PatchDisplay(
        int id,
        [FromBody] UpdateDisplayRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        UpdateDisplayCommand query = new(id, request.Name, UpdateOperations.PATCH);

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteDisplay(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteDisplayCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetDisplays(
        [AsParameters] PaginationQueryParameters parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetDisplaysPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<Display>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<Display>, Pagination<DisplayPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetDisplay(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetDisplayByIdQuery query = new(Id: id);

        ErrorOr<Display> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<Display, DisplayByIdResponse>(value: result);
    }
}
