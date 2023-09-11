namespace IndicatorsApi.Application.Features.IndicatorTypes.DeleteIndicatorType;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteIndicatorTypeCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
