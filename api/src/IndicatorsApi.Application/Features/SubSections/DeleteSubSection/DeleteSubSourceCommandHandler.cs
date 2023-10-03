using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.SubSections.DeleteSubSection;

/// <inheritdoc/>
internal sealed class DeleteSubSectionCommandHandler
    : ICommandHandler<DeleteSubSectionCommand, Deleted>
{
    private readonly ISubSectionRepository _subSectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSubSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteSubSectionCommandHandler(ISubSectionRepository subSectionRepository, IUnitOfWork unitOfWork)
    {
        _subSectionRepository = subSectionRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteSubSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            SubSection? subSection = await _subSectionRepository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (subSection is null)
            {
                return DomainErrors.NotFound<SubSection>();
            }

            _subSectionRepository.Delete(entity: subSection);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Deleted;
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
