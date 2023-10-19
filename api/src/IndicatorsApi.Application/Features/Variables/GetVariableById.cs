using IndicatorsApi.Contracts.Variables;
using IndicatorsApi.Domain.Features.Variables;
using Mapster;

namespace IndicatorsApi.Application.Features.Variables;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Variable id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetVariableByIdQuery(int Id)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<VariableByIdResponse>;

/// <inheritdoc/>
internal sealed class GetVariableByIdQueryHandler
    : IQueryHandler<GetVariableByIdQuery, VariableByIdResponse>
{
    private readonly IVariableRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetVariableByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IVariableRepository"/>.</param>
    public GetVariableByIdQueryHandler(IVariableRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<VariableByIdResponse>> Handle(GetVariableByIdQuery request, CancellationToken cancellationToken)
    {
        Variable? actorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<Variable>();
        }

        return actorType.Adapt<VariableByIdResponse>();
    }
}
