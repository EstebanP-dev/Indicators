namespace IndicatorsApi.Application.Features.Meanings.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Meaning id.</param>
/// <param name="Name">Meaning name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateMeaningCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
