using IndicatorsApi.Application.Features.Variables;
using IndicatorsApi.Contracts.Variables;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Variable endpoints.
/// </summary>
public sealed class VariableModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableModule"/> class.
    /// </summary>
    public VariableModule()
        : base("variables")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateVariable)
            .WithTags("Variables")
            .WithName(nameof(CreateVariable));

        app
            .MapPut("/{id}", UpdateVariable)
            .WithTags("Variables")
            .WithName(nameof(UpdateVariable));

        app
            .MapDelete("/{id}", DeleteVariable)
            .WithTags("Variables")
            .WithName(nameof(DeleteVariable));

        app
            .MapGet("/", GetVariables)
            .WithTags("Variables")
            .WithName(nameof(GetVariables));

        app
            .MapGet("/{id}", GetVariable)
            .WithTags("Variables")
            .WithName(nameof(GetVariable));
    }

    private static async Task<IResult> CreateVariable(
        [FromBody] CreateVariableRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateVariableCommand command = request.Adapt<CreateVariableCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return NoContentResult(result);
    }

    private static async Task<IResult> UpdateVariable(
        int id,
        [FromBody] UpdateVariableRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateVariableCommand query = request.Adapt<UpdateVariableCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return NoContentResult(result);
    }

    private static async Task<IResult> DeleteVariable(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteVariableCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return NoContentResult(result);
    }

    private static async Task<IResult> GetVariables(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetVariablesPaginationQuery query = new(new PaginationParameters<int>(parameters.Page, parameters.Rows, ids));

        ErrorOr<Pagination<VariablePaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }

    private static async Task<IResult> GetVariable(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetVariableByIdQuery query = new(Id: id);

        ErrorOr<VariableByIdResponse> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }
}
