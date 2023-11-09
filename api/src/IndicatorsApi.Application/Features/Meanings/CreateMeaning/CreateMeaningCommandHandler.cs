using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Meanings.CreateMeaning;

/// <inheritdoc/>
internal sealed class CreateMeaningCommandHandler
    : ICommandHandler<CreateMeaningCommand, Created>
{
    private readonly IMeaningRepository _meaningRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMeaningCommandHandler"/> class.
    /// </summary>
    /// <param name="meaningRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateMeaningCommandHandler(IMeaningRepository meaningRepository, IUnitOfWork unitOfWork)
    {
        _meaningRepository = meaningRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateMeaningCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _meaningRepository.Add(entity: request.Adapt<Meaning>());

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
