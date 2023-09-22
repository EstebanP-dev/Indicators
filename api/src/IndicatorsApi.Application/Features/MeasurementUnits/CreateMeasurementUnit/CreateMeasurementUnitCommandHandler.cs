using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.MeasurementUnits.CreateMeasurementUnit;

/// <inheritdoc/>
internal sealed class CreateMeasurementUnitCommandHandler
    : ICommandHandler<CreateMeasurementUnitCommand, Created>
{
    private readonly IMeasurementUnitRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMeasurementUnitCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateMeasurementUnitCommandHandler(IMeasurementUnitRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _repository.Add(entity: request.Adapt<MeasurementUnit>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Created;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex);
            return DomainErrors.CreationOrUpdatingFailed;
        }
        catch (OperationCanceledException)
        {
            return DomainErrors.CancelledOperation;
        }
    }
}
