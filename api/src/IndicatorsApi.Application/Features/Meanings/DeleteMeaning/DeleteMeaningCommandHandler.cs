using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Meanings.DeleteMeaning;

/// <inheritdoc/>
internal sealed class DeleteMeaningCommandHandler
    : ICommandHandler<DeleteMeaningCommand, Deleted>
{
    private readonly IMeaningRepository _meaningRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMeaningCommandHandler"/> class.
    /// </summary>
    /// <param name="meaningRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteMeaningCommandHandler(IMeaningRepository meaningRepository, IUnitOfWork unitOfWork)
    {
        _meaningRepository = meaningRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteMeaningCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Meaning? meaning = await _meaningRepository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (meaning is null)
            {
                return DomainErrors.NotFound<Meaning>();
            }

            _meaningRepository.Delete(entity: meaning);

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
