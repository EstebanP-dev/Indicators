using System.Linq.Expressions;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class IndicatorResultByIndicatorIdSpecification
    : Specification<IndicatorResult, int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorResultByIndicatorIdSpecification"/> class.
    /// </summary>
    /// <param name="indicatorId">Indicator id.</param>
    public IndicatorResultByIndicatorIdSpecification(int indicatorId)
        : base(x => x.IndicatorId == indicatorId)
    {
    }
}

/// <inheritdoc cref="IIndicatorResultRepository" />
internal sealed class IndicatorResultRepository
    : Repository<IndicatorResult, int>, IIndicatorResultRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorResultRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public IndicatorResultRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TResponse>> GetAllByIndicatorIdAsync<TResponse>(
            int indicatorId,
            Expression<Func<IndicatorResult, TResponse>> selector,
            CancellationToken cancellationToken)
        => await ApplySpecification(new IndicatorResultByIndicatorIdSpecification(indicatorId))
        .Select(selector)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);
}

/// <inheritdoc/>
internal sealed class IndicatorResultConfiguration
    : IEntityTypeConfiguration<IndicatorResult>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IndicatorResult> builder)
    {
        builder.ToTable(@"resultadoindicador");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Result)
            .HasColumnName(@"resultado")
            .IsRequired();

        builder.Property(x => x.CalculusDate)
            .HasColumnName(@"fechacalculo")
            .IsRequired();

        builder.Property(x => x.IndicatorId)
            .HasColumnName(@"fkidindicador")
            .IsRequired();
    }
}
