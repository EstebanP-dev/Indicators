using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Sections.CreateSection;

/// <inheritdoc/>
internal sealed class CreateSectionCommandHandler
    : ICommandHandler<CreateSectionCommand, Created>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="sectionRepository">Instance of <see cref="ISectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateSectionCommandHandler(ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _sectionRepository.Add(entity: request.Adapt<Section>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Created;
        }
        catch (DbUpdateException)
        {
            return DomainErrors.CreationOrUpdatingFailed;
        }
        catch (OperationCanceledException)
        {
            return DomainErrors.CancelledOperation;
        }
    }
}
