using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Users.GetUserById;

/// <inheritdoc/>
internal sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(
                id: UserId.ToUserId(value: request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return DomainErrors.NotFound<User>();
        }

        return user;
    }
}
