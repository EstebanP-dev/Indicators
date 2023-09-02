using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Users.GetUsersPagination;

/// <inheritdoc/>
internal sealed class GetUsersPaginationQueryHandler
    : IQueryHandler<GetUsersPaginationQuery, Pagination<UserPaginationResponse>>
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
    public async Task<Result<Pagination<UserPaginationResponse>>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
    {
        Either<Pagination<User>, Error> either = await _userRepository
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
