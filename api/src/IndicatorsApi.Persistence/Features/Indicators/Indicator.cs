using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class IndicatorRepository
    : Repository<Indicator, int>, IIndicatorRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public IndicatorRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public override Task<Indicator?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbContext.Indicators
                .Where(x => x.Id == id)
                .IgnoreAutoIncludes()
                .Include(x => x.IndicatorType)
                .Include(x => x.MeasurementUnit)
                .Include(x => x.Meaning)
                .Include(x => x.Frequency)
                .Include(x => x.Displays)
                .Include(x => x.Sources)
                .Include(x => x.Actors)
                    .ThenInclude(x => x.ActorType)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(cancellationToken);
    }
}

/// <inheritdoc/>
internal sealed class IndicatorConfiguration
    : IEntityTypeConfiguration<Indicator>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Indicator> builder)
    {
        builder
            .ToTable("indicador");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Objective)
            .HasColumnName("objetivo")
            .HasMaxLength(4000)
            .IsRequired();

        builder
            .Property(x => x.Scope)
            .HasColumnName("alcance")
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(x => x.Formula)
            .HasColumnName("formula")
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(x => x.IndicatorTypeId)
            .HasColumnName("fkidtipoindicador")
            .IsRequired();

        builder
            .Property(x => x.MeasurementUnitId)
            .HasColumnName("fkidunidadmedicion")
            .IsRequired();

        builder
            .Property(x => x.Goal)
            .HasColumnName("meta")
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(x => x.MeaningId)
            .HasColumnName("fkidsentido")
            .IsRequired();

        builder
            .Property(x => x.FrequencyId)
            .HasColumnName("fkidfrecuencia")
            .IsRequired();

        builder
            .HasOne(x => x.IndicatorType)
            .WithMany()
            .HasForeignKey(x => x.IndicatorTypeId)
            .HasConstraintName("indicador_ibfk_1");

        builder
            .HasOne(x => x.MeasurementUnit)
            .WithMany()
            .HasForeignKey(x => x.MeasurementUnitId)
            .HasConstraintName("indicador_ibfk_2");

        builder
            .HasOne(x => x.Meaning)
            .WithMany()
            .HasForeignKey(x => x.MeaningId)
            .HasConstraintName("indicador_ibfk_3");

        builder
            .HasOne(x => x.Frequency)
            .WithMany()
            .HasForeignKey(x => x.FrequencyId)
            .HasConstraintName("indicador_ibfk_4");

        builder
            .HasMany(x => x.Displays)
            .WithMany()
            .UsingEntity<DisplayIndicator>(
                "represenvisualporindicador",
                model => model
                    .HasOne<Display>()
                    .WithMany()
                    .HasForeignKey(x => x.DisplayId)
                    .HasConstraintName("cons_fkidrepresenvisual"),
                model => model
                    .HasOne<Indicator>()
                    .WithMany()
                    .HasForeignKey(x => x.IndicatorId)
                    .HasConstraintName("cons_fkidindicador2"),
                modelBuilder =>
                {
                    modelBuilder
                        .ToTable("represenvisualporindicador");

                    modelBuilder
                        .Property(x => x.DisplayId)
                        .HasColumnName("fkidrepresenvisual")
                        .IsRequired();

                    modelBuilder
                        .Property(x => x.IndicatorId)
                        .HasColumnName("fkidindicador")
                        .IsRequired();
                });

        builder
            .HasMany(x => x.Sources)
            .WithMany()
            .UsingEntity<SourceIndicator>(
                "fuentesporindicador",
                model => model
                    .HasOne<Source>()
                    .WithMany()
                    .HasForeignKey(x => x.SourceId)
                    .HasConstraintName("cons_fkidfuente"),
                model => model
                    .HasOne<Indicator>()
                    .WithMany()
                    .HasForeignKey(x => x.IndicatorId)
                    .HasConstraintName("cons_fkidindicador1"),
                modelBuilder =>
                {
                    modelBuilder
                        .ToTable("fuentesporindicador");

                    modelBuilder
                        .HasKey(x => new { x.SourceId, x.IndicatorId })
                        .HasName("fuentesporindicador_pkey");

                    modelBuilder
                        .Property(x => x.SourceId)
                        .HasColumnName("fkidfuente")
                        .IsRequired();

                    modelBuilder
                        .Property(x => x.IndicatorId)
                        .HasColumnName("fkidindicador")
                        .IsRequired();
                });

        builder
            .HasMany(x => x.Actors)
            .WithMany()
            .UsingEntity<ActorIndicator>(
                "responsablesporindicador",
                model => model
                    .HasOne<Actor>()
                    .WithMany()
                    .HasForeignKey(x => x.ActorId)
                    .HasConstraintName("cons_fkidresponsable"),
                model => model
                    .HasOne<Indicator>()
                    .WithMany()
                    .HasForeignKey(x => x.IndicatorId)
                    .HasConstraintName("cons_fkidindicador1"),
                modelBuilder =>
                {
                    modelBuilder
                        .ToTable("responsablesporindicador");

                    modelBuilder
                        .Property(x => x.ActorId)
                        .HasColumnName("fkidresponsable")
                        .IsRequired();

                    modelBuilder
                        .Property(x => x.IndicatorId)
                        .HasColumnName("fkidindicador")
                        .IsRequired();

                    modelBuilder
                        .Property(x => x.Date)
                        .HasColumnName("fechaasignacion")
                        .ValueGeneratedOnAdd()
                        .IsRequired();
                });
    }
}