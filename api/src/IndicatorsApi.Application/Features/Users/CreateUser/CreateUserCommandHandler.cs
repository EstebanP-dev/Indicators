using System.Net;
using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Exceptions;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <inheritdoc/>
internal sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand, User>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="passwordHasher">Instance of <see cref="IPasswordHasher"/>.</param>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateUserCommandHandler(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = _passwordHasher.HashPasword(request.Password, out byte[] salt);

            User user = new(id: UserId.ToUserId(request.Email), password: passwordHash)
            {
                Salt = new[] { salt },
            };

            IEnumerable<Role> roles = await _roleRepository
                .GetBulkIdsAsync(
                    ids: request.Roles
                        .Select(roleId => RoleId.ToRoleId(roleId))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (roles.All(role => request.Roles.Any(roleId => roleId == role.Id.Value)))
            {
                return DomainErrors.BulkNotFound;
            }

            foreach (Role role in roles)
            {
                user.Add(role);
            }

            _userRepository.Add(entity: user);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return user;
        }
        catch (OperationCanceledException)
        {
            return DomainErrors.CancelledOperation;
        }
    }
}
