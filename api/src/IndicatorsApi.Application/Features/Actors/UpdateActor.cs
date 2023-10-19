using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateActorCommand(string Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateActorCommandHandler
    : ICommandHandler<UpdateActorCommand, Updated>
{
    private readonly IActorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateActorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateActorCommandHandler(IActorRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        Actor? actionType = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (actionType is null)
        {
            return DomainErrors.NotFound<Actor>();
        }

        actionType.Name = request.Name;

        _repository.Update(entity: actionType);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}