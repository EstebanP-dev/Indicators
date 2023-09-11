namespace IndicatorsApi.Application.Features.Displays.UpdateSection;

/// <summary>
/// Update s
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateDisplayCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
