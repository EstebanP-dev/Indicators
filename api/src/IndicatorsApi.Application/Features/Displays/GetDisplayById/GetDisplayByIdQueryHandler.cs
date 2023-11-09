using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Application.Features.Displays.GetDisplayById;

/// <inheritdoc/>
internal sealed class GetDisplayByIdQueryHandler
    : IQueryHandler<GetDisplayByIdQuery, Display>
{
    private readonly IDisplayRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDisplayByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IDisplayRepository"/>.</param>
    public GetDisplayByIdQueryHandler(IDisplayRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Display>> Handle(GetDisplayByIdQuery request, CancellationToken cancellationToken)
    {
        Display? display = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (display is null)
        {
            return DomainErrors.NotFound<Display>();
        }

        return display;
    }
}
