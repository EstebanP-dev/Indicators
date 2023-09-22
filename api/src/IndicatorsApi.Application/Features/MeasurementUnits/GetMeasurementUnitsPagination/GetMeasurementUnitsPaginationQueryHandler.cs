using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.Application.Features.MeasurementUnits.GetMeasurementUnitsPagination;

/// <inheritdoc/>
internal sealed class GetMeasurementUnitsPaginationQueryHandler
    : IQueryHandler<GetMeasurementUnitsPaginationQuery, Pagination<MeasurementUnit>>
{
    private readonly IMeasurementUnitRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMeasurementUnitsPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    public GetMeasurementUnitsPaginationQueryHandler(IMeasurementUnitRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<MeasurementUnit>>> Handle(GetMeasurementUnitsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<MeasurementUnit> pagination = await _repository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => MeasurementUnitId.ToMeasurementUnitId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
