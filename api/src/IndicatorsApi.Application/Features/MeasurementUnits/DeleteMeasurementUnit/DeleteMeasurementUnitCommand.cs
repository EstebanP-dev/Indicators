namespace IndicatorsApi.Application.Features.MeasurementUnits.DeleteMeasurementUnit;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">MeasurementUnit id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteMeasurementUnitCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
