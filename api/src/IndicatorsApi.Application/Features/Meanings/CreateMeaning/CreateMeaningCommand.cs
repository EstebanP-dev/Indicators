namespace IndicatorsApi.Application.Features.Meanings.CreateMeaning;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateMeaningCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter