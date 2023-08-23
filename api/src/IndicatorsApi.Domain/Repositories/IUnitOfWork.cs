using System.Data;

namespace IndicatorsApi.Domain.Repositories;

/// <summary>
/// Unit of work pattern.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Save database changes.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token instance.</param>
    /// <returns>Returns a task.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a database transaction.
    /// </summary>
    /// <param name="isolationLevel">Instance of <see cref="IsolationLevel"/>.</param>
    /// <returns>Returns an instance of <see cref="IDbTransaction"/>.</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
}
