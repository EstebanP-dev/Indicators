using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.Application.Features.MeasurementUnits.GetMeasurementUnitById;

/// <inheritdoc/>
internal sealed class GetMeasurementUnitByIdQueryHandler
    : IQueryHandler<GetMeasurementUnitByIdQuery, MeasurementUnit>
{
    private readonly IMeasurementUnitRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMeasurementUnitByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    public GetMeasurementUnitByIdQueryHandler(IMeasurementUnitRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<MeasurementUnit>> Handle(GetMeasurementUnitByIdQuery request, CancellationToken cancellationToken)
    {
        MeasurementUnit? source = await _repository
            .GetByIdAsync(
                id: MeasurementUnitId.ToMeasurementUnitId(request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (source is null)
        {
            return DomainErrors.NotFound<MeasurementUnit>();
        }

        return source;
    }
}
