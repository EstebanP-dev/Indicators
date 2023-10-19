using IndicatorsApi.Application.Features.MeasurementUnits.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.MeasurementUnits.UpdateMeasurementUnit;

/// <inheritdoc/>
internal sealed class UpdateMeasurementUnitCommandHandler
    : ICommandHandler<UpdateMeasurementUnitCommand, Updated>
{
    private readonly IMeasurementUnitRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMeasurementUnitCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateMeasurementUnitCommandHandler(IMeasurementUnitRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            MeasurementUnit? source = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (source is null)
            {
                return DomainErrors.NotFound<MeasurementUnit>();
            }

            source.Description = request.Description;

            _repository.Update(entity: source);

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
