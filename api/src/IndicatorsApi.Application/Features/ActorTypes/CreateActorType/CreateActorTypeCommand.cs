using IndicatorsApi.Contracts.Features.Users.CreateUser;

namespace IndicatorsApi.Application.Features.ActorTypes.CreateActorType;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">ActorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateActorTypeCommand(string Name)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter