using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Sections.DeleteSection;

/// <inheritdoc/>
internal sealed class DeleteSectionCommandHandler
    : ICommandHandler<DeleteSectionCommand, Deleted>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="sectionRepository">Instance of <see cref="ISectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteSectionCommandHandler(ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Section? section = await _sectionRepository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (section is null)
            {
                return DomainErrors.NotFound<Section>();
            }

            _sectionRepository.Delete(entity: section);

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
