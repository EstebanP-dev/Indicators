using IndicatorsApi.Application.Features.Meanings.CreateMeaning;
using IndicatorsApi.Application.Features.Meanings.DeleteMeaning;
using IndicatorsApi.Application.Features.Meanings.GetMeaningById;
using IndicatorsApi.Application.Features.Meanings.GetMeaningsPagination;
using IndicatorsApi.Application.Features.Meanings.UpdateSection;
using IndicatorsApi.Contracts.Features.Meanings.GetMeaningById;
using IndicatorsApi.Contracts.Features.Meanings.GetMeaningsPagination;
using IndicatorsApi.Contracts.Meanings;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

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
        app.MapPost("/", async ([FromBody] CreateMeaningRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateMeaningCommand command = request.Adapt<CreateMeaningCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: command, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

            GetMeaningsPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<Meaning>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<Meaning>, Pagination<MeaningPaginationResponse>>(value: result);
        });

        app.MapGet("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetMeaningByIdQuery query = new(Id: id);

            ErrorOr<Meaning> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<Meaning, MeaningByIdResponse>(value: result);
        });

        app.MapPut("/{id}", async (int id, [FromBody] UpdateMeaningRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (id != request.Id)
            {
                return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
            }

            UpdateMeaningCommand query = request.Adapt<UpdateMeaningCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapDelete("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            DeleteMeaningCommand query = new(id);

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });
    }
}
