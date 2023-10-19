namespace IndicatorsApi.Domain.Features.Frecuencies;

/// <summary>
/// Frecuency repository methods.
/// </summary>
public interface IFrecuencyRepository
    : IRepository<Frecuency, int>
{
}

/// <summary>
/// Frecuency model from the database table.
/// </summary>
public sealed class Frecuency
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Frecuency"/> class.
    /// </summary>
    /// <param name="id">Frecuency id.</param>
    /// <param name="description">Frecuency name.</param>
    public Frecuency(int id, string description)
        : base(id)
    {
        Description = description;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Frecuency"/> class.
    /// </summary>
    public Frecuency()
        : base()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Frecuency"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Frecuency"/>'s name.
    /// </value>
    required public string Description { get; set; }
}