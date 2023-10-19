using IndicatorsApi.Application.Features.Frecuencies;
using IndicatorsApi.Contracts.Frecuencies;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <inheritdoc/>
public sealed class FrecuencyModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrecuencyModule"/> class.
    /// </summary>
    public FrecuencyModule()
        : base("frecuency")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", Create)
            .WithName(nameof(Create))
            .WithTags("Frecuencies");

        app
            .MapGet("/", GetPagination)
            .WithName(nameof(GetPagination))
            .WithTags("Frecuencies");

        app
            .MapPost("/{id}", GetById)
            .WithName(nameof(GetById))
            .WithTags("Frecuencies");
    }

    private static async Task<IResult> Create(
        [FromBody] CreateFrecuencyRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateFrecuencyCommand command = request.Adapt<CreateFrecuencyCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> Update(
        int id,
        [FromBody] UpdateFrecuencyRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        UpdateFrecuencyCommand command = new()
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetFrecuencyByIdQuery command = new(id);

        ErrorOr<FrecuencyByIdResponse> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetPagination(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetFrecuencyPaginationQuery query = new(
            new PaginationParameters<int>(
                Page: parameters.Page,
                Rows: parameters.Rows,
                Excludes: ids));

        ErrorOr<Pagination<FrecuencyPaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }
}
