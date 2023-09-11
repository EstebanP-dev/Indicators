using IndicatorsApi.Contracts.ActorTypes;
using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.WebApi.Features.ActorTypes;

/// <inheritdoc/>
internal sealed class ActorTypeMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ActorType, ActorTypeByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<ActorType, ActorTypePaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
