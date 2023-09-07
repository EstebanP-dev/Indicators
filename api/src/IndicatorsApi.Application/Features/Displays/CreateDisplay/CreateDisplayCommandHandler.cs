using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Displays.CreateDisplay;

/// <inheritdoc/>
internal sealed class CreateDisplayCommandHandler
    : ICommandHandler<CreateDisplayCommand>
{
    private readonly IDisplayRepository _displayRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDisplayCommandHandler"/> class.
    /// </summary>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateDisplayCommandHandler(IDisplayRepository displayRepository, IUnitOfWork unitOfWork)
    {
        _displayRepository = displayRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Handle(CreateDisplayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _displayRepository.Add(entity: request.Adapt<Display>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Success;
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
