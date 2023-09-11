using IndicatorsApi.Application.Features.Meanings.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Meanings.UpdateMeaning;

/// <inheritdoc/>
internal sealed class UpdateMeaningCommandHandler
    : ICommandHandler<UpdateMeaningCommand, Updated>
{
    private readonly IMeaningRepository _sourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMeaningCommandHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateMeaningCommandHandler(IMeaningRepository sourceRepository, IUnitOfWork unitOfWork)
    {
        _sourceRepository = sourceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateMeaningCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Meaning? source = await _sourceRepository
                    .GetByIdAsync(id: MeaningId.ToMeaningId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (source is null)
            {
                return DomainErrors.NotFound<Meaning>();
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
