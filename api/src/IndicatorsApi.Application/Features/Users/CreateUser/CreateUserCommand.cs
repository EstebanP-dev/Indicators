using IndicatorsApi.Contracts.Features.Users.CreateUser;

namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateUserCommand(string Email, string Password)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter