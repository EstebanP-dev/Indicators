using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Variables;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Variable name.</param>
/// <param name="CreatedBy">Variable created by.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateVariableCommand(string Name, string CreatedBy)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateVariableCommandHandler
    : ICommandHandler<CreateVariableCommand, Created>
{
    private readonly IVariableRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVariableCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IVariableRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateVariableCommandHandler(IVariableRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateVariableCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: new Variable()
        {
            Id = -1,
            Name = request.Name,
            CreationDate = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}