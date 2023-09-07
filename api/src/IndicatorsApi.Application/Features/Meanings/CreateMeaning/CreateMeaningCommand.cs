using IndicatorsApi.Contracts.Features.Users.CreateUser;

namespace IndicatorsApi.Application.Features.Meanings.CreateMeaning;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateMeaningCommand(string Name)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter