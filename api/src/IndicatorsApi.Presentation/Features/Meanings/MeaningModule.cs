using IndicatorsApi.Application.Features.Meanings.CreateMeaning;
using IndicatorsApi.Application.Features.Meanings.DeleteMeaning;
using IndicatorsApi.Application.Features.Meanings.GetMeaningById;
using IndicatorsApi.Application.Features.Meanings.GetMeaningsPagination;
using IndicatorsApi.Application.Features.Meanings.UpdateSection;
using IndicatorsApi.Contracts.Meanings;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Meanings;

/// <summary>
/// Meaning endpoints.
/// </summary>
public sealed class MeaningModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeaningModule"/> class.
    /// </summary>
    public MeaningModule()
        : base("meanings")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateMeaning)
            .WithTags("Meanings")
            .WithName(nameof(CreateMeaning));

        app
            .MapPut("/{id}", UpdateMeaning)
            .WithTags("Meanings")
            .WithName(nameof(UpdateMeaning));

        app
            .MapDelete("/{id}", DeleteMeaning)
            .WithTags("Meanings")
            .WithName(nameof(DeleteMeaning));

        app
            .MapGet("/", GetMeanings)
            .WithTags("Meanings")
            .WithName(nameof(GetMeanings));

        app
            .MapGet("/{id}", GetMeaning)
            .WithTags("Meanings")
            .WithName(nameof(GetMeaning));
    }

    private static async Task<IResult> CreateMeaning(
        [FromBody] CreateMeaningRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateMeaningCommand command = request.Adapt<CreateMeaningCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateMeaning(
        int id,
        [FromBody] UpdateMeaningRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateMeaningCommand query = request.Adapt<UpdateMeaningCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteMeaning(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteMeaningCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetMeanings(
        [AsParameters] PaginationQueryParameters parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetMeaningsPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<Meaning>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<Meaning>, Pagination<MeaningPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetMeaning(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetMeaningByIdQuery query = new(Id: id);

        ErrorOr<Meaning> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<Meaning, MeaningByIdResponse>(value: result);
    }
}
