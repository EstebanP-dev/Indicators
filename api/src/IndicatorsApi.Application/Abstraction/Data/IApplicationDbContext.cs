﻿using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Features.Frecuencies;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Abstraction.Data;

/// <summary>
/// Application database context interface.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets or sets the user table.
    /// </summary>
    /// <value>
    /// User table.
    /// </value>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the role table.
    /// </summary>
    /// <value>
    /// The role table.
    /// </value>
    DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the section table.
    /// </summary>
    /// <value>
    /// The section table.
    /// </value>
    DbSet<Section> Sections { get; set; }

    /// <summary>
    /// Gets or sets the sub section table.
    /// </summary>
    /// <value>
    /// The sub section table.
    /// </value>
    DbSet<SubSection> SubSections { get; set; }

    /// <summary>
    /// Gets or sets the section table.
    /// </summary>
    /// <value>
    /// The section table.
    /// </value>
    DbSet<Source> Sources { get; set; }

    /// <summary>
    /// Gets or sets the measurement unit table.
    /// </summary>
    /// <value>
    /// The measurement unit table.
    /// </value>
    DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    /// <summary>
    /// Gets or sets the articles table.
    /// </summary>
    /// <value>
    /// The articles table.
    /// </value>
    DbSet<Article> Articles { get; set; }

    /// <summary>
    /// Gets or sets the frecuency table.
    /// </summary>
    /// <value>
    /// The frecuency table.
    /// </value>
    DbSet<Frecuency> Frecuencies { get; set; }
}
