using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRolesPagination;

/// <inheritdoc/>
internal sealed class GetRolesPaginationQueryHandler
    : IQueryHandler<GetRolesPaginationQuery, Pagination<Role>>
{
    private readonly IRoleRepository _actorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRolesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IRoleRepository"/>.</param>
    public GetRolesPaginationQueryHandler(IRoleRepository actorTypeRepository)
    {
        _actorTypeRepository = actorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Role>>> Handle(GetRolesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Role> pagination = await _actorTypeRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: request.Excludes ?? Array.Empty<int>(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
