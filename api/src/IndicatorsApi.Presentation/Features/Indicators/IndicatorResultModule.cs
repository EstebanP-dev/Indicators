using IndicatorsApi.Application.Features.Indicators.Create;
using IndicatorsApi.Application.Features.Indicators.Delete;
using IndicatorsApi.Application.Features.Indicators.GetById;
using IndicatorsApi.Application.Features.Indicators.GetPagination;
using IndicatorsApi.Application.Features.Indicators.Update;
using IndicatorsApi.Contracts.Indicators;

namespace IndicatorsApi.Presentation.Features.Indicators;

/// <summary>
/// IndicatorResult endpoints.
/// </summary>
public sealed class IndicatorResultModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorResultModule"/> class.
    /// </summary>
    public IndicatorResultModule()
        : base("indicators/indicatorResults")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateIndicatorResult)
            .WithTags("IndicatorResults")
            .WithName(nameof(CreateIndicatorResult));

        app
            .MapPut("/{id}", UpdateIndicatorResult)
            .WithTags("IndicatorResults")
            .WithName(nameof(UpdateIndicatorResult));

        app
            .MapDelete("/{id}", DeleteIndicatorResult)
            .WithTags("IndicatorResults")
            .WithName(nameof(DeleteIndicatorResult));

        app
            .MapGet("/", GetIndicatorResults)
            .WithTags("IndicatorResults")
            .WithName(nameof(GetIndicatorResults));

        app
            .MapGet("/{id}", GetIndicatorResult)
            .WithTags("IndicatorResults")
            .WithName(nameof(GetIndicatorResult));
    }

    private static async Task<IResult> CreateIndicatorResult(
        [FromBody] CreateIndicatorResultRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateIndicatorResultCommand command = request.Adapt<CreateIndicatorResultCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateIndicatorResult(
        int id,
        [FromBody] UpdateIndicatorResultRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateIndicatorResultCommand query = request.Adapt<UpdateIndicatorResultCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteIndicatorResult(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteIndicatorResultCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetIndicatorResults(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetIndicatorResultsPaginationQuery query = new(new PaginationParameters<int>(parameters.Page, parameters.Rows, ids));

        ErrorOr<Pagination<IndicatorResultPaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }

    private static async Task<IResult> GetIndicatorResult(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetIndicatorResultByIdQuery query = new(Id: id);

        ErrorOr<IndicatorResultByIdResponse> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }
}
