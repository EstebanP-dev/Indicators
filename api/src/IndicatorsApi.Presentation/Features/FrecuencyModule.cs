using IndicatorsApi.Application.Features.Frequencies;
using IndicatorsApi.Contracts.Frequencies;

namespace IndicatorsApi.Presentation.Features;

/// <inheritdoc/>
public sealed class FrequencyModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrequencyModule"/> class.
    /// </summary>
    public FrequencyModule()
        : base("frequencies")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateFrequency)
            .WithName(nameof(CreateFrequency))
            .WithTags("Frequencies");

        app
            .MapPut("/{id}", UpdateFrequency)
            .WithName(nameof(UpdateFrequency))
            .WithTags("Frequencies");

        app
            .MapDelete("/{id}", DeleteFrequency)
            .WithName(nameof(DeleteFrequency))
            .WithTags("Frequencies");

        app
            .MapGet("/", GetPaginationFrequency)
            .WithName(nameof(GetPaginationFrequency))
            .WithTags("Frequencies");

        app
            .MapGet("/{id}", GetByIdFrequency)
            .WithName(nameof(GetByIdFrequency))
            .WithTags("Frequencies");
    }

    private static async Task<IResult> CreateFrequency(
        [FromBody] CreateFrequencyRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateFrequencyCommand command = request.Adapt<CreateFrequencyCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateFrequency(
        int id,
        [FromBody] UpdateFrequencyRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateFrequencyCommand command = request.Adapt<UpdateFrequencyCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(result);
    }

    private static async Task<IResult> DeleteFrequency(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteFrequencyCommand command = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(result);
    }

    private static async Task<IResult> GetByIdFrequency(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetFrequencyByIdQuery command = new(id);

        ErrorOr<FrequencyByIdResponse> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetPaginationFrequency(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetFrequencyPaginationQuery query = new(
            new PaginationParameters<int>(
                Page: parameters.Page,
                Rows: parameters.Rows,
                Excludes: ids));

        ErrorOr<Pagination<FrequencyPaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }
}
