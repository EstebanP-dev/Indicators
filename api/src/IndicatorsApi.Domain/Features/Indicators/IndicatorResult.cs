using System.Linq.Expressions;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// IndicatorResult repository methods.
/// </summary>
public interface IIndicatorResultRepository
    : IRepository<IndicatorResult, int>
{
    /// <summary>
    /// Gets a variable indicator instance by indicator id.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="indicatorId">Indicator id.</param>
    /// <param name="selector">Variable mapping selector.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Returns either the variable indicator instance or null.</returns>
    Task<IEnumerable<TResponse>> GetAllByIndicatorIdAsync<TResponse>(
        int indicatorId,
        Expression<Func<IndicatorResult, TResponse>> selector,
        CancellationToken cancellationToken);
}

/// <summary>
/// IndicatorResult model from the database table.
/// </summary>
public sealed class IndicatorResult
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorResult"/> class.
    /// </summary>
    /// <param name="id">IndicatorResult id.</param>
    /// <param name="result">IndicatorResult result.</param>
    /// <param name="calculusDate">IndicatorResult calculus date.</param>
    public IndicatorResult(int id, double result, DateTime calculusDate)
        : base(id)
    {
        Result = result;
        CalculusDate = calculusDate;
    }

    /// <summary>
    /// Gets or sets the <see cref="IndicatorResult"/>'s result.
    /// </summary>
    /// <value>
    /// The <see cref="IndicatorResult"/>'s result.
    /// </value>
    required public double Result { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="IndicatorResult"/>'s calculus date.
    /// </summary>
    /// <value>
    /// The <see cref="IndicatorResult"/>'s calculus date.
    /// </value>
    required public DateTime CalculusDate { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="IndicatorResult"/>'s indicator id.
    /// </summary>
    /// <value>
    /// The <see cref="IndicatorResult"/>'s indicator id.
    /// </value>
    required public int IndicatorId { get; set; }
}
