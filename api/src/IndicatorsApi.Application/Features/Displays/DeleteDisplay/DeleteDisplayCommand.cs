namespace IndicatorsApi.Application.Features.Displays.DeleteDisplay;

/// <summary>
/// Delete display command.
/// </summary>
/// <param name="Id">Display id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteDisplayCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
