﻿namespace IndicatorsApi.Contracts.MeasurementUnits;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">MeasurementUnit id.</param>
/// <param name="Description">MeasurementUnit description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class MeasurementUnitPaginationResponse(int Id, string Description);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter