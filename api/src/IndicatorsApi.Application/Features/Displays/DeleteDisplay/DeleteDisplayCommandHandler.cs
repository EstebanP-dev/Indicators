using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Application.Features.Displays.DeleteDisplay;

/// <inheritdoc/>
internal sealed class DeleteDisplayCommandHandler
    : ICommandHandler<DeleteDisplayCommand, Deleted>
{
    private readonly IDisplayRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDisplayCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteDisplayCommandHandler(IDisplayRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteDisplayCommand request, CancellationToken cancellationToken)
    {
        Display? display = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (display is null)
        {
            return DomainErrors.NotFound<Display>();
        }

        _repository.Delete(entity: display);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}
