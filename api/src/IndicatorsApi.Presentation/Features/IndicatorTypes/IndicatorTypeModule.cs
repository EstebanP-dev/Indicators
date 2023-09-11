using IndicatorsApi.Application.Features.IndicatorTypes.CreateIndicatorType;
using IndicatorsApi.Application.Features.IndicatorTypes.DeleteIndicatorType;
using IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypeById;
using IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypesPagination;
using IndicatorsApi.Application.Features.IndicatorTypes.UpdateSection;
using IndicatorsApi.Contracts.IndicatorTypes;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.IndicatorTypes;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.IndicatorTypes;

/// <summary>
/// IndicatorType endpoints.
/// </summary>
public sealed class IndicatorTypeModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorTypeModule"/> class.
    /// </summary>
    public IndicatorTypeModule()
        : base("indicatorTypes")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateIndicatorType)
            .WithTags("IndicatorTypes")
            .WithName(nameof(CreateIndicatorType));

        app
            .MapPut("/", UpdateIndicatorType)
            .WithTags("IndicatorTypes")
            .WithName(nameof(UpdateIndicatorType));

        app
            .MapDelete("/{id}", DeleteIndicatorType)
            .WithTags("IndicatorTypes")
            .WithName(nameof(DeleteIndicatorType));

        app
            .MapGet("/", GetIndicatorTypes)
            .WithTags("IndicatorTypes")
            .WithName(nameof(GetIndicatorTypes));

        app
            .MapGet("/{id}", GetIndicatorType)
            .WithTags("IndicatorTypes")
            .WithName(nameof(GetIndicatorType));
    }

    private static async Task<IResult> CreateIndicatorType(
        [FromBody] CreateIndicatorTypeRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateIndicatorTypeCommand command = request.Adapt<CreateIndicatorTypeCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateIndicatorType(
        int id,
        [FromBody] UpdateIndicatorTypeRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateIndicatorTypeCommand query = request.Adapt<UpdateIndicatorTypeCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteIndicatorType(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteIndicatorTypeCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetIndicatorTypes(
        [AsParameters] PaginationQueryParameters parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetIndicatorTypesPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<IndicatorType>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<IndicatorType>, Pagination<IndicatorTypePaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetIndicatorType(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetIndicatorTypeByIdQuery query = new(Id: id);

        ErrorOr<IndicatorType> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<IndicatorType, IndicatorTypeByIdResponse>(value: result);
    }
}
