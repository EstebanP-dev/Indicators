using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <inheritdoc/>
internal sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    public CreateUserValidator(IUserRepository repository, IRoleRepository roleRepository)
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .MustAsync(async (id, cancellationToken) =>
                !(await repository
                        .DoEntityExistsAsync(id, cancellationToken)
                        .ConfigureAwait(false)))
            .WithMessage(DomainErrors.AlreadyExists<User>().Description);

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Roles)
            .MustAsync((ids, cancellationToken) => roleRepository.DoEntitiesExistsAsync(ids.ToArray(), cancellationToken))
            .WithMessage(DomainErrors.BulkNotFound.Description);
    }
}

/// <inheritdoc/>
internal sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand, Created>
{
    private readonly IUserRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="passwordHasher">Instance of <see cref="IPasswordHasher"/>.</param>
    public CreateUserCommandHandler(
        IUserRepository repository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Role> roles = await _roleRepository
            .GetBulkIdsAsync(
                ids: request.Roles
                    .Select(id => id)
                    .ToArray(),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (roles.Any(role => !request.Roles.Contains(role.Id)))
        {
            return DomainErrors.BulkNotFound;
        }

        string passwordHash = _passwordHasher.HashPasword(password: request.Password, salt: out byte[] salt);

        User user = new()
        {
            Id = request.Email,
            Password = passwordHash,
            Salt = new[] { salt },
        };

        foreach (Role role in roles)
        {
            user.Add(role: role);
        }

        _repository.Add(entity: user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}
