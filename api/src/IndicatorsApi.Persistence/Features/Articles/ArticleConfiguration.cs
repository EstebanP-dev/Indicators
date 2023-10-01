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
            .HasConversion(
                articleId => articleId.Value,
                value => ArticleId.ToArticleId(value))
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
            .HasConversion(
                sectionId => sectionId.Value,
                value => SectionId.ToSectionId(value))
            .HasColumnName("fkidseccion")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(article => article.SubSectionId)
            .HasConversion(
                subsectionId => subsectionId.Value,
                value => SubSectionId.ToSubSectionId(value))
            .HasColumnName("fkidsubseccion")
            .HasMaxLength(2)
            .IsRequired();

        builder.HasOne(article => article.Section)
            .WithMany(section => section.Articles)
            .HasForeignKey(article => article.SectionId)
            .HasConstraintName("articulo_ibfk_1");

        builder.HasOne(article => article.SubSection)
            .WithMany(subsection => subsection.Articles)
            .HasForeignKey(article => article.SubSectionId)
            .HasConstraintName("articulo_ibfk_2");
    }
}