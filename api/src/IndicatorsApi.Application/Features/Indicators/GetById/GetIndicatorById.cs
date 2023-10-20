using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Indicator id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetIndicatorByIdQuery(int Id)
    : IQuery<IndicatorByIdResponse>;

/// <inheritdoc/>
internal sealed class GetIndicatorByIdQueryHandler
    : IQueryHandler<GetIndicatorByIdQuery, IndicatorByIdResponse>
{
    private readonly IIndicatorRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    public GetIndicatorByIdQueryHandler(IIndicatorRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IndicatorByIdResponse>> Handle(GetIndicatorByIdQuery request, CancellationToken cancellationToken)
    {
        Indicator? indicatorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<Indicator>();
        }

        return indicatorType.Adapt<IndicatorByIdResponse>();
    }
}