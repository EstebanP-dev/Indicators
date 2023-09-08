using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.IndicatorTypes;

namespace IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypeById;

/// <inheritdoc/>
internal sealed class GetIndicatorTypeByIdQueryHandler
    : IQueryHandler<GetIndicatorTypeByIdQuery, IndicatorType>
{
    private readonly IIndicatorTypeRepository _indicatorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorTypeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public GetIndicatorTypeByIdQueryHandler(IIndicatorTypeRepository indicatorTypeRepository)
    {
        _indicatorTypeRepository = indicatorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IndicatorType>> Handle(GetIndicatorTypeByIdQuery request, CancellationToken cancellationToken)
    {
        IndicatorType? indicatorType = await _indicatorTypeRepository
            .GetByIdAsync(
                id: IndicatorTypeId.ToIndicatorTypeId(request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorType>();
        }

        return indicatorType;
    }
}
