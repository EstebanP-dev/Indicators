using IndicatorsApi.Contracts.Features.Users.CreateUser;

namespace IndicatorsApi.Application.Features.Sources.CreateSource;

/// <summary>
/// Create source command.
/// </summary>
/// <param name="Name">Source name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateSourceCommand(string Name)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter