using IndicatorsApi.Contracts.MeasurementUnits;
using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.WebApi.Features.MeasurementUnits;

/// <inheritdoc/>
internal sealed class MeasurementUnitMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MeasurementUnit, MeasurementUnitByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<MeasurementUnit, MeasurementUnitPaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Description, src => src.Description);
    }
}
