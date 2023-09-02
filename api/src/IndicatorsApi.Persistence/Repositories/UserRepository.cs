using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;

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
            User? user = await SingleByEmailAsync(DbContext, email, cancellationToken)
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

    /// <inheritdoc/>
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    public async Task<Either<Pagination<User>, Error>> GetPaginationAsync(int page, int rows, string[]? excludes, CancellationToken cancellationToken = default)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    {
        try
        {
            int totalRows = await CountAsync(DbContext, cancellationToken)
                .ConfigureAwait(false);

            int totalPages = PaginationUtils.ConvertTotalPages(totalRows: totalRows, pageSize: rows);

            List<User> users = await PaginationAsync(
                        context: DbContext,
                        page: page,
                        rows: rows,
                        excludes: excludes ?? Array.Empty<string>(),
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            Pagination<User> pagination = new(
                totalPages: totalPages,
                currentPage: page,
                pageSize: rows,
                response: users);

            return new(pagination);
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
                    new UsersCannotBeSearchedException(
                        innerException: ex)));
        }
    }

    private static Task<User?> SingleByEmailAsync(ApplicationDbContext context, string email, CancellationToken cancellationToken)
        => context.Users
            .AsSingleQuery()
            .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

    private static Task<List<User>> PaginationAsync(ApplicationDbContext context, int page, int rows, string[] excludes, CancellationToken cancellationToken)
        => context.Users
            .AsSingleQuery()
            .Where(user => !excludes.Any(exclude => exclude == user.Email))
            .Skip(page * rows)
            .Take(rows)
            .ToListAsync(cancellationToken);

    private static Task<int> CountAsync(ApplicationDbContext context, CancellationToken cancellationToken)
        => context.Users
            .AsSingleQuery()
            .CountAsync(cancellationToken);
}
