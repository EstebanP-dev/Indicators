using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class RoleRepository
    : Repository<Role>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public RoleRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<Either<Role, Error>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Role? role = await SingleByIdAsync(DbContext, id, cancellationToken)
                               .ConfigureAwait(false);

            if (role is null)
            {
                return new(right: DomainErrors
                        .Role
                        .NotFound(
                            ids: new int[] { id }));
            }

            return new(left: role);
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
                    new RoleOrRolesByIdCannotBeFoundException(
                        ids: new int[] { id },
                        innerException: ex)));
        }
    }

    /// <inheritdoc/>
    public Task<Either<IEnumerable<Role>, Error>> GetPaginationAsync(int page, int rows, out int totalPages, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Either<IEnumerable<Role>, Error>> GetBulkByIdsAsync(int[] ids, CancellationToken cancellationToken)
    {
        try
        {
            List<Role> roles = await MasiveByIdsAsync(
                context: DbContext,
                ids: ids,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

            if (roles is null)
            {
                return new(right: DomainErrors
                        .General
                        .UndefinedError(
                        exception: new RoleOrRolesByIdCannotBeFoundException(
                                    ids: ids)));
            }

            return new(left: roles);
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
                    new RoleOrRolesByIdCannotBeFoundException(
                        ids: ids,
                        innerException: ex)));
        }
    }

    /// <inheritdoc/>
    public async Task<Either<IEnumerable<int>, Error>> GetRolesIdsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            List<int> ids = await GetIdsByUserIdAsync(
                    context: DbContext,
                    userId: userId,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return new(left: ids);
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
                        email: userId,
                        innerException: ex)));
        }
    }

    private static Task<Role?> SingleByIdAsync(ApplicationDbContext context, int id, CancellationToken cancellationToken)
        => context.Roles
            .AsSingleQuery()
            .SingleOrDefaultAsync(role => role.Id == id, cancellationToken);

    private static Task<List<int>> GetIdsByUserIdAsync(ApplicationDbContext context, string userId, CancellationToken cancellationToken)
        => context.UserRoles
            .AsSingleQuery()
            .Where(userRole => userRole.UserId == userId)
            .Select(userRole => userRole.RoleId)
            .ToListAsync(cancellationToken);

    private static Task<List<Role>> MasiveByIdsAsync(ApplicationDbContext context, int[] ids, CancellationToken cancellationToken)
        => context.Roles
            .AsSingleQuery()
            .Where(role => ids.Contains(role.Id))
            .ToListAsync(cancellationToken);
}
