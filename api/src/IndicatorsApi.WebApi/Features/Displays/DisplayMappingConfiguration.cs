using IndicatorsApi.Application.Features.Displays.UpdateSection;
using IndicatorsApi.Contracts.Displays;
using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.WebApi.Features.Displays;

/// <inheritdoc/>
internal sealed class DisplayMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Display, DisplayByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Display, DisplayPaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<UpdateDisplayCommand, Display>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name ?? string.Empty)
            .ConstructUsing(src => new Display(src.Id ?? -1, src.Name ?? string.Empty));
    }
}
