﻿using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.SubSections;

/// <inheritdoc/>
internal sealed class SubSectionRepository
    : Repository<SubSection, string>, ISubSectionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSectionRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public SubSectionRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}