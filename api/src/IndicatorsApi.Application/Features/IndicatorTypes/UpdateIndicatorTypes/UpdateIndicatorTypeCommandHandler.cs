using IndicatorsApi.Application.Features.IndicatorTypes.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.IndicatorTypes;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.IndicatorTypes.UpdateIndicatorType;

/// <inheritdoc/>
internal sealed class UpdateIndicatorTypeCommandHandler
    : ICommandHandler<UpdateIndicatorTypeCommand, Updated>
{
    private readonly IIndicatorTypeRepository _indicatorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateIndicatorTypeCommandHandler(IIndicatorTypeRepository indicatorTypeRepository, IUnitOfWork unitOfWork)
    {
        _indicatorTypeRepository = indicatorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateIndicatorTypeCommand request, CancellationToken cancellationToken)
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

            indicatorType.Name = request.Name;

            _indicatorTypeRepository.Update(entity: indicatorType);

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
