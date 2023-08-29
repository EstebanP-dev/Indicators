using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class UserRepository
    : Repository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<Either<User, Error>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            User? user = await SingleByEmailAsync(DbContext, email)
                               .ConfigureAwait(false);

            if (user is null)
            {
                return new(
                    right: DomainErrors
                        .User
                        .NotFound(email));
            }

            return new(left: user);
        }
        catch (OperationCanceledException ex)
        {
            return new(
                right: DomainErrors
                    .General
                    .CancelledOperation(ex));
        }
        catch (ArgumentNullException ex)
        {
            return new(
                right: DomainErrors.General.UndefinedError(
                    new UserByEmailCannotBeFoundException(
                        email: email,
                        innerException: ex)));
        }
    }

    /// <inheritdoc/>
    public void AddUserRoles(UserRole[] userRoles)
    {
        foreach (UserRole userRole in userRoles)
        {
            DbContext.UserRoles
            .Add(new()
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
            });
        }
    }

    private static Task<User?> SingleByEmailAsync(ApplicationDbContext context, string email)
        => context.Users
            .AsSingleQuery()
            .SingleOrDefaultAsync(user => user.Email == email);
}
