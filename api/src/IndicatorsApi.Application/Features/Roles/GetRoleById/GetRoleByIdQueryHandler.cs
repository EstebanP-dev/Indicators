﻿using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoleById;

/// <inheritdoc/>
internal sealed class GetRoleByIdQueryHandler
    : IQueryHandler<GetRoleByIdQuery, Role>
{
    private readonly ISourceRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRoleByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="ISourceRepository"/>.</param>
    public GetRoleByIdQueryHandler(ISourceRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Role? role = await _roleRepository
            .GetByIdAsync(
                id: RoleId.ToRoleId(request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
        {
            return DomainErrors.NotFound<Role>();
        }

        return role;
    }
}
