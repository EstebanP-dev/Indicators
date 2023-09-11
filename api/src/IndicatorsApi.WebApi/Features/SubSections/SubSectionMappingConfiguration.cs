using IndicatorsApi.Contracts.SubSections;
using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.WebApi.Features.SubSections;

/// <inheritdoc/>
internal sealed class SubSectionMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubSection, SubSectionByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<SubSection, SubSectionPaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
