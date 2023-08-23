﻿using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Application.Abstraction.Messaging;

/// <summary>
/// Interface of <see cref="IRequest"/> and <seealso cref="ICommandBase"/>.
/// </summary>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface ICommand : IRequest<Result>, ICommandBase
#pragma warning restore CA1040 // Avoid empty interfaces
{
}

/// <summary>
/// Interface of <see cref="IRequest{TResponse}"/> and <seealso cref="ICommandBase"/>.
/// </summary>
/// <typeparam name="TResponse">Response value type.</typeparam>
#pragma warning disable CA1040 // Avoid empty interfaces
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
#pragma warning restore CA1040 // Avoid empty interfaces
{
}
