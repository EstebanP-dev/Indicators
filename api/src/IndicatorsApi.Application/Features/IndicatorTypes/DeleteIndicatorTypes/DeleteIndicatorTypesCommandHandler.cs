using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.IndicatorTypes;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.IndicatorTypes.DeleteIndicatorType;

/// <inheritdoc/>
internal sealed class DeleteIndicatorTypeCommandHandler
    : ICommandHandler<DeleteIndicatorTypeCommand, Deleted>
{
    private readonly IIndicatorTypeRepository _indicatorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteIndicatorTypeCommandHandler(IIndicatorTypeRepository indicatorTypeRepository, IUnitOfWork unitOfWork)
    {
        _indicatorTypeRepository = indicatorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteIndicatorTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            IndicatorType? indicatorType = await _indicatorTypeRepository
                .GetByIdAsync(id: IndicatorTypeId.ToIndicatorTypeId(value: request.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (indicatorType is null)
            {
                return DomainErrors.NotFound<IndicatorType>();
            }

            _indicatorTypeRepository.Delete(entity: indicatorType);

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
