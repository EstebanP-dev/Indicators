﻿namespace IndicatorsApi.Application.Features.IndicatorTypes.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorTypeCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter