using IndicatorsApi.Application.Features.Sources.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Sources.UpdateSource;

/// <inheritdoc/>
internal sealed class UpdateSourceCommandHandler
    : ICommandHandler<UpdateSourceCommand, Updated>
{
    private readonly ISourceRepository _sourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSourceCommandHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateSourceCommandHandler(ISourceRepository sourceRepository, IUnitOfWork unitOfWork)
    {
        _sourceRepository = sourceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Source? source = await _sourceRepository
                    .GetByIdAsync(id: SourceId.ToSourceId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (source is null)
            {
                return DomainErrors.NotFound<Source>();
            }

            source.Name = request.Name;

            _sourceRepository.Update(entity: source);

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
