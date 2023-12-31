﻿using IndicatorsApi.Application.Features.Actors;
using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Actors;

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
        : base("actorTypes")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateActorType)
            .WithTags("ActorTypes")
            .WithName(nameof(CreateActorType));

        app
            .MapPut("/{id}", UpdateActorType)
            .WithTags("ActorTypes")
            .WithName(nameof(UpdateActorType));

        app
            .MapDelete("/{id}", DeleteActorType)
            .WithTags("ActorTypes")
            .WithName(nameof(DeleteActorType));

        app
            .MapGet("/", GetActorTypes)
            .WithTags("ActorTypes")
            .WithName(nameof(GetActorTypes));

        app
            .MapGet("/{id}", GetActorType)
            .WithTags("ActorTypes")
            .WithName(nameof(GetActorType));
    }

    private static async Task<IResult> CreateActorType(
        [FromBody] CreateActorTypeRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateActorTypeCommand command = request.Adapt<CreateActorTypeCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateActorType(
        int id,
        [FromBody] UpdateActorTypeRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateActorTypeCommand query = request.Adapt<UpdateActorTypeCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteActorType(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteActorTypeCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetActorTypes(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetActorTypesPaginationQuery query = new(new(parameters.Page, parameters.Rows, ids));

        ErrorOr<Pagination<ActorTypePaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }

    private static async Task<IResult> GetActorType(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetActorTypeByIdQuery query = new(Id: id);

        ErrorOr<ActorTypeByIdResponse> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }
}
