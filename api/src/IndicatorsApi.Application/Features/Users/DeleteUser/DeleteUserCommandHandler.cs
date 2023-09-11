using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Users.DeleteUser;

/// <inheritdoc/>
internal sealed class DeleteUserCommandHandler
    : ICommandHandler<DeleteUserCommand, Deleted>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository
                .GetByIdAsync(id: UserId.ToUserId(value: request.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
            {
                return DomainErrors.NotFound<User>();
            }

            _userRepository.Delete(entity: user);

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
