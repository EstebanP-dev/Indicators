using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Necessary.")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
public sealed record class GetActorTypesPaginationQuery(PaginationParameters<int> Parameters)
    : IQuery<Pagination<ActorTypePaginationResponse>>;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

/// <inheritdoc/>
internal sealed class GetActorTypesPaginationQueryHandler
    : IQueryHandler<GetActorTypesPaginationQuery, Pagination<ActorTypePaginationResponse>>
{
    private readonly IActorTypeRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorTypesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorTypeRepository"/>.</param>
    public GetActorTypesPaginationQueryHandler(IActorTypeRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<ActorTypePaginationResponse>>> Handle(GetActorTypesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<ActorTypePaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new ActorTypePaginationResponse(x.Id, x.Name),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}