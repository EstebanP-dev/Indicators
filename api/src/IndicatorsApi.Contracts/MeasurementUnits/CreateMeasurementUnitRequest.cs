namespace IndicatorsApi.Contracts.MeasurementUnits;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Description">MeasurementUnit description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateMeasurementUnitRequest(string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter