﻿using IndicatorsApi.Contracts.Features.Sections.GetSectionById;
using IndicatorsApi.Contracts.Features.Sections.GetSectionsPagination;
using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Persistence.Mapping;

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
