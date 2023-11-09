using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Variables;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Variable id.</param>
/// <param name="Name">Variable name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateVariableCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateVariableCommandHandler
    : ICommandHandler<UpdateVariableCommand, Updated>
{
    private readonly IVariableRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateVariableCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IVariableRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateVariableCommandHandler(IVariableRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateVariableCommand request, CancellationToken cancellationToken)
    {
        Variable? actionType = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

        if (actionType is null)
        {
            return DomainErrors.NotFound<Variable>();
        }

        actionType.Name = request.Name;

        _repository.Update(entity: actionType);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}