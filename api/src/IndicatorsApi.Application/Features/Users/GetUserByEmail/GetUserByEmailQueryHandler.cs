using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Users.GetUserByEmail;

/// <summary>
/// <see cref="GetUserByEmailQuery"/> handler.
/// </summary>
internal sealed class GetUserByEmailQueryHandler
    : IQueryHandler<GetUserByEmailQuery, User>
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
    public async Task<ErrorOr<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(
                id: new UserId(request.Email),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return DomainErrors.NotFound<User>();
        }

        return user;
    }
}
