using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoles;

/// <inheritdoc/>
internal sealed class GetRolesQueryHandler
    : IQueryHandler<GetRolesQuery, IEnumerable<Role>>
{
    private readonly IRoleRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRolesQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IRoleRepository"/>.</param>
    public GetRolesQueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IEnumerable<Role>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Role> roles = await _repository
                .GetAllAsync(
                    ids: request.Excludes ?? Array.Empty<int>(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return roles.ToList();
    }
}
