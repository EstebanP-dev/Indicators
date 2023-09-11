using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.SubSections.CreateSubSection;

/// <inheritdoc/>
internal sealed class CreateSubSectionCommandHandler
    : ICommandHandler<CreateSubSectionCommand, Created>
{
    private readonly ISubSectionRepository _subSectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSubSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateSubSectionCommandHandler(ISubSectionRepository subSectionRepository, IUnitOfWork unitOfWork)
    {
        _subSectionRepository = subSectionRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateSubSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _subSectionRepository.Add(entity: request.Adapt<SubSection>());

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
