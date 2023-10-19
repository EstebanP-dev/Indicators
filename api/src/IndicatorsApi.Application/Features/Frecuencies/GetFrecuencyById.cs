using System.Globalization;
using IndicatorsApi.Contracts.Frecuencies;
using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Contracts.SubSections;
using IndicatorsApi.Domain.Features.Frecuencies;

namespace IndicatorsApi.Application.Features.Frecuencies;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Frecuency id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetFrecuencyByIdQuery(int Id)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<FrecuencyByIdResponse>;

/// <inheritdoc/>
internal sealed class GetFrecuencyByIdQueryHandler
    : IQueryHandler<GetFrecuencyByIdQuery, FrecuencyByIdResponse>
{
    private readonly IFrecuencyRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFrecuencyByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrecuencyRepository"/>.</param>
    public GetFrecuencyByIdQueryHandler(IFrecuencyRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<FrecuencyByIdResponse>> Handle(GetFrecuencyByIdQuery request, CancellationToken cancellationToken)
    {
        Frecuency? article = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (article == null)
        {
            return DomainErrors.NotFound<Frecuency>();
        }

        return article.Adapt<FrecuencyByIdResponse>();
    }
}
