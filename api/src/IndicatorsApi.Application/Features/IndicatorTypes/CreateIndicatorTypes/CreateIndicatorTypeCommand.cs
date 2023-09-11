namespace IndicatorsApi.Application.Features.IndicatorTypes.CreateIndicatorType;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorTypeCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter