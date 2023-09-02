using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Users.GetUserByEmail;

/// <summary>
/// <see cref="GetUserByEmailQuery"/> handler.
/// </summary>
internal sealed class GetUserByEmailQueryHandler
    : IQueryHandler<GetUserByEmailQuery, UserByEmailResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByEmailQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository"><see cref="IUserRepository"/> instance.</param>
    /// <param name="roleRepository"><see cref="IRoleRepository"/> instance.</param>
    public GetUserByEmailQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<UserByEmailResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        Either<User, Error> userResponse = await _userRepository
            .GetByEmailAsync(
                email: request.Email,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        (UserByEmailResponse?, Error?) previusResponse = userResponse
            .Match<(UserByEmailResponse?, Error?)>(
                    left: left =>
                    {
                        UserByEmailResponse userResponse = left.Adapt<UserByEmailResponse>();

                        return (userResponse, null);
                    },
                    right: right =>
                        (null, right));

        if (previusResponse.Item2 is not null && previusResponse.Item1 is null)
        {
            return Result.Failure<UserByEmailResponse>(previusResponse.Item2);
        }

        Either<IEnumerable<int>, Error> rolesResponse = await _roleRepository
            .GetRolesIdsByUserIdAsync(
                userId: request.Email,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        (UserByEmailResponse?, Error?) response = rolesResponse
            .Match<(UserByEmailResponse?, Error?)>(
                left: left =>
                {
                    UserByEmailResponse userResponse = new(request.Email, left);

                    return (userResponse, null);
                },
                right: right =>
                    (null, right));

        return MapResultFromTuple(response);
    }

    private static Result<UserByEmailResponse> MapResultFromTuple((UserByEmailResponse?, Error?) tuple)
    {
        if (tuple.Item1 is null && tuple.Item2 is not null)
        {
            return Result.Failure<UserByEmailResponse>(tuple.Item2);
        }

        return Result.Success(tuple.Item1!);
    }
}
