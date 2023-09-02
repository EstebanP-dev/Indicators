using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Roles.GetRolesPagination;

/// <inheritdoc/>
internal sealed class GetRolesPaginationQueryHandler
    : IQueryHandler<GetRolesPaginationQuery, Pagination<UserPaginationResponse>>
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
    public async Task<Result<Pagination<UserPaginationResponse>>> Handle(GetRolesPaginationQuery request, CancellationToken cancellationToken)
    {
        Either<Pagination<Role>, Error> either = await _roleRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    excludes: request.Excludes,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return either
                .Match(
                left: left => left.Adapt<Pagination<UserPaginationResponse>>(),
                right: Result.Failure<Pagination<UserPaginationResponse>>);
    }
}
