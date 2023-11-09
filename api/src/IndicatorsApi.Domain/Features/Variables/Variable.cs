using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Variables;

/// <summary>
/// Variable model from the database table.
/// </summary>
public sealed class Variable
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Variable"/> class.
    /// </summary>
    /// <param name="id">Variable id.</param>
    /// <param name="name">Variable name.</param>
    /// <param name="creationDate">Variable creationDate.</param>
    /// <param name="createdBy">Variable created by.</param>
    public Variable(int id, string name, DateTime creationDate, string createdBy)
        : base(id)
    {
        Name = name;
        CreationDate = creationDate;
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Variable"/> class.
    /// </summary>
    public Variable()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Variable"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Variable"/>'s name.
    /// </value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Variable"/> creation date.
    /// </summary>
    /// <value>
    /// The <see cref="Variable"/> creation date.
    /// </value>
    required public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the creation user email.
    /// </summary>
    /// <value>
    /// The creation user email.
    /// </value>
    required public string CreatedBy { get; set; }
}
