using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Variables;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Variable id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteVariableCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteVariableCommandHandler
    : ICommandHandler<DeleteVariableCommand, Deleted>
{
    private readonly IVariableRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteVariableCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IVariableRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteVariableCommandHandler(IVariableRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteVariableCommand request, CancellationToken cancellationToken)
    {
        Variable? actorType = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<Variable>();
        }

        _repository.Delete(entity: actorType);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}