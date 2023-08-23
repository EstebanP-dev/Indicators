using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Exceptions;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class UserRepository
    : IUserRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Either<User, Error>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            User? user = await _context.Users
                .AsSingleQuery()
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
            {
                return new Either<User, Error>(right: DomainErrors.User.NotFound(email));
            }

            return new Either<User, Error>(left: user);
        }
        catch (OperationCanceledException ex)
        {
            return new Either<User, Error>(
                right: DomainErrors.General.CancelledOperation(ex));
        }
        catch (ArgumentNullException ex)
        {
            return new Either<User, Error>(
                right: DomainErrors.General.UndefinedError(
                    new BaseException(
                        message: $"Email = {email}",
                        innerException: ex)));
        }
    }

    /// <inheritdoc/>
    public void Add(User user)
    {
        _context.Users
            .Add(user);
    }
}
