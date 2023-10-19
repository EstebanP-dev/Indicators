namespace IndicatorsApi.Application.Abstraction.Messaging;

/// <summary>
/// <see cref="ICommand"/> and <seealso cref="ICommand{TResponse}"/> base interface.
/// </summary>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface ICommandBase
#pragma warning restore CA1040 // Avoid empty interfaces
{
}

#pragma warning disable CA1040 // Avoid empty interfaces
/// <summary>
/// Interface of <see cref="IRequest{TResponse}"/> and <seealso cref="ICommandBase"/>.
/// </summary>
/// <typeparam name="TResponse">Response value type.</typeparam>
public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>, ICommandBase
{
}

/// <summary>
/// No return command.
/// </summary>
public interface ICommand : ICommand<Success>
{
}

/// <summary>
/// Create command.
/// </summary>
public interface ICreateCommand : ICommand<Created>
{
}

/// <summary>
/// Create command.
/// </summary>
public interface IUpdateCommand : ICommand<Updated>
{
}

/// <summary>
/// Delete command.
/// </summary>
public interface IDeleteCommand : ICommand<Deleted>
{
}
#pragma warning restore CA1040 // Avoid empty interfaces
