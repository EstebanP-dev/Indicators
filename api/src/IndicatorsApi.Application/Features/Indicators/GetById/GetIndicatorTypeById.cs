using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetIndicatorTypeByIdQuery(int Id)
    : IQuery<IndicatorTypeByIdResponse>;

/// <inheritdoc/>
internal sealed class GetIndicatorTypeByIdQueryHandler
    : IQueryHandler<GetIndicatorTypeByIdQuery, IndicatorTypeByIdResponse>
{
    private readonly IIndicatorTypeRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorTypeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public GetIndicatorTypeByIdQueryHandler(IIndicatorTypeRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IndicatorTypeByIdResponse>> Handle(GetIndicatorTypeByIdQuery request, CancellationToken cancellationToken)
    {
        IndicatorType? indicatorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorType>();
        }

        return indicatorType.Adapt<IndicatorTypeByIdResponse>();
    }
}