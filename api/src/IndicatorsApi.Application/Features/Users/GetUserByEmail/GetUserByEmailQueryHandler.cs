using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Users.GetUserByEmail;

/// <summary>
/// <see cref="GetUserByEmailQuery"/> handler.
/// </summary>
internal sealed class GetUserByEmailQueryHandler
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByEmailQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository"><see cref="IUserRepository"/> instance.</param>
    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        Either<User, Error> userResponse = await _userRepository
            .GetByEmailAsync(
                email: request.Email,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return userResponse
                .Match(
                    MapUserResponse,
                    MapErrorResponse);
    }

    private static Result<UserResponse> MapErrorResponse(Error error)
    {
        return Result.Failure<UserResponse>(error);
    }

    private static Result<UserResponse> MapUserResponse(User user)
    {
        return user.Adapt<UserResponse>();
    }
}
