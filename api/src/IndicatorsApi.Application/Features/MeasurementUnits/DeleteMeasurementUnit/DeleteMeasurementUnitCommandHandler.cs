using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.MeasurementUnits.DeleteMeasurementUnit;

/// <inheritdoc/>
internal sealed class DeleteMeasurementUnitCommandHandler
    : ICommandHandler<DeleteMeasurementUnitCommand, Deleted>
{
    private readonly IMeasurementUnitRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMeasurementUnitCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteMeasurementUnitCommandHandler(IMeasurementUnitRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            MeasurementUnit? meaning = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (meaning is null)
            {
                return DomainErrors.NotFound<MeasurementUnit>();
            }

            _repository.Delete(entity: meaning);

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
