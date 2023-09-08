using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Users;

/// <inheritdoc/>
internal sealed class UserRepository
    : Repository<User, UserId>, IUserRepository
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
    public override async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users
            .Include(user => user.Roles)
            .IgnoreAutoIncludes()
            .AsSingleQuery()
            .FirstOrDefaultAsync(
                predicate: user => user.Id == id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
