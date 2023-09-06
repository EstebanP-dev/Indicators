using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Roles.GetRolesPagination;

/// <inheritdoc/>
internal sealed class GetRolesPaginationQueryHandler
    : IQueryHandler<GetRolesPaginationQuery, Pagination<Role>>
{
    private readonly ISourceRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRolesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="ISourceRepository"/>.</param>
    public GetRolesPaginationQueryHandler(ISourceRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Role>>> Handle(GetRolesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Role> pagination = await _roleRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => RoleId.ToRoleId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
