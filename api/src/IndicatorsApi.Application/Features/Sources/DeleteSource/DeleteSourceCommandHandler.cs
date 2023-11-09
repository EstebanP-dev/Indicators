using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Sources.DeleteSource;

/// <inheritdoc/>
internal sealed class DeleteSourceCommandHandler
    : ICommandHandler<DeleteSourceCommand, Deleted>
{
    private readonly ISourceRepository _sourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSourceCommandHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteSourceCommandHandler(ISourceRepository sourceRepository, IUnitOfWork unitOfWork)
    {
        _sourceRepository = sourceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Source? source = await _sourceRepository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (source is null)
            {
                return DomainErrors.NotFound<Source>();
            }

            _sourceRepository.Delete(entity: source);

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
