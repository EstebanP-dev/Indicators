using IndicatorsApi.Application.Features.MeasurementUnits.CreateMeasurementUnit;
using IndicatorsApi.Application.Features.MeasurementUnits.DeleteMeasurementUnit;
using IndicatorsApi.Application.Features.MeasurementUnits.GetMeasurementUnitById;
using IndicatorsApi.Application.Features.MeasurementUnits.GetMeasurementUnitsPagination;
using IndicatorsApi.Application.Features.MeasurementUnits.UpdateSection;
using IndicatorsApi.Contracts.MeasurementUnits;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.MeasurementUnits;

/// <summary>
/// MeasurementUnit endpoints.
/// </summary>
public sealed class MeasurementUnitModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementUnitModule"/> class.
    /// </summary>
    public MeasurementUnitModule()
        : base("measurementunits")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", CreateMeasurementUnit)
            .WithTags("MeasurementUnits")
            .WithName(nameof(CreateMeasurementUnit));

        app
            .MapPut("/{id}", UpdateMeasurementUnit)
            .WithTags("MeasurementUnits")
            .WithName(nameof(UpdateMeasurementUnit));

        app
            .MapDelete("/{id}", DeleteMeasurementUnit)
            .WithTags("MeasurementUnits")
            .WithName(nameof(DeleteMeasurementUnit));

        app
            .MapGet("/", GetMeasurementUnits)
            .WithTags("MeasurementUnits")
            .WithName(nameof(GetMeasurementUnits));

        app
            .MapGet("/{id}", GetMeasurementUnit)
            .WithTags("MeasurementUnits")
            .WithName(nameof(GetMeasurementUnit));
    }

    private static async Task<IResult> CreateMeasurementUnit(
        [FromBody] CreateMeasurementUnitRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateMeasurementUnitCommand command = request.Adapt<CreateMeasurementUnitCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateMeasurementUnit(
        int id,
        [FromBody] UpdateMeasurementUnitRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateMeasurementUnitCommand query = request.Adapt<UpdateMeasurementUnitCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteMeasurementUnit(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteMeasurementUnitCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetMeasurementUnits(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetMeasurementUnitsPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<MeasurementUnit>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<MeasurementUnit>, Pagination<MeasurementUnitPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetMeasurementUnit(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetMeasurementUnitByIdQuery query = new(Id: id);

        ErrorOr<MeasurementUnit> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<MeasurementUnit, MeasurementUnitByIdResponse>(value: result);
    }
}
