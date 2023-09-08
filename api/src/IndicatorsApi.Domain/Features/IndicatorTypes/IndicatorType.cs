using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.IndicatorTypes;

/// <summary>
/// IndicatorType model from the database table.
/// </summary>
public sealed class IndicatorType
    : Entity<IndicatorTypeId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorType"/> class.
    /// </summary>
    /// <param name="id">IndicatorType id.</param>
    /// <param name="name">IndicatorType name.</param>
    public IndicatorType(IndicatorTypeId id, string name)
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
    public string Name { get; set; }
}
