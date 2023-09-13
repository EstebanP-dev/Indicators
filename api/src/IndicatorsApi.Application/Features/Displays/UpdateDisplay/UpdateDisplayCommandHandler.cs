using IndicatorsApi.Application.Abstraction.Enums;
using IndicatorsApi.Application.Features.Displays.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Displays.UpdateDisplay;

/// <inheritdoc/>
internal sealed class UpdateDisplayCommandHandler
    : ICommandHandler<UpdateDisplayCommand, Updated>
{
    private readonly IDisplayRepository _displayRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDisplayCommandHandler"/> class.
    /// </summary>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateDisplayCommandHandler(IDisplayRepository displayRepository, IUnitOfWork unitOfWork)
    {
        _displayRepository = displayRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateDisplayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null)
            {
                return DomainErrors.NotFound<Display>();
            }

            Display? display = await _displayRepository
                    .GetByIdAsync(id: DisplayId.ToDisplayId(value: request.Id.Value), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (display is null)
            {
                return DomainErrors.NotFound<Display>();
            }

            switch (request.Operations)
            {
                case UpdateOperations.PUT:
                    display = request.Adapt<Display>();
                    break;
                case UpdateOperations.PATCH:
                    display.Name = request.Name ?? display.Name;
                    break;
                default:
                    throw new InvalidCastException();
            }

            _displayRepository.Update(entity: display);

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
