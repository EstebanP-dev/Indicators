using IndicatorsApi.Application.Features.Indicators.Create;
using IndicatorsApi.Application.Features.Indicators.Delete;
using IndicatorsApi.Application.Features.Indicators.GetById;
using IndicatorsApi.Application.Features.Indicators.GetPagination;
using IndicatorsApi.Application.Features.Indicators.Update;
using IndicatorsApi.Contracts.Indicators;

namespace IndicatorsApi.Presentation.Features.Indicators;

/// <summary>
/// Indicator endpoints.
/// </summary>
public sealed class IndicatorModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorModule"/> class.
    /// </summary>
    public IndicatorModule()
        : base("indicators")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateIndicator)
            .WithTags("Indicators")
            .WithName(nameof(CreateIndicator));

        app
            .MapPut("/{id}", UpdateIndicator)
            .WithTags("Indicators")
            .WithName(nameof(UpdateIndicator));

        app
            .MapDelete("/{id}", DeleteIndicator)
            .WithTags("Indicators")
            .WithName(nameof(DeleteIndicator));

        app
            .MapGet("/{id}", GetIndicatorById)
            .WithTags("Indicators")
            .WithName(nameof(GetIndicatorById));

        app
            .MapGet("/", GetIndicators)
            .WithTags("Indicators")
            .WithName(nameof(GetIndicators));
    }

    private static async Task<IResult> CreateIndicator(
        [FromBody] CreateIndicatorRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateIndicatorCommand command = request.Adapt<CreateIndicatorCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateIndicator(
        int id,
        [FromBody] UpdateIndicatorRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateIndicatorCommand command = request.Adapt<UpdateIndicatorCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteIndicator(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteIndicatorCommand command = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetIndicatorById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetIndicatorByIdQuery command = new(id);

        ErrorOr<IndicatorByIdResponse> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetIndicators(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetIndicatorsPaginationQuery query = new(new PaginationParameters<int>(parameters.Page, parameters.Rows, ids));

        ErrorOr<Pagination<IndicatorPaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }
}
