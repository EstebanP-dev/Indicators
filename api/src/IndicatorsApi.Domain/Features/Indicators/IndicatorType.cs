using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// IndicatorType repository methods.
/// </summary>
public interface IIndicatorTypeRepository
    : IRepository<IndicatorType, int>
{
}

/// <summary>
/// IndicatorType model from the database table.
/// </summary>
public sealed class IndicatorType
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorType"/> class.
    /// </summary>
    /// <param name="id">IndicatorType id.</param>
    /// <param name="name">IndicatorType name.</param>
    public IndicatorType(int id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="IndicatorType"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="IndicatorType"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
