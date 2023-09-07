﻿using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Sources.DeleteSource;

/// <inheritdoc/>
internal sealed class DeleteSourceCommandHandler
    : ICommandHandler<DeleteSourceCommand>
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
    public async Task<ErrorOr<Success>> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
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

            _sourceRepository.Delete(entity: source);

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
