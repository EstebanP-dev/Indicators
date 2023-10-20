using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Domain.Features.Sources;

/// <summary>
/// Source model from the database table.
/// </summary>
public sealed class Source
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Source"/> class.
    /// </summary>
    /// <param name="id">Source id.</param>
    /// <param name="name">Source name.</param>
    public Source(int id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s name.
    /// </value>
    public string Name { get; set; }
}
