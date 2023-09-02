using System.Net;
using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Exceptions;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <inheritdoc/>
internal sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand>
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
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = _passwordHasher.HashPasword(request.Password, out byte[] salt);

            User user = new()
            {
                Email = request.Email,
                Password = passwordHash,
                Salt = new[] { salt },
            };

            Either<IEnumerable<Role>, Error> eitherRoles = await _roleRepository
                .GetBulkByIdsAsync(ids: request.Roles.ToArray(), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!eitherRoles.IsLeft)
            {
                return Result.Failure(
                        DomainErrors
                        .Role
                        .NotFound(
                                ids: request.Roles.ToArray()));
            }

            _userRepository.Add(entity: user);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            Either<User, Error> either = await _userRepository
                .GetByEmailAsync(
                    email: request.Email,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            _userRepository.AddUserRoles(
                userRoles: eitherRoles
                        .Match(
                            left: left => left
                                .Select(role => new UserRole()
                                {
                                    RoleId = role.Id,
                                    UserId = request.Email,
                                })
                                .ToArray(),
                            right: right => throw new InvalidEitherOperationBaseOnTheBusinessLogicException()));

            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return either.Match(
                            left: MapSuccessResult,
                            right: MapFailureResult);
        }
        catch (InvalidEitherOperationBaseOnTheBusinessLogicException ex)
        {
            return Result.Failure(
                    DomainErrors
                    .General
                    .UndefinedError(exception: ex));
        }
        catch (OperationCanceledException ex)
        {
            return Result.Failure(
                    DomainErrors
                    .General
                    .CancelledOperation(exception: ex));
        }
    }

    private static Result MapFailureResult(Error error)
    {
        return Result.Failure(error: error);
    }

    private static Result MapSuccessResult(User user)
    {
        return Result.Success();
    }
}
