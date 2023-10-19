using IndicatorsApi.Contracts.Variables;
using IndicatorsApi.Domain.Features.Variables;

namespace IndicatorsApi.Application.Features.Variables;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
public sealed record class GetVariablesPaginationQuery(PaginationParameters<int> Parameters)
    : IQuery<Pagination<VariablePaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetVariablesPaginationQueryHandler
    : IQueryHandler<GetVariablesPaginationQuery, Pagination<VariablePaginationResponse>>
{
    private readonly IVariableRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetVariablesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IVariableRepository"/>.</param>
    public GetVariablesPaginationQueryHandler(IVariableRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<VariablePaginationResponse>>> Handle(GetVariablesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<VariablePaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new VariablePaginationResponse(x.Id, x.Name),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}