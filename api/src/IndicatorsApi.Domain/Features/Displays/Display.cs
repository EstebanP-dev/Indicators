using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Displays;

/// <summary>
/// Display model from the database table.
/// </summary>
public sealed class Display
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Display"/> class.
    /// </summary>
    /// <param name="id">Display id.</param>
    /// <param name="name">Display name.</param>
    public Display(int id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Display"/> class.
    /// </summary>
    public Display()
        : base()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Display"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Display"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
