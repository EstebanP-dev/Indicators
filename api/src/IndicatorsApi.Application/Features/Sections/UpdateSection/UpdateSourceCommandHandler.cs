using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Sections.UpdateSection;

/// <inheritdoc/>
internal sealed class UpdateSectionCommandHandler
    : ICommandHandler<UpdateSectionCommand, Updated>
{
    private readonly ISectionRepository _sourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSectionCommandHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISectionRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateSectionCommandHandler(ISectionRepository sourceRepository, IUnitOfWork unitOfWork)
    {
        _sourceRepository = sourceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Section? source = await _sourceRepository
                    .GetByIdAsync(id: SectionId.ToSectionId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (source is null)
            {
                return DomainErrors.NotFound<Section>();
            }

            source.Name = request.Name;

            _sourceRepository.Update(entity: source);

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
