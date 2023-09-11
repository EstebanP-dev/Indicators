using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Displays.DeleteDisplay;

/// <inheritdoc/>
internal sealed class DeleteDisplayCommandHandler
    : ICommandHandler<DeleteDisplayCommand, Deleted>
{
    private readonly IDisplayRepository _displayRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDisplayCommandHandler"/> class.
    /// </summary>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteDisplayCommandHandler(IDisplayRepository displayRepository, IUnitOfWork unitOfWork)
    {
        _displayRepository = displayRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteDisplayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Display? display = await _displayRepository
                .GetByIdAsync(id: DisplayId.ToDisplayId(value: request.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (display is null)
            {
                return DomainErrors.NotFound<Display>();
            }

            _displayRepository.Delete(entity: display);

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
