using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.SubSections.UpdateSubSection;

/// <inheritdoc/>
internal sealed class UpdateSubSectionCommandHandler
    : ICommandHandler<UpdateSubSectionCommand, Updated>
{
    private readonly ISubSectionRepository _subSectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSubSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateSubSectionCommandHandler(ISubSectionRepository subSectionRepository, IUnitOfWork unitOfWork)
    {
        _subSectionRepository = subSectionRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateSubSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            SubSection? subSection = await _subSectionRepository
                    .GetByIdAsync(id: SubSectionId.ToSubSectionId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (subSection is null)
            {
                return DomainErrors.NotFound<SubSection>();
            }

            subSection.Name = request.Name;

            _subSectionRepository.Update(entity: subSection);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Updated;
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
