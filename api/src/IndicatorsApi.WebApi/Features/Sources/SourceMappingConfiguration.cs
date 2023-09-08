using IndicatorsApi.Application.Features.Sources.UpdateSection;
using IndicatorsApi.Contracts.Features.Sources.GetSourceById;
using IndicatorsApi.Contracts.Features.Sources.GetSourcesPagination;
using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.WebApi.Features.Sources;

/// <inheritdoc/>
internal sealed class SourceMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Source, SourceByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Source, SourcePaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
