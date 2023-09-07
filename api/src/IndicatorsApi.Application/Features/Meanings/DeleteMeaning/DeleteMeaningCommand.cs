namespace IndicatorsApi.Application.Features.Meanings.DeleteMeaning;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Meaning id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteMeaningCommand(int Id)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
