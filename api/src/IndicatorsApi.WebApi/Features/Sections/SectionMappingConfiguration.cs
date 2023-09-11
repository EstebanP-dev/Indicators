using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.WebApi.Features.Sections;

/// <inheritdoc/>
internal sealed class SectionMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Section, SectionByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Section, SectionPaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
