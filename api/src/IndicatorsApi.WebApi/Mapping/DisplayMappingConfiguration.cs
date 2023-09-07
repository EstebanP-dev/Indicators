﻿using IndicatorsApi.Contracts.Features.Displays.GetDisplayById;
using IndicatorsApi.Contracts.Features.Displays.GetDisplaysPagination;
using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Persistence.Mapping;

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
    }
}
