using IndicatorsApi.Contracts.IndicatorTypes;
using IndicatorsApi.Domain.Features.IndicatorTypes;

namespace IndicatorsApi.WebApi.Features.IndicatorTypes;

/// <inheritdoc/>
internal sealed class IndicatorTypeMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IndicatorType, IndicatorTypeByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<IndicatorType, IndicatorTypePaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
