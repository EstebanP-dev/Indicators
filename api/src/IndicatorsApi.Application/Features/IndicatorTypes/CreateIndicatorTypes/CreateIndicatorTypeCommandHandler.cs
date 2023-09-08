using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.IndicatorTypes;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.IndicatorTypes.CreateIndicatorType;

/// <inheritdoc/>
internal sealed class CreateIndicatorTypeCommandHandler
    : ICommandHandler<CreateIndicatorTypeCommand>
{
    private readonly IIndicatorTypeRepository _indicatorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateIndicatorTypeCommandHandler(IIndicatorTypeRepository indicatorTypeRepository, IUnitOfWork unitOfWork)
    {
        _indicatorTypeRepository = indicatorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Handle(CreateIndicatorTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _indicatorTypeRepository.Add(entity: request.Adapt<IndicatorType>());

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
