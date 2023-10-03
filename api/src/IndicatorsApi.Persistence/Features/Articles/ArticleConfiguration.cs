using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Persistence.Features.Articles;

/// <inheritdoc/>
internal sealed class ArticleConfiguration
    : IEntityTypeConfiguration<Article>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articulo");

        builder.HasKey(articule => articule.Id);

        builder.Property(article => article.Id)
            .HasColumnName("id")
            .HasMaxLength(20)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(article => article.Name)
            .HasColumnName("nombre")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(article => article.Description)
            .HasColumnName("descripcion")
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(article => article.SectionId)
            .HasColumnName("fkidseccion")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(article => article.SubSectionId)
            .HasColumnName("fkidsubseccion")
            .HasMaxLength(2)
            .IsRequired();

        builder.HasOne(article => article.Section)
            .WithMany()
            .HasForeignKey(article => article.SectionId)
            .HasConstraintName("articulo_ibfk_1");

        builder.HasOne(article => article.SubSection)
            .WithMany()
            .HasForeignKey(article => article.SubSectionId)
            .HasConstraintName("articulo_ibfk_2");
    }
}