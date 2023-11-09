namespace IndicatorsApi.Application.Features.MeasurementUnits.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">MeasurementUnit id.</param>
/// <param name="Description">MeasurementUnit description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateMeasurementUnitCommand(int Id, string Description)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
