using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Users.GetUsersPagination;

/// <inheritdoc/>
internal sealed class GetUsersPaginationQueryHandler
    : IQueryHandler<GetUsersPaginationQuery, Pagination<User>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    public GetUsersPaginationQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<User>>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<User> pagination = await _userRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<string>())
                        .Select(id => new UserId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
