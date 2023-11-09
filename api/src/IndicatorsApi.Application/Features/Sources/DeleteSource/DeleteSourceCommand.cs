namespace IndicatorsApi.Application.Features.Sources.DeleteSource;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Source id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteSourceCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
