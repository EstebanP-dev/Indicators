namespace IndicatorsApi.Application.Features.Sources.UpdateSection;

/// <summary>
/// Update s
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSourceCommand(int Id, string Name)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
