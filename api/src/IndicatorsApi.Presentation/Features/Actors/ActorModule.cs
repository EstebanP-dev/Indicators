using IndicatorsApi.Application.Features.Actors;
using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Actors;

/// <summary>
/// Actor endpoints.
/// </summary>
public sealed class ActorModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorModule"/> class.
    /// </summary>
    public ActorModule()
        : base("actors")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateActor)
            .WithTags("Actors")
            .WithName(nameof(CreateActor));

        app
            .MapPut("/{id}", UpdateActor)
            .WithTags("Actors")
            .WithName(nameof(UpdateActor));

        app
            .MapDelete("/{id}", DeleteActor)
            .WithTags("Actors")
            .WithName(nameof(DeleteActor));

        app
            .MapGet("/", GetActors)
            .WithTags("Actors")
            .WithName(nameof(GetActors));

        app
            .MapGet("/{id}", GetActor)
            .WithTags("Actors")
            .WithName(nameof(GetActor));
    }

    private static async Task<IResult> CreateActor(
        [FromBody] CreateActorRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateActorCommand command = request.Adapt<CreateActorCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateActor(
        string id,
        [FromBody] UpdateActorRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateActorCommand query = request.Adapt<UpdateActorCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteActor(
        string id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteActorCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetActors(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

        GetActorsPaginationQuery query = new(Parameters: new(parameters.Page, parameters.Rows, ids));

        ErrorOr<Pagination<ActorPaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }

    private static async Task<IResult> GetActor(string id, ISender sender, CancellationToken cancellationToken)
    {
        GetActorByIdQuery query = new(Id: id);

        ErrorOr<ActorByIdResponse> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }
}
