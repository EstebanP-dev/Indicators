using FluentValidation;
using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Articles;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Article name.</param>
/// <param name="Description">Article description.</param>
/// <param name="Section">Article section.</param>
/// <param name="SubSection">Article subsection.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateArticleCommand(string Name, string Description, string Section, string SubSection)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateArticleValidator"/> class.
    /// </summary>
    /// <param name="sectionRepository">Instance of <see cref="ISectionRepository"/>.</param>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    public CreateArticleValidator(ISectionRepository sectionRepository, ISubSectionRepository subSectionRepository)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Section)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Section)
            .MustAsync(async (sectionId, _) =>
            {
                return await sectionRepository.DoEntityExistsAsync(sectionId, default)
                    .ConfigureAwait(false);
            })
            .WithMessage(DomainErrors.NotFound<Section>().Description);

        RuleFor(x => x.SubSection)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.SubSection)
            .MustAsync(async (subSectionId, _) =>
            {
                return await subSectionRepository.DoEntityExistsAsync(subSectionId, default)
                    .ConfigureAwait(false);
            })
            .WithMessage(DomainErrors.NotFound<SubSection>().Description);
    }
}

/// <inheritdoc/>
internal sealed class CreateArticleCommandHandler
    : ICommandHandler<CreateArticleCommand, Created>
{
    private readonly IArticleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateArticleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IArticleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateArticleCommandHandler(IArticleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: new Article
        {
            Id = GenerateArticleId(request),
            Name = request.Name,
            Description = request.Description,
            SectionId = request.Section,
            SubSectionId = request.SubSection,
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result.Created;
    }

    private static string GenerateArticleId(CreateArticleCommand request)
    {
        return $"{request.Section}.{request.SubSection}";
    }
}