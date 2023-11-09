using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetIndicatorResultByIdQuery(int Id)
    : IQuery<IndicatorResultByIdResponse>;

/// <inheritdoc/>
internal sealed class GetIndicatorResultByIdQueryHandler
    : IQueryHandler<GetIndicatorResultByIdQuery, IndicatorResultByIdResponse>
{
    private readonly IIndicatorResultRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorResultByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    public GetIndicatorResultByIdQueryHandler(IIndicatorResultRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IndicatorResultByIdResponse>> Handle(GetIndicatorResultByIdQuery request, CancellationToken cancellationToken)
    {
        IndicatorResult? indicatorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorResult>();
        }

        return indicatorType.Adapt<IndicatorResultByIdResponse>();
    }
}