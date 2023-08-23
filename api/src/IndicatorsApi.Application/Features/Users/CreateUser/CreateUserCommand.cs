﻿namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <summary>
/// Create User Command.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Not necessary.")]
public record class CreateUserCommand(string Email, string Password)
    : ICommand;
