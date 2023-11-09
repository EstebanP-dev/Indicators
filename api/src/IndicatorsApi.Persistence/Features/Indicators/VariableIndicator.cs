using System.Linq.Expressions;
using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Contracts.Variables;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class VariableIndicatorByIndicatorIdSpecification
    : Specification<VariableIndicator, int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableIndicatorByIndicatorIdSpecification"/> class.
    /// </summary>
    /// <param name="indicatorId">Indicator id.</param>
    public VariableIndicatorByIndicatorIdSpecification(int indicatorId)
        : base(x => x.IndicatorId == indicatorId)
    {
    }
}

/// <inheritdoc/>
internal sealed class VariableIndicatorRepository
    : Repository<VariableIndicator, int>, IVariableIndicatorRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableIndicatorRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public VariableIndicatorRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TResponse>> GetAllByIndicatorIdAsync<TResponse>(
            int indicatorId,
            Expression<Func<VariableIndicator, TResponse>> selector,
            CancellationToken cancellationToken)
        => await ApplySpecification(new VariableIndicatorByIndicatorIdSpecification(indicatorId))
        .Select(selector)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);
}

/// <inheritdoc/>
internal sealed class VariableIndicatorConfiguration
    : IEntityTypeConfiguration<VariableIndicator>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<VariableIndicator> builder)
    {
        builder
            .ToTable("variablesporindicador");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Datum)
            .HasColumnName("dato")
            .IsRequired();

        builder
            .Property(x => x.Date)
            .HasColumnName("fechadato")
            .IsRequired();

        builder
            .Property(x => x.VariableId)
            .HasColumnName("fkidvariable")
            .IsRequired();

        builder
            .Property(x => x.IndicatorId)
            .HasColumnName("fkidindicador")
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasColumnName("fkemailusuario")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasOne(x => x.Variable)
            .WithMany()
            .HasForeignKey(x => x.VariableId)
            .HasConstraintName("cons_fkidvariable");
    }
}