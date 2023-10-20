using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// VariableIndicator repository methods.
/// </summary>
public interface IVariableIndicatorRepository
    : IRepository<VariableIndicator, int>
{
}

/// <summary>
/// VariableIndicator model from the database table.
/// </summary>
public sealed class VariableIndicator
    : Entity<int>
{
    /// <summary>
    /// Gets or sets the <see cref="VariableIndicator"/>'s variable id.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s variable id.
    /// </value>
    required public int VariableId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VariableIndicator"/>'s indicator id.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s indicator id.
    /// </value>
    required public int IndicatorId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VariableIndicator"/>'s datum.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s datum.
    /// </value>
    required public double Datum { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VariableIndicator"/>'s date.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s date.
    /// </value>
    required public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VariableIndicator"/>'s user id.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s user id.
    /// </value>
    required public string UserId { get; set; }

    /// <summary>
    /// Gets the <see cref="VariableIndicator"/>'s <see cref="Variable"/>.
    /// </summary>
    /// <value>
    /// The <see cref="VariableIndicator"/>'s <see cref="Variable"/>.
    /// </value>
    public Variable? Variable { get; }
}
