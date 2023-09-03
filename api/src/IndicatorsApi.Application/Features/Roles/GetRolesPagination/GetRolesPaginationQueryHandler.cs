using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Roles.GetRolesPagination;

/// <inheritdoc/>
internal sealed class GetRolesPaginationQueryHandler
    : IQueryHandler<GetRolesPaginationQuery, Pagination<Role>>
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRolesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    public GetRolesPaginationQueryHandler(IRoleRepository roleRepository)
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
                            id => new RoleId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
