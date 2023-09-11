namespace IndicatorsApi.Application.Features.Sources.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Source id.</param>
/// <param name="Name">Source name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateSourceCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
