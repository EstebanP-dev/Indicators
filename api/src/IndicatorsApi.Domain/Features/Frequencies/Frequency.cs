namespace IndicatorsApi.Domain.Features.Frequencies;

/// <summary>
/// Frequency repository methods.
/// </summary>
public interface IFrequencyRepository
    : IRepository<Frequency, int>
{
}

/// <summary>
/// Frequency model from the database table.
/// </summary>
public sealed class Frequency
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Frequency"/> class.
    /// </summary>
    /// <param name="id">Frequency id.</param>
    /// <param name="description">Frequency name.</param>
    public Frequency(int id, string description)
        : base(id)
    {
        Description = description;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Frequency"/> class.
    /// </summary>
    public Frequency()
        : base()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Frequency"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Frequency"/>'s name.
    /// </value>
    required public string Description { get; set; }
}