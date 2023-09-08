using IndicatorsApi.Application.Features.IndicatorTypes.CreateIndicatorType;
using IndicatorsApi.Application.Features.IndicatorTypes.DeleteIndicatorType;
using IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypeById;
using IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypesPagination;
using IndicatorsApi.Application.Features.IndicatorTypes.UpdateSection;
using IndicatorsApi.Contracts.Features.IndicatorTypes.GetIndicatorTypeById;
using IndicatorsApi.Contracts.Features.IndicatorTypes.GetIndicatorTypesPagination;
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
        : base("indicatortypes")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateIndicatorTypeRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateIndicatorTypeCommand command = request.Adapt<CreateIndicatorTypeCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: command, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

            GetIndicatorTypesPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<IndicatorType>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<IndicatorType>, Pagination<IndicatorTypePaginationResponse>>(value: result);
        });

        app.MapGet("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetIndicatorTypeByIdQuery query = new(Id: id);

            ErrorOr<IndicatorType> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<IndicatorType, IndicatorTypeByIdResponse>(value: result);
        });

        app.MapPut("/{id}", async (int id, [FromBody] UpdateIndicatorTypeRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (id != request.Id)
            {
                return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
            }

            UpdateIndicatorTypeCommand query = request.Adapt<UpdateIndicatorTypeCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapDelete("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            DeleteIndicatorTypeCommand query = new(id);

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });
    }
}
