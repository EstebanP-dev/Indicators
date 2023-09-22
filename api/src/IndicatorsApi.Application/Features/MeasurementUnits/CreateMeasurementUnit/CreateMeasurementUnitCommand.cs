namespace IndicatorsApi.Application.Features.MeasurementUnits.CreateMeasurementUnit;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Description">MeasurementUnit description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateMeasurementUnitCommand(string Description)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter