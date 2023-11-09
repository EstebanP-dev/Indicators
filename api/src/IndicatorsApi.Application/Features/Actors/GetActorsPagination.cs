using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
public sealed record class GetActorsPaginationQuery(PaginationParameters<string> Parameters)
    : IQuery<Pagination<ActorPaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetActorsPaginationQueryHandler
    : IQueryHandler<GetActorsPaginationQuery, Pagination<ActorPaginationResponse>>
{
    private readonly IActorRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorsPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorRepository"/>.</param>
    public GetActorsPaginationQueryHandler(IActorRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<ActorPaginationResponse>>> Handle(GetActorsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<ActorPaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new ActorPaginationResponse(x.Id, x.Name),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}