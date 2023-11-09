using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Sources.CreateSource;

/// <inheritdoc/>
internal sealed class CreateSourceCommandHandler
    : ICommandHandler<CreateSourceCommand, Created>
{
    private readonly ISourceRepository _sourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSourceCommandHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateSourceCommandHandler(ISourceRepository sourceRepository, IUnitOfWork unitOfWork)
    {
        _sourceRepository = sourceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _sourceRepository.Add(entity: request.Adapt<Source>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Created;
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
