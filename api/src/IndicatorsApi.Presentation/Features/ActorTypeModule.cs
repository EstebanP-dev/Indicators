using IndicatorsApi.Application.Features.ActorTypes.CreateActorType;
using IndicatorsApi.Application.Features.ActorTypes.DeleteActorType;
using IndicatorsApi.Application.Features.ActorTypes.GetActorTypeById;
using IndicatorsApi.Application.Features.ActorTypes.GetActorTypesPagination;
using IndicatorsApi.Application.Features.ActorTypes.UpdateSection;
using IndicatorsApi.Contracts.ActorTypes;
using IndicatorsApi.Contracts.Features.ActorTypes.GetActorTypeById;
using IndicatorsApi.Contracts.Features.ActorTypes.GetActorTypesPagination;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.ActorTypes;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// ActorType endpoints.
/// </summary>
public sealed class ActorTypeModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorTypeModule"/> class.
    /// </summary>
    public ActorTypeModule()
        : base("actortypes")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateActorTypeRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateActorTypeCommand command = request.Adapt<CreateActorTypeCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: command, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

            GetActorTypesPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<ActorType>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<ActorType>, Pagination<ActorTypePaginationResponse>>(value: result);
        });

        app.MapGet("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetActorTypeByIdQuery query = new(Id: id);

            ErrorOr<ActorType> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<ActorType, ActorTypeByIdResponse>(value: result);
        });

        app.MapPut("/{id}", async (int id, [FromBody] UpdateActorTypeRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (id != request.Id)
            {
                return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
            }

            UpdateActorTypeCommand query = request.Adapt<UpdateActorTypeCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapDelete("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            DeleteActorTypeCommand query = new(id);

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });
    }
}
