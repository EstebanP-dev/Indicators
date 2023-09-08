using IndicatorsApi.Contracts.Features.Meanings.GetMeaningById;
using IndicatorsApi.Contracts.Features.Meanings.GetMeaningsPagination;
using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.WebApi.Features.Meanings;

/// <inheritdoc/>
internal sealed class MeaningMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Meaning, MeaningByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Meaning, MeaningPaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
